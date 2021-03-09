using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RESTLib.Server.UnitTest
{
	[TestClass]
	public class RouteNodeUnitTest
	{
		[TestMethod]
		public void ShouldAddStaticChildNodeIfDoesntExists()
		{
			RouteNode node;
			RouteNode child;

			node = new RouteNode();
			child=node.GetStaticNode("Static1");
			Assert.IsNotNull(child);
		}
		
		[TestMethod]
		public void ShouldSetVariableChildNodeIfDoesntExists()
		{
			RouteNode node;
			RouteNode child;

			node = new RouteNode();
			child = node.GetVariableNode();
			Assert.IsNotNull(child);
		}

		
		[TestMethod]
		public void ShouldGetStaticChildNodeIfExists()
		{
			RouteNode node;
			RouteNode child,node2;

			node = new RouteNode();
			child = node.GetStaticNode("Static1");
			Assert.IsNotNull(child);
			node2 = node.GetStaticNode("Static1");
			Assert.AreEqual(child, node2);
		}
		[TestMethod]
		public void ShouldGetVariableChildNodeIfExists()
		{
			RouteNode node;
			RouteNode child, node2;

			node = new RouteNode();
			child = node.GetVariableNode();
			Assert.IsNotNull(child);
			node2 = node.GetVariableNode();
			Assert.AreEqual(child, node2);
		}

		

	}
}
