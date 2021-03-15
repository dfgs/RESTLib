using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTLib.Server.Exceptions;
using RESTLib.Server.UnitTest.RouteHandlers;
using System;
using System.Reflection;

namespace RESTLib.Server.UnitTest
{
	[TestClass]
	public class RouteManagerUnitTest
	{

		[TestMethod]
		public void ShouldCheckConstructorParameters()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new RouteManager(null, new ResponseSerializer()));
			Assert.ThrowsException<ArgumentNullException>(() => new RouteManager(new RouteParser(), null));
		}
		[TestMethod]
		public void ShouldFailToCreateRouteIfParametersAreEmpty()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			Assert.ThrowsException<InvalidRouteException>(() => routeManager.CreateRoute(new BooksRouteHandler(), MethodInfos.GetBook, new StaticRouteSegment("1")));
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.CreateRoute(null, MethodInfos.GetBook, new StaticRouteSegment("1")));
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.CreateRoute(new BooksRouteHandler(),MethodInfos.GetBook, null));
		}
		[TestMethod]
		public void ShouldCreateRoute()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.CreateRoute(new BooksRouteHandler(), MethodInfos.GetBook, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"), new VariableRouteSegment("Id"));
		}

		[TestMethod]
		public void ShouldCreateRouteWithVariableSegment()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.CreateRoute(new BooksRouteHandler(), MethodInfos.GetBook, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"), new VariableRouteSegment("Id"));
		}

		[TestMethod]
		public void ShouldFailToCreateRouteWithVariableSegmentWhenVariableNameIsNotMapped()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			Assert.ThrowsException<InvalidRouteException>(()=> routeManager.CreateRoute(new BooksRouteHandler(), MethodInfos.GetBook, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"), new VariableRouteSegment("Test")));
		}

		[TestMethod]
		public void ShouldFailToCreateRouteWithVariableSegmentWhenVariableCountDoesntMatch()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			Assert.ThrowsException<InvalidRouteException>(() => routeManager.CreateRoute(new BooksRouteHandler(), MethodInfos.GetBook, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books")));
			Assert.ThrowsException<InvalidRouteException>(() => routeManager.CreateRoute(new BooksRouteHandler(), MethodInfos.GetBook, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"), new VariableRouteSegment("Id1"), new VariableRouteSegment("Id2")));
		}

		[TestMethod]
		public void ShouldNotCreateDuplicateRoute()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.CreateRoute(new BooksRouteHandler(), MethodInfos.GetBook, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"), new VariableRouteSegment("Id"));
			Assert.ThrowsException<DuplicateRouteException>(()=> routeManager.CreateRoute(new BooksRouteHandler(), MethodInfos.GetBook, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"), new VariableRouteSegment("Id")));
		}

		[TestMethod]
		public void ShouldNoteCreateRouteWithVariableSegment()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.CreateRoute(new BooksRouteHandler(), MethodInfos.GetBook, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"), new VariableRouteSegment("Id"));
			Assert.ThrowsException<DuplicateRouteException>(() => routeManager.CreateRoute(new BooksRouteHandler(), MethodInfos.GetBook, new StaticRouteSegment("root"), new StaticRouteSegment("API"), new StaticRouteSegment("Books"), new VariableRouteSegment("Id")));
		}


		[TestMethod]
		public void ShouldFailsToAddRouteHandlerWhenParameterIsNull()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.AddRouteHandler(null));
		}
		[TestMethod]
		public void ShouldAddRouteHandler()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
		}
		[TestMethod]
		public void ShouldFailsToGetRouteWhenParameterIsNull()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.GetRoute(null));
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.GetRoute(""));
		}
		[TestMethod]
		public void ShouldGetRoute()
		{
			RouteManager routeManager;
			Route route;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			route = routeManager.GetRoute("root/API/Authors/5");
			Assert.IsNotNull(route);
			Assert.IsNotNull(route.RouteHandler);
			Assert.IsNotNull(route.MethodInfo);

			Assert.AreEqual("5", route.Get("Id"));
		}

		[TestMethod]
		public void ShouldNotGetRouteIfSegmentIsNotFound()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			Assert.ThrowsException<RouteNotFoundException>(()=> routeManager.GetRoute("root/API/Authors/5/Test"));
		}

		[TestMethod]
		public void ShouldNotGetRouteIfMethodInfoIsNotFound()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			Assert.ThrowsException<RouteNotFoundException>(() => routeManager.GetRoute("root/API/Authors"));
		}




		[TestMethod]
		public void ShouldGetResponseWithParameter()
		{
			RouteManager routeManager;
			Response response;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			response=routeManager.GetResponse("root/API/Books/5");
			Assert.IsNotNull(response);
			Assert.AreEqual("5",response.Body);
		}

		[TestMethod]
		public void ShouldGetResponseWithNullContent()
		{
			RouteManager routeManager;
			Response response;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			response = routeManager.GetResponse("root/API/GetNull");
			Assert.IsNotNull(response);
			Assert.IsNull(response.Body);
		}
		[TestMethod]
		public void ShouldGetCustomResponse()
		{
			RouteManager routeManager;
			Response response;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			response = routeManager.GetResponse("root/API/GetCustomResponse");
			Assert.IsNotNull(response);
			Assert.AreEqual(ResponseCodes.Custom, response.ResponseCode);
			Assert.AreEqual("Custom", response.Body);
		}

		[TestMethod]
		public void ShouldGetResponseWithoutParameter()
		{
			RouteManager routeManager;
			Response response;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new StatusRouteHandler());
			response = routeManager.GetResponse("root/API/Status");
			Assert.IsNotNull(response);
			Assert.AreEqual("True", response.Body);
		}

		[TestMethod]
		public void ShouldNotGetResponseIfURLIsEmpty()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.GetResponse(null));
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.GetResponse(""));
		}
		[TestMethod]
		public void ShouldNotGetResponseIfParameterIsWrongType()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			Assert.ThrowsException<InvalidParameterException>(() => routeManager.GetResponse("root/API/Books/t"));
			
		}



	}
}
