﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	[AttributeUsage(AttributeTargets.Method)]
	public class RouteAttribute:Attribute
	{
		public string URL
		{
			get;
			set;
		}

		public RouteAttribute(string URL)
		{
			this.URL = URL;
		}

	}
}
