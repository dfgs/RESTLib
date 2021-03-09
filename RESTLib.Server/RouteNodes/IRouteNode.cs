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
		IRouteHandler RouteHandler
		{
			get;
			set;
		}

		MethodInfo MethodInfo
		{
			get;
			set;
		}
		StaticRouteNode CreateStaticNode(string Value);
		VariableRouteNode CreateVariableNode(string Name);

		StaticRouteNode GetStaticNode(string Value);
		VariableRouteNode GetVariableNode();

	}
}
