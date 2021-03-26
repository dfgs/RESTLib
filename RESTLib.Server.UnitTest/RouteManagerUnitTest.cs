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
		public void ShouldFailToBindRouteIfParametersAreEmpty()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.BindRoute(null, MethodInfos.GetBook, RESTMethods.GET, "/root"));
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.BindRoute(new BooksRouteHandler(), null, RESTMethods.GET, "/root"));
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.BindRoute(new BooksRouteHandler(),MethodInfos.GetBook, RESTMethods.GET, null));
		}
		[TestMethod]
		public void ShouldBindRoute()
		{
			RouteManager routeManager;
			RouteBinding result;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			result=routeManager.BindRoute(new BooksRouteHandler(), MethodInfos.GetBooks, RESTMethods.GET,"/root/API/Books");
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void ShouldBindRouteWithVariableSegment()
		{
			RouteManager routeManager;
			RouteBinding result;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			result = routeManager.BindRoute(new BooksRouteHandler(), MethodInfos.GetBook, RESTMethods.GET, "/root/API/Books/(?<Id>[^/]+)");
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void ShouldFailToBindRouteWithVariableSegmentWhenVariableNameIsNotMapped()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			Assert.ThrowsException<InvalidRouteException>(()=> routeManager.BindRoute(new BooksRouteHandler(), MethodInfos.GetBook, RESTMethods.GET, "/root/API/Books/(?<Test>[^/]+)"));
		}

		[TestMethod]
		public void ShouldFailToBindRouteWithVariableSegmentWhenVariableCountDoesntMatch()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			Assert.ThrowsException<InvalidRouteException>(() => routeManager.BindRoute(new BooksRouteHandler(), MethodInfos.GetBook, RESTMethods.GET, "/root/API/Books"));
			Assert.ThrowsException<InvalidRouteException>(() => routeManager.BindRoute(new BooksRouteHandler(), MethodInfos.GetBook, RESTMethods.GET, "/root/API/Books/(?<Id1>[^/]+)/(?<Id2>[^/]+)"));
		}

		[TestMethod]
		public void ShouldNotCreateDuplicateRoute()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.BindRoute(new BooksRouteHandler(), MethodInfos.GetBook, RESTMethods.GET, "/root/API/Books/(?<Id>[^/]+)");
			Assert.ThrowsException<DuplicateRouteException>(()=> routeManager.BindRoute(new BooksRouteHandler(), MethodInfos.GetBook, RESTMethods.GET, "/root/API/Books/(?<Id>[^/]+)") );
		}

		[TestMethod]
		public void ShouldCreateDuplicateRouteWithDifferentMethods()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.BindRoute(new BooksRouteHandler(), MethodInfos.GetBook, RESTMethods.GET, "/root/API/Books/(?<Id>[^/]+)");
			routeManager.BindRoute(new BooksRouteHandler(), MethodInfos.GetBook, RESTMethods.PUT, "/root/API/Books/(?<Id>[^/]+)");
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
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.GetRoute(RESTMethods.GET, null));
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.GetRoute(RESTMethods.GET, ""));
		}
		[TestMethod]
		public void ShouldGetRoute()
		{
			RouteManager routeManager;
			Route route;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			route = routeManager.GetRoute(RESTMethods.GET, "/root/API/Authors/5");
			Assert.IsNotNull(route);
			Assert.IsNotNull(route.Binding);
			Assert.IsNotNull(route.Binding.RouteHandler);
			Assert.IsNotNull(route.Binding.MethodInfo);

			Assert.AreEqual("5", route.Get("Id"));
		}
		[TestMethod]
		public void ShouldGetRouteWithFilters()
		{
			RouteManager routeManager;
			Route route;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			route = routeManager.GetRoute(RESTMethods.GET, "/root/API/books?year=1980&author=moliere");
			Assert.IsNotNull(route);
			Assert.IsNotNull(route.Binding);
			Assert.IsNotNull(route.Binding.RouteHandler);
			Assert.IsNotNull(route.Binding.MethodInfo);

			Assert.AreEqual("1980", route.Get("Year"));
			Assert.AreEqual("moliere", route.Get("Author"));
		}
		[TestMethod]
		public void ShouldGetRouteWithEncodedParameters()
		{
			RouteManager routeManager;
			Route route;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			route = routeManager.GetRoute(RESTMethods.GET, "/root/API/books?year=1980&author=to%20to");
			Assert.IsNotNull(route);
			Assert.IsNotNull(route.Binding);
			Assert.IsNotNull(route.Binding.RouteHandler);
			Assert.IsNotNull(route.Binding.MethodInfo);

			Assert.AreEqual("1980", route.Get("Year"));
			Assert.AreEqual("to to", route.Get("Author"));
		}
		[TestMethod]
		public void ShouldNotGetRouteIfSegmentIsNotFound()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			Assert.ThrowsException<RouteNotFoundException>(()=> routeManager.GetRoute(RESTMethods.GET, "/root/API/Authors/5/Test"));
		}

		[TestMethod]
		public void ShouldNotGetRouteIfMethodInfoIsNotFound()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			Assert.ThrowsException<RouteNotFoundException>(() => routeManager.GetRoute(RESTMethods.GET, "/root/API/Authors"));
		}
		[TestMethod]
		public void ShouldNotGetRouteIfMethodsAreDifferent()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			Assert.ThrowsException<RouteNotFoundException>(() => routeManager.GetRoute(RESTMethods.PUT, "/root/API/Authors"));

		}



		[TestMethod]
		public void ShouldGetResponseWithParameter()
		{
			RouteManager routeManager;
			Response response;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			response=routeManager.GetResponse(RESTMethods.GET, "/root/API/Books/5");
			Assert.IsNotNull(response);
			Assert.IsTrue(response.Body.Contains("5"));
		}

		[TestMethod]
		public void ShouldGetResponseWithNullContent()
		{
			RouteManager routeManager;
			Response response;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			response = routeManager.GetResponse(RESTMethods.GET, "/root/API/GetNull");
			Assert.IsNotNull(response);
			Assert.AreEqual("",response.Body) ;
		}
		[TestMethod]
		public void ShouldGetCustomResponse()
		{
			RouteManager routeManager;
			Response response;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			response = routeManager.GetResponse(RESTMethods.GET, "/root/API/GetCustomResponse");
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
			response = routeManager.GetResponse(RESTMethods.GET, "/root/API/Status");
			Assert.IsNotNull(response);
			Assert.IsTrue(response.Body.Contains("true"));
		}

		[TestMethod]
		public void ShouldNotGetResponseIfURLIsEmpty()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.GetResponse(RESTMethods.GET, null));
			Assert.ThrowsException<ArgumentNullException>(() => routeManager.GetResponse(RESTMethods.GET, ""));
		}
		[TestMethod]
		public void ShouldNotGetResponseIfParameterIsWrongType()
		{
			RouteManager routeManager;

			routeManager = new RouteManager(new RouteParser(), new ResponseSerializer());
			routeManager.AddRouteHandler(new BooksRouteHandler());
			Assert.ThrowsException<InvalidParameterException>(() => routeManager.GetResponse(RESTMethods.GET, "/root/API/Books/t"));
			
		}



	}
}
