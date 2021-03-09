﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public interface IRouteParser
	{
		RouteSegment[] Parse(string URL);
	}
}