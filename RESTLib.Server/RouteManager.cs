using RESTLib.Server.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	// http://api.example.com/device-management/managed-devices?region=USA&brand=XYZ&sort=installation-date

	public class RouteManager : IRouteManager
	{
		private IRouteParser routeParser;
		private Dictionary<string,RouteBinding> getRouteDictionary;
		private Dictionary<string, RouteBinding> putRouteDictionary;
		private Dictionary<string, RouteBinding> postRouteDictionary;
		private Dictionary<string, RouteBinding> deleteRouteDictionary;
		private IResponseSerializer serializer;

		public RouteManager(IRouteParser RouteParser, IResponseSerializer Serializer)
		{
			if (RouteParser == null) throw new ArgumentNullException(nameof(RouteParser));
			if (Serializer == null) throw new ArgumentNullException(nameof(Serializer));
			this.routeParser = RouteParser;
			this.serializer = Serializer;
			getRouteDictionary = new Dictionary<string, RouteBinding>();
			putRouteDictionary = new Dictionary<string, RouteBinding>();
			postRouteDictionary = new Dictionary<string, RouteBinding>();
			deleteRouteDictionary = new Dictionary<string, RouteBinding>();
		}

		public void AddRouteHandler(IRouteHandler RouteHandler)
		{
			Type type;
			MethodInfo[] mis;
			RouteAttribute attribute;
			

			if (RouteHandler == null) throw new ArgumentNullException(nameof(RouteHandler));

			type = RouteHandler.GetType();
			mis = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
			foreach (MethodInfo mi in mis)
			{
				attribute = mi.GetCustomAttribute<RouteAttribute>();
				if (attribute == null) continue;
				BindRoute(RouteHandler, mi,attribute.Method, routeParser.GetPattern(attribute.URL));
			}

		}

		private Dictionary<string, RouteBinding> GetMethodDictionary(RESTMethods Method)
		{
			switch (Method)
			{
				case RESTMethods.GET: return getRouteDictionary;
				case RESTMethods.PUT: return putRouteDictionary;
				case RESTMethods.POST: return postRouteDictionary;
				case RESTMethods.DELETE: return deleteRouteDictionary;
				default: throw new InvalidParameterException($"REST Method {Method} not supported");
			}
		}

		public RouteBinding BindRoute(IRouteHandler RouteHandler, MethodInfo MethodInfo,RESTMethods Method, string Pattern)
		{
			Dictionary<string, RouteBinding> methodDictionary;
			ParameterInfo[] pis;
			ParameterInfo pi;
			RouteBinding binding;
			Regex regex;
			string[] variables;

			if (RouteHandler == null) throw new ArgumentNullException(nameof(RouteHandler));
			if (MethodInfo == null) throw new ArgumentNullException(nameof(MethodInfo));
			if (string.IsNullOrEmpty(Pattern)) throw new ArgumentNullException(nameof(Pattern));

			pis = MethodInfo.GetParameters();

			methodDictionary = GetMethodDictionary(Method);
			if (methodDictionary.ContainsKey(Pattern)) throw new DuplicateRouteException(MethodInfo.Name);

			regex = new Regex(Pattern);
			variables = regex.GetGroupNames().Skip(1).ToArray();

			if (variables.Count() != pis.Length) throw new InvalidRouteException(MethodInfo.Name);
			foreach(string variable in variables)
			{
				pi = pis.FirstOrDefault(item => item.Name == variable);
				if (pi == null) throw new InvalidRouteException(MethodInfo.Name);
			}
			

			binding = new RouteBinding() { MethodInfo=MethodInfo, RouteHandler=RouteHandler };

			methodDictionary.Add(Pattern, binding);


			return binding;
		}
		public Route GetRoute(RESTMethods Method, string URL)
		{
			Dictionary<string, RouteBinding> methodDictionary;
			Route route;
			Match match;

			if (string.IsNullOrEmpty(URL)) throw new ArgumentNullException(nameof(URL));

			methodDictionary = GetMethodDictionary(Method);

			foreach(KeyValuePair<string,RouteBinding> keyValuePair in methodDictionary)
			{
				match = Regex.Match(URL, keyValuePair.Key);
				if (!match.Success) continue;
				
				route = new Route() { Binding = keyValuePair.Value };
				foreach(Group group in match.Groups)
				{
					route.Set(group.Name, group.Value);
				}

				return route;
			}
			throw new RouteNotFoundException(URL);
		}


		public Response GetResponse(RESTMethods Method,string URL)
		{
			Route route;
			List<object> parameters;
			object result;

			if (string.IsNullOrEmpty(URL)) throw new ArgumentNullException(nameof(URL));

			route = GetRoute(Method, URL);
			parameters = new List<object>();


			foreach (ParameterInfo pi in route.Binding.MethodInfo.GetParameters())
			{
				try
				{
					parameters.Add(Convert.ChangeType(route.Get(pi.Name), pi.ParameterType));
				}
				catch
				{
					throw new InvalidParameterException(URL);
				}
				
			}

			try
			{
				result = route.Binding.MethodInfo.Invoke(route.Binding.RouteHandler, parameters.ToArray());
				if (result is Response response) return response;

				return serializer.Serialize(result);
			}
			catch
			{
				return Response.InternalError;
			}

						

	
		}


	}
}
