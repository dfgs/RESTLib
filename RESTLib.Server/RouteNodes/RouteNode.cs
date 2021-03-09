using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class RouteNode:IRouteNode
	{
		private Dictionary<string, RouteNode> staticNodes;
		private RouteNode variableNode;

		public MethodInfo MethodInfo
		{
			get;
			set;
		}

		public RouteNode()
		{
			staticNodes = new Dictionary<string, RouteNode>();
			variableNode = null;
		}

		public RouteNode GetStaticNode(string Value)
		{
			RouteNode node;

			if (string.IsNullOrEmpty(Value)) throw new ArgumentNullException(nameof(Value));
			if (!staticNodes.TryGetValue(Value, out node))
			{
				node = new RouteNode();
				staticNodes.Add(Value, node);
			}

			return node;
		}

		

		public RouteNode GetVariableNode()
		{
			if (variableNode==null) variableNode = new RouteNode();
					
			return variableNode;
		}
		
		


	}
}
