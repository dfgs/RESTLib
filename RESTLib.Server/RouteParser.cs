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
		private static Regex variableSegmentRegex = new Regex(@"{(?<Variable>[^}]+)}");

		public string GetPattern(string URL)
		{
			string pattern;
			string escapedURL;

			if (string.IsNullOrEmpty(URL)) throw new ArgumentNullException(nameof(URL));

			escapedURL = URL.Replace(".",@"\.").Replace("?",@"\?");
			pattern="^"+ variableSegmentRegex.Replace(escapedURL, EvaluateVariableMatch)+"$";
			
			
			return pattern;
		}

		private string EvaluateVariableMatch(Match Match)
		{
			return $"(?<{Match.Groups["Variable"].Value}>[^/&]+)";
		}
	
		


	}
}
