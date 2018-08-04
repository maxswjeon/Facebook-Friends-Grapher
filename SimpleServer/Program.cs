using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleServer
{
	class Program
	{
		static void Main(string[] args)
		{
			SimpleServer server = new SimpleServer();
			server.Run();
			Thread.Sleep(1000 * 60 * 60);
		}
	}
}
