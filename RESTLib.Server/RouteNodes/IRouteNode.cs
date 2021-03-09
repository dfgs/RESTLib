using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public interface IRouteNode
	{
		MethodInfo MethodInfo
		{
			get;
			set;
		}
		RouteNode GetStaticNode(string Value);
		RouteNode GetVariableNode();


	}
}
