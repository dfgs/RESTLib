using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	public class Book
	{
		public int Id
		{
			get;
			set;
		}
		public int Year
		{
			get;
			set;
		}
		public string Title
		{
			get;
			set;
		}

		public string Author
		{
			get;
			set;
		}

		public override string ToString()
		{
			return $"Id={Id}, Year={Year}, Title={Title}, Author={Author}";
		}


	}
}
