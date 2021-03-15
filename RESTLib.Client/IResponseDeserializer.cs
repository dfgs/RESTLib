using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Client
{
	public interface IResponseDeserializer
	{
		T Deserialize<T>(Stream Stream);
	}
}
