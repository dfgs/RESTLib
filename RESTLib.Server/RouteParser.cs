using RESTLib.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	// http://api.example.com/device-management/managed-devices?region=USA&brand=XYZ&sort=installation-date

	// 
	public class RouteParser : IRouteParser
	{
		private static Regex variableSegmentRegex = new Regex(@"\{([^}]+)\}");

		public string GetPattern(string URL)
		{
			string[] parts;
			string pattern;
			Match match;

			if (string.IsNullOrEmpty(URL)) throw new ArgumentNullException(nameof(URL));
			parts = URL.Split('/');
			if (parts.Length == 0) throw new InvalidURLException(URL);

			pattern = "^";
			foreach (string part in parts)
			{
				if (string.IsNullOrEmpty(part)) continue;

				match = variableSegmentRegex.Match(part);

				if (match.Success) pattern += $"/(?<{match.Groups[1].Value}>[^/]+)";
				else pattern += $"/{Regex.Escape(part)}";
			}
			pattern += "$";
			return pattern;
		}

	
		public string[] Split(string URL)
		{
			string[] parts;

			if (string.IsNullOrEmpty(URL)) throw new ArgumentNullException(nameof(URL));
			parts = URL.Split('/');
			if (parts.Length == 0) throw new InvalidURLException(URL);

			return parts.Where(item=>!string.IsNullOrEmpty(item)).ToArray();
		}


	}
}
