using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public enum ResponseCodes:int {OK=200,NoContent=204, BadRequest=400, NotFound = 404,InternalError=500,Custom=999	};

}
