using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RESTLib.Server.UnitTest
{
	[TestClass]
	public class RouteParserUnitTest
	{
		


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

		[TestMethod]
		public void ShouldSplitURLStartingWithSlash()
		{
			RouteParser parser;
			string[] segments;

			parser = new RouteParser();
			segments = parser.Split("/root/API");
			Assert.AreEqual(2, segments.Length);
			Assert.AreEqual("root", segments[0]);
			Assert.AreEqual("API", segments[1]);
		}







		[TestMethod]
		public void ShouldNotGetPatternForEmptyURL()
		{
			RouteParser parser;

			parser = new RouteParser();
			Assert.ThrowsException<ArgumentNullException>(() => parser.GetPattern(null));
			Assert.ThrowsException<ArgumentNullException>(() => parser.GetPattern(""));
		}

		[TestMethod]
		public void ShouldGetPatternForRootURL()
		{
			RouteParser parser;
			string result;

			parser = new RouteParser();
			result = parser.GetPattern("root");
			Assert.AreEqual("^/root$", result);
		}

		[TestMethod]
		public void ShouldGetPatternForShortStaticURL()
		{
			RouteParser parser;
			string result;

			parser = new RouteParser();
			result = parser.GetPattern("root/API");
			Assert.AreEqual("^/root/API$", result);
		}


		[TestMethod]
		public void ShouldGetPatternForMediumStaticURL()
		{
			RouteParser parser;
			string result;

			parser = new RouteParser();
			result = parser.GetPattern("root/API/Books/1");
			Assert.AreEqual("^/root/API/Books/1$", result);
		}

		[TestMethod]
		public void ShouldGetPatternForURLStartingWithSlash()
		{
			RouteParser parser;
			string result;

			parser = new RouteParser();
			result = parser.GetPattern("/root/API");
			Assert.AreEqual("^/root/API$", result);
		}


		[TestMethod]
		public void ShouldGetPatternForStaticURLWithSpecialChars()
		{
			RouteParser parser;
			string result;

			parser = new RouteParser();
			result = parser.GetPattern("root/API/Books.Test/1");
			Assert.AreEqual(@"^/root/API/Books\.Test/1$", result);
			result = parser.GetPattern("root/API/Books[Test]/1");
			Assert.AreEqual(@"^/root/API/Books\[Test]/1$", result);
		}

		[TestMethod]
		public void ShouldGetPatternForURLWithVariable()
		{
			RouteParser parser;
			string result;

			parser = new RouteParser();
			result = parser.GetPattern("root/API/Books/{id}");
			Assert.AreEqual("^/root/API/Books/(?<id>[^/]+)$", result);
		}

		[TestMethod]
		public void ShouldGetPatternForURLWithVariableBis()
		{
			RouteParser parser;
			string result;
			parser = new RouteParser();
			result = parser.GetPattern("root/API/Books/{id}/Name");
			Assert.AreEqual("^/root/API/Books/(?<id>[^/]+)/Name$", result);

		}

		
	}
}
