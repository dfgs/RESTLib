using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RESTLib.Server
{
	//application/xml
	public class ResponseSerializer:IResponseSerializer
	{
		public Response Serialize(object Body)
		{
			XmlSerializer serializer;
			MemoryStream stream;
			byte[] buffer;

			if (Body == null) return Response.NoContent;
			serializer = new XmlSerializer(Body.GetType());
			stream = new MemoryStream();
			serializer.Serialize(stream, Body);
			buffer=stream.ToArray();

			return Response.OK(Encoding.UTF8.GetString(buffer), "application/xml");
		}


	}
}
