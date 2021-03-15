using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public class Response
	{
		public static Response NotFound = new Response(ResponseCodes.NotFound, "Not found", "text/html");
		public static Response BadRequest = new Response(ResponseCodes.BadRequest, "Bad request", "text/html");
		public static Response InternalError = new Response(ResponseCodes.InternalError, "Internal error", "text/html");
		public static Response NoContent = new Response(ResponseCodes.NoContent, "", "application/xml");
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

		public string ContentType
		{
			get;
			private set;
		}

		public Encoding Encoding
		{
			get;
			private set;
		}

		public Response(ResponseCodes ResponseCode,string Body,string ContentType)
		{
			//if (Body == null) throw new ArgumentNullException(nameof(Body));
			this.ResponseCode = ResponseCode;this.Body = Body;this.ContentType = ContentType;
			Encoding = Encoding.UTF8;
		}
		public static Response OK(string Body,string ContentType)
		{
			return new Response(ResponseCodes.OK, Body,ContentType);
		}
		

	}
}
