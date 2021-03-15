using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RESTLib.Client
{
	//application/xml
	public class ResponseDeserializer:IResponseDeserializer
	{
		public T Deserialize<T>(Stream Stream)
		{
			XmlSerializer serializer;

			if (Stream == null) throw new ArgumentNullException(nameof(Stream));

			serializer = new XmlSerializer(typeof(T));
			return (T)serializer.Deserialize(Stream);
		
		}


	}
}
