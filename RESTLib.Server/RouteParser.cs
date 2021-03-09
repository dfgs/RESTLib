using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class RouteParser : IRouteParser
	{
		private static Regex variableSegmentRegex = new Regex(@"\{([^}]+)\}");
		
		public RouteSegment[] Parse(string URL)
		{
			string[] parts;
			Match match;
			List<RouteSegment> segments;
			RouteSegment segment;

			if (string.IsNullOrEmpty(URL)) throw new ArgumentNullException(nameof(URL));
			parts = URL.Split('/');
			if (parts.Length==0) throw new ArgumentException(nameof(URL));

			segments = new List<RouteSegment>();
			foreach(string part in parts)
			{
				match = variableSegmentRegex.Match(part);

				if (match.Success) segment = new VariableRouteSegment(match.Groups[1].Value);
				else segment = new StaticRouteSegment(part);
				
				segments.Add(segment) ;
			}

			return segments.ToArray();
		}



	}
}
