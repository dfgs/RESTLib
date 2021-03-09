using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class Response
	{
		public static Response NotFound = new Response(ResponseCodes.NotFound, "Not found" );
		public static Response InternalError = new Response(ResponseCodes.InternalError, "Internal error" );
		public ResponseCodes ResponseCode
		{
			get;
			private set;
		}
		public string Body
		{
			get;
			private set;
		}

		public Response(ResponseCodes ResponseCode,string Body)
		{
			if (Body == null) throw new ArgumentNullException(nameof(Body));
			this.ResponseCode = ResponseCode;this.Body = Body;
		}
		public static Response OK(string Body)
		{
			return new Response( ResponseCodes.OK, Body);
		}
		

	}
}
