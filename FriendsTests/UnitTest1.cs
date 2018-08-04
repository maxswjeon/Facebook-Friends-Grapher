using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Friends.Library;

namespace FriendsTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			SimpleServer server = new SimpleServer();
			server.Run();
		}
	}
}
