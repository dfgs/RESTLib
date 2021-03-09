using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public abstract class RouteNode:IRouteNode
	{
		private Dictionary<string, StaticRouteNode> staticNodes;
		private VariableRouteNode variableNode;

		public IRouteHandler RouteHandler
		{
			get;
			set;
		}
		public MethodInfo MethodInfo
		{
			get;
			set;
		}

		public RouteNode()
		{
			staticNodes = new Dictionary<string, StaticRouteNode>();
			variableNode = null;
		}

		public StaticRouteNode CreateStaticNode(string Value)
		{
			StaticRouteNode node;

			if (string.IsNullOrEmpty(Value)) throw new ArgumentNullException(nameof(Value));
			if (!staticNodes.TryGetValue(Value, out node))
			{
				node = new StaticRouteNode(Value);
				staticNodes.Add(Value, node);
			}

			return node;
		}

		
		public VariableRouteNode CreateVariableNode(string Name)
		{
			if (variableNode==null) variableNode = new VariableRouteNode(Name);
					
			return variableNode;
		}

		public StaticRouteNode GetStaticNode(string Value)
		{
			StaticRouteNode node;

			if (string.IsNullOrEmpty(Value)) throw new ArgumentNullException(nameof(Value));
			staticNodes.TryGetValue(Value, out node);

			return node;
		}


		public VariableRouteNode GetVariableNode()
		{
			return variableNode;
		}


	}
}
