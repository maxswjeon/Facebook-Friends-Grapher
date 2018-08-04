﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleServer
{
	public class SimpleServer
	{
		private HttpListener _listener = new HttpListener();

		public SimpleServer()
		{
			if (!HttpListener.IsSupported)
			{
				throw new NotSupportedException("Needs Windows XP SP2, Server 2003 or later.");
			}

			_listener.Prefixes.Add("http://localhost:30113/src/");
			_listener.Start();
		}

		public void Run()
		{
			ThreadPool.QueueUserWorkItem((o) =>
			{
				while (_listener.IsListening)
				{
					ThreadPool.QueueUserWorkItem((c) =>
					{
						var ctx = c as HttpListenerContext;

						switch (ctx.Request.Url.AbsolutePath)
						{
							case "/src/index.html":
								ctx.Response.ContentType = "text/html";

								StreamReader reader =
									File.OpenText(Path.Combine(Environment.CurrentDirectory, "src/index.html"));

								byte[] buf = Encoding.UTF8.GetBytes(reader.ReadToEnd());
								
								ctx.Response.ContentLength64 = buf.Length;
								ctx.Response.OutputStream.Write(buf, 0, buf.Length);


								break;
							case "/src/result.json":
								ctx.Response.ContentType = "text/json";

								StreamReader reader2 =
									File.OpenText(Path.Combine(Environment.CurrentDirectory, "src/result.json"));

								byte[] buf2 = Encoding.UTF8.GetBytes(reader2.ReadToEnd());
								
								reader2.Close();

								ctx.Response.ContentLength64 = buf2.Length;
								ctx.Response.OutputStream.Write(buf2, 0, buf2.Length);
								break;
							default:
								String res = "<!DOCTYPE html><head></head><body><h1>Forbidden</h1><h3>Access Forbidden for Security</h3></body>";
								byte[] buf3 = Encoding.UTF8.GetBytes(res);
								ctx.Response.ContentLength64 = buf3.Length;
								ctx.Response.OutputStream.Write(buf3, 0, buf3.Length);
								ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden;
								break;
						}
						ctx.Response.OutputStream.Flush();
						ctx.Response.OutputStream.Close();
					}, _listener.GetContext());
				}
			});
		}

		public void Stop()
		{
			_listener.Stop();
			_listener.Close();
		}
	}
}
