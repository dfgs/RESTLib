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
			StaticRouteNode child;

			node = new StaticRouteNode("root");
			child=node.CreateStaticNode("Static1");
			Assert.IsNotNull(child);
			Assert.AreEqual("Static1",child.Value);
		}
		
		[TestMethod]
		public void ShouldSetVariableChildNodeIfDoesntExists()
		{
			RouteNode node;
			VariableRouteNode child;

			node = new StaticRouteNode("root");
			child = node.CreateVariableNode("A");
			Assert.IsNotNull(child);
			Assert.AreEqual("A", child.Name);
		}


		[TestMethod]
		public void ShouldCreateStaticChildNodeIfExists()
		{
			RouteNode node;
			StaticRouteNode child,node2;

			node = new StaticRouteNode("root");
			child = node.CreateStaticNode("Static1");
			Assert.IsNotNull(child);
			Assert.AreEqual("Static1", child.Value);
			node2 = node.CreateStaticNode("Static1");
			Assert.AreEqual(child, node2);
			Assert.AreEqual("Static1", node2.Value);
		}
		[TestMethod]
		public void ShouldCreateVariableChildNodeIfExists()
		{
			RouteNode node;
			VariableRouteNode child, node2;

			node = new StaticRouteNode("root");
			child = node.CreateVariableNode("A");
			Assert.IsNotNull(child);
			Assert.AreEqual("A", child.Name);
			node2 = node.CreateVariableNode("A");
			Assert.AreEqual(child, node2);
			Assert.AreEqual("A", node2.Name);
		}


		[TestMethod]
		public void ShouldGetStaticChildNode()
		{
			RouteNode node;
			StaticRouteNode child;

			node = new StaticRouteNode("root");
			child = node.CreateStaticNode("Static1");
			Assert.IsNotNull(child);
			Assert.AreEqual("Static1", child.Value);
			child = node.GetStaticNode("Static1");
			Assert.IsNotNull(child);
			Assert.AreEqual("Static1", child.Value);

		}

		[TestMethod]
		public void ShouldGetVariableChildNode()
		{
			RouteNode node;
			VariableRouteNode child;

			node = new StaticRouteNode("root");
			child = node.CreateVariableNode("A");
			Assert.IsNotNull(child);
			Assert.AreEqual("A", child.Name);
			child = node.GetVariableNode();
			Assert.IsNotNull(child);
			Assert.AreEqual("A", child.Name);
		}

		[TestMethod]
		public void ShouldNotGetStaticChildNode()
		{
			RouteNode node;
			RouteNode child;

			node = new StaticRouteNode("root");
			child = node.GetStaticNode("Static1");
			Assert.IsNull(child);

		}

		[TestMethod]
		public void ShouldNotGetVariableChildNode()
		{
			RouteNode node;
			RouteNode child;

			node = new StaticRouteNode("root");
			child = node.GetVariableNode();
			Assert.IsNull(child);
		}

	}
}
