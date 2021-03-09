using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RESTLib.Server.UnitTest
{
	[TestClass]
	public class RouteParserUnitTest
	{
		[TestMethod]
		public void ShouldNotParseEmptyURL()
		{
			RouteParser parser;

			parser = new RouteParser();
			Assert.ThrowsException<ArgumentNullException>(() => parser.Parse(null));
			Assert.ThrowsException<ArgumentNullException>(() => parser.Parse(""));
		}

		[TestMethod]
		public void ShouldParseRootURL()
		{
			RouteParser parser;
			RouteSegment[] segments;

			parser = new RouteParser();
			segments=parser.Parse("root");
			Assert.AreEqual(1, segments.Length);
			Assert.AreEqual("root", ((StaticRouteSegment)segments[0]).Value);
		}

		[TestMethod]
		public void ShouldParseShortStaticURL()
		{
			RouteParser parser;
			RouteSegment[] segments;

			parser = new RouteParser();
			segments = parser.Parse("root/API");
			Assert.AreEqual(2, segments.Length);
			Assert.AreEqual("root", ((StaticRouteSegment)segments[0]).Value);
			Assert.AreEqual("API", ((StaticRouteSegment)segments[1]).Value);
		}

		[TestMethod]
		public void ShouldParseMediumStaticURL()
		{
			RouteParser parser;
			RouteSegment[] segments;

			parser = new RouteParser();
			segments = parser.Parse("root/API/Books/1");
			Assert.AreEqual(4, segments.Length);
			Assert.AreEqual("root", ((StaticRouteSegment)segments[0]).Value);
			Assert.AreEqual("API", ((StaticRouteSegment)segments[1]).Value);
			Assert.AreEqual("Books", ((StaticRouteSegment)segments[2]).Value);
			Assert.AreEqual("1", ((StaticRouteSegment)segments[3]).Value);
		}

		[TestMethod]
		public void ShouldParseURLWithVariable()
		{
			RouteParser parser;
			RouteSegment[] segments;

			parser = new RouteParser();
			segments = parser.Parse("root/API/Books/{id}");
			Assert.AreEqual(4, segments.Length);
			Assert.AreEqual("root", ((StaticRouteSegment)segments[0]).Value);
			Assert.AreEqual("API", ((StaticRouteSegment)segments[1]).Value);
			Assert.AreEqual("Books", ((StaticRouteSegment)segments[2]).Value);
			Assert.AreEqual("id", ((VariableRouteSegment)segments[3]).Name);
		}

		[TestMethod]
		public void ShouldParseURLWithVariableBis()
		{
			RouteParser parser;
			RouteSegment[] segments;

			parser = new RouteParser();
			segments = parser.Parse("root/API/Books/{id}/Name");
			Assert.AreEqual(5, segments.Length);
			Assert.AreEqual("root", ((StaticRouteSegment)segments[0]).Value);
			Assert.AreEqual("API", ((StaticRouteSegment)segments[1]).Value);
			Assert.AreEqual("Books", ((StaticRouteSegment)segments[2]).Value);
			Assert.AreEqual("id", ((VariableRouteSegment)segments[3]).Name);
			Assert.AreEqual("Name", ((StaticRouteSegment)segments[4]).Value);
		}





		[TestMethod]
		public void ShouldNotSplitEmptyURL()
		{
			RouteParser parser;

			parser = new RouteParser();
			Assert.ThrowsException<ArgumentNullException>(() => parser.Split(null));
			Assert.ThrowsException<ArgumentNullException>(() => parser.Split(""));
		}

		[TestMethod]
		public void ShouldSplitRootURL()
		{
			RouteParser parser;
			string[] segments;

			parser = new RouteParser();
			segments = parser.Split("root");
			Assert.AreEqual(1, segments.Length);
			Assert.AreEqual("root", segments[0]);
		}

		[TestMethod]
		public void ShouldSplitShortStaticURL()
		{
			RouteParser parser;
			string[] segments;

			parser = new RouteParser();
			segments = parser.Split("root/API");
			Assert.AreEqual(2, segments.Length);
			Assert.AreEqual("root",segments[0]);
			Assert.AreEqual("API", segments[1]);
		}

		[TestMethod]
		public void ShouldSplitMediumStaticURL()
		{
			RouteParser parser;
			string[] segments;

			parser = new RouteParser();
			segments = parser.Split("root/API/Books/1");
			Assert.AreEqual(4, segments.Length);
			Assert.AreEqual("root", segments[0]);
			Assert.AreEqual("API", segments[1]);
			Assert.AreEqual("Books", segments[2]);
			Assert.AreEqual("1", segments[3]);
		}

	
		

	}
}
