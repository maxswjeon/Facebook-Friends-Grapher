using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Library
{
	public class User
	{
		public readonly String ID;
		public String Name;

		public User(String id, String name = null)
		{
			ID = id;
			Name = name;
		}
	}
}
