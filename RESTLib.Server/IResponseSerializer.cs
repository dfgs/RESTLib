using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLib.Server
{
	public interface IResponseSerializer
	{
		Response Serialize(object Body);
	}
}
