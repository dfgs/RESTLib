using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public interface IRouteParser
	{
		string GetPattern(string URL);
		string[] Split(string URL);
	}
}
