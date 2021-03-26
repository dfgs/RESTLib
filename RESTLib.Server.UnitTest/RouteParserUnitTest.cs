using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RESTLib.Server.UnitTest
{
	[TestClass]
	public class RouteParserUnitTest
	{
		



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
			result = parser.GetPattern("/root");
			Assert.AreEqual("^/root$", result);
		}

		[TestMethod]
		public void ShouldGetPatternForShortStaticURL()
		{
			RouteParser parser;
			string result;

			parser = new RouteParser();
			result = parser.GetPattern("/root/API");
			Assert.AreEqual("^/root/API$", result);
		}


		[TestMethod]
		public void ShouldGetPatternForMediumStaticURL()
		{
			RouteParser parser;
			string result;

			parser = new RouteParser();
			result = parser.GetPattern("/root/API/Books/1");
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
			result = parser.GetPattern("/root/API/Books.Test/1");
			Assert.AreEqual(@"^/root/API/Books\.Test/1$", result);
			result = parser.GetPattern("/root/API/Books?author={authorID}&year={yearNumber}");
			Assert.AreEqual(@"^/root/API/Books\?author=(?<authorID>[^/&]+)&year=(?<yearNumber>[^/&]+)$", result);
		}

		[TestMethod]
		public void ShouldGetPatternForURLWithVariable()
		{
			RouteParser parser;
			string result;

			parser = new RouteParser();
			result = parser.GetPattern("/root/API/Books/{id}");
			Assert.AreEqual("^/root/API/Books/(?<id>[^/&]+)$", result);
		}

		[TestMethod]
		public void ShouldGetPatternForURLWithVariableBis()
		{
			RouteParser parser;
			string result;
			parser = new RouteParser();
			result = parser.GetPattern("/root/API/Books/{id}/Name");
			Assert.AreEqual("^/root/API/Books/(?<id>[^/&]+)/Name$", result);

		}
		[TestMethod]
		public void ShouldGetPatternForURLWithVariableAndFilters()
		{
			RouteParser parser;
			string result;

			parser = new RouteParser();
			result = parser.GetPattern("/root/API/Books/{id}?author={authorID}&year={yearNumber}");
			Assert.AreEqual(@"^/root/API/Books/(?<id>[^/&]+)\?author=(?<authorID>[^/&]+)&year=(?<yearNumber>[^/&]+)$", result);

		}

	}
}
