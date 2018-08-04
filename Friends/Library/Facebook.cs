using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Friends.Library
{
	public class Facebook
	{
		public delegate String OnSecondAuthCodeRequest();
		public delegate Boolean OnBrowserSelectRequest();

		public OnBrowserSelectRequest onBrowserSelectRequest;
		public OnSecondAuthCodeRequest onSecondAuthCodeRequest;

		private readonly IWebDriver _driver;
		private readonly Boolean _verbose;

		public User User;

		public Facebook(IWebDriver driver, Boolean verbose = false)
		{
			_driver = driver;
			_verbose = verbose;

			Print("Facebook Library Object Created");
			PrintSeparator();
		}

		public Boolean Login(String userID, String userPass)
		{
			if (IsLoggedIn())
			{
				Print("Already Logged In, Aborting");
				return false;
			}

			Print("Trying Login to Facebook with Given Credentials");
			Print("UserID : " + userID, 1);
			Print("UserPW : " + Regex.Replace(userPass, ".", "*"), 1);
			PrintSeparator();


			Print("Loading Facebook Login Page");
			_driver.Url = "https://www.facebook.com/login.php";
			_driver.Navigate();
			Print("Loaded Facebook Login Page");
			PrintSeparator();


			Print("Finding Login Form");
			IWebElement loginForm = _driver.FindElement(By.Id("login_form"));
			Print("Found Login Form");
			Print("Filling UserID");
			loginForm.FindElement(By.Name("email"))
				.SendKeys(userID);
			Print("Filled UserID");
			Print("Filling UserPW");
			loginForm.FindElement(By.Name("pass"))
				.SendKeys(userPass);
			Print("Filled UserPW");
			Print("Clicking Login Button");
			loginForm.FindElement(By.Id("loginbutton"))
				.Click();
			Print("Clicked Login Button");
			PrintSeparator();


			Print("Checking if Login Failled");
			if (_driver.Url.Contains("https://www.facebook.com/login.php"))
			{
				Print("Login Failed");
				return false;
			}
			Print("Login Not Failled");
			PrintSeparator();


			Print("Checking Existence of Checkpoint");
			while (_driver.Url.Equals("https://www.facebook.com/checkpoint/?next"))
			{
				Print("Redirected To " + _driver.Url);
				PrintSeparator(1);

				Print("Checkpoint Found", 1);
				Print("Handling Checkpoint", 1);
				if (!HandleCheckPoint())
				{
					Print("Failed to Handle Checkpoint", 1);
					return false;
				}
				Print("Successfully Handled Checkpoint", 1);
				PrintSeparator(1);
			}
			Print("Redirected To " + _driver.Url);
			Print("Checked Checkpoints");
			PrintSeparator();


			Print("Getting User Info");
			Print("Loading Profile Page");
			_driver.Url = "https://www.facebook.com/profile.php";
			_driver.Navigate();
			Print("Loaded Profile Page");
			PrintSeparator();


			Print("Finding User UID");
			String id = _driver.FindElement(By.CssSelector("meta[property=\"al:android:url\"]"))
				.GetAttribute("content")
				.Substring("fb://profile/".Length);
			Print("Found User UID");
			Print("UserUID : " + id, 1);
			PrintSeparator();


			Print("Saving User");
			User = new User(id, "Me");
			PrintSeparator();


			Print("Login Success");
			Print("Returning True");
			PrintSeparator();
			return true;
		}


		public void Logout()
		{
			Print("Loading Facebook Main Page");
			_driver.Url = "https://m.facebook.com/";
			_driver.Navigate();
			Print("Loaded Facebook Main Page");
			PrintSeparator();


			Print("Loading Facebook Setting Page (for Logout)");
			Print("Finding Settings Menu");
			_driver.FindElement(By.Id("bookmarks_jewel"))
					.Click();
			Print("Clicked Settings Menu");
			PrintSeparator();


			Print("Redirected To " + _driver.Url);
			PrintSeparator();


			Print("Finding Logout URL");
			Regex regex = new Regex("\"logoutURL\":\"\\\\(.*?)\",", RegexOptions.Multiline);
			Match match = regex.Match(_driver.PageSource);
			String logoutURL = null;
			if (match.Success)
			{
				logoutURL = match.Groups[1].Value;
				if (logoutURL[0] == '/')
				{
					Uri uri = new Uri(_driver.Url);
					logoutURL = uri.Scheme + "://" + uri.Host + logoutURL;
					Print("Found Logout URL : " + logoutURL);
				}
			}
			else
			{
				Print("Failed to Find Logout URL");
				Print("Returning");
				return;
			}
			PrintSeparator();

			Print("Loading Logout Page");
			_driver.Url = logoutURL;
			_driver.Navigate();
			Print("Redirected To " + _driver.Url);
			Print("Logged Out");
			PrintSeparator();

			Print("Closing IWebDriver Windows");
			_driver.Quit();
		}

		public List<User> GetFriendsList()
		{
			return GetFriendsList(User.ID);
		}

		public List<User> GetFriendsList(User tuser)
		{
			return GetFriendsList(tuser.ID);
		}

		public List<User> GetFriendsList(String id)
		{
			Print("Checking Login");
			if (!IsLoggedIn())
			{
				Print("Not Logged In");
				Print("Throwing Error");
				return new List<User>();
			}
			Print("Logged in with UID : " + User.ID);
			PrintSeparator();


			Print("Loading Facebook My Friend List Page");
			_driver.Url = "https://www.facebook.com/profile.php?sk=friends&id=" + id;
			_driver.Navigate();
			Print("Loaded Facebook My Friend List Page");
			PrintSeparator();


			Print("Loading All Lists By Scrolling");
			Print("Got JavascriptExecutor from IWebDriver");
			IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
			long scroll = 1000;
			PrintSeparator();

			Print("Checking Pager Existence");
			while (_driver.FindElements(By.Id("pagelet_timeline_medley_movies")).Count == 0)
			{
				Print("Pager Found, Scrolling");
				Print("Scrolling to Y=" + scroll);
				jse.ExecuteScript("scrollTo(0, " + scroll + ");");
				scroll += 1000;
			}
			Print("No Pager Found");
			PrintSeparator();


			Print("Getting All Friend List");
			List<IWebElement> list = new List<IWebElement>(_driver.FindElements(By.CssSelector("div[data-testid=\"friend_list_item\"]")));_driver.FindElements(By.CssSelector("div[data-testid=\"friend_list_item\"]"));
			Print("Found " + list.Count + " Friends");
			List<User> userList = new List<User>(list.Count + 1);
			PrintSeparator();
			ParseFriend(userList, list);

			return userList;
		}

		public List<User> GetMutualFriendList(User tuser)
		{
			return GetMutualFriendList(tuser.ID);
		}

		public List<User> GetMutualFriendList(String id)
		{
			Print("Checking If Given User has Disabled his Account");
			if (id.Equals("-1"))
			{
				Print("Given User has Disabled his Account");
				Print("Returning Empty List");
				PrintSeparator();
				return new List<User>();
			}

			Print("Loading Mutual Friends Page");
			_driver.Url = "https://www.facebook.com/browse/mutual_friends/?uid=" + id;
			_driver.Navigate();
			Print("Loaded Mutual Friends Page");
			PrintSeparator();


			Print("Loading All Lists By Scrolling");
			Print("Got JavascriptExecutor from IWebDriver");
			IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
			long scroll = 1000;
			PrintSeparator();

			Print("Checking Pager Existence");
			while (_driver.FindElements(By.ClassName("morePager")).Count != 0)
			{
				Print("Pager Found, Scrolling");
				Print("Scrolling to Y=" + scroll);
				jse.ExecuteScript("scrollTo(0, " + scroll + ");");
				scroll += 1000;
			}
			Print("No Pager Found");
			PrintSeparator();


			Print("Getting Mutual Friend List");
			List<IWebElement> list = new List<IWebElement>(_driver.FindElements(By.ClassName("fbProfileBrowserListItem")));_driver.FindElements(By.ClassName("fbProfileBrowserListItem"));
			Print("Found " + list.Count + " Mutual Friends");
			List<User> userList = new List<User>(list.Count + 1);
			ParseFriend(userList, list);

			return userList;
		}

		public int GetFriendCount()
		{
			return GetFriendCount(User);
		}

		public int GetFriendCount(User tuser)
		{
			return GetFriendCount(tuser.ID);
		}

		public int GetFriendCount(String id)
		{
			return GetFriendsList(id).Count;
		}

		private void ParseFriend(List<User> userList, List<IWebElement> list)
		{
			foreach (IWebElement element in list)
			{
				Print("Get UserID From a Tag JSON Data", 1);
				IWebElement userInfo =
						element.FindElement(By.ClassName("uiProfileBlockContent"))
								.FindElement(By.TagName("a"));

				Print("Get JSON", 1);
				String jsonString = userInfo.GetAttribute("data-gt");
				if (jsonString == null)
				{
					User tuser = new User("-1")
					{
						Name = element.FindElement(By.TagName("img")).GetAttribute("aria-label")
					};
					userList.Add(tuser);

					Print("User '" + tuser.Name + "' has Disabled His or Her Account", 1);
					Print("Setting his UID to -1 and Will Be Not Displayed in Graph", 1);
					PrintSeparator(1);
					continue;
				}

				jsonString = HttpUtility.UrlDecode(jsonString, Encoding.UTF8);
				Print("Decoded JSON", 1);
				PrintSeparator(1);
				jsonString = jsonString.Replace("&quot;", "\"")
						.Replace("&amp;", "&");

				JObject json = JObject.Parse(jsonString);
				String friendID = json["engagement"]["eng_tid"].ToString();
				String friendName = userInfo.Text;
				User user = new User(friendID, friendName);
				userList.Add(user);

				Print("User Parsed", 1);
				Print("UID  : " + user.Name, 2);
				Print("Name : " + user.ID, 2);
				PrintSeparator(1);
			}
		}

		private Boolean HandleCheckPoint()
		{
			Print("Checking Checkpoint Type", 1);
			if (_driver.FindElements(By.Id("approvals_code")).Count != 0)
			{
				Print("Checkpoint Type : 2ndAuthRequest", 1);
				PrintSeparator(1);
				return Handle2ndAuth();
			}
			else if (_driver.FindElements(By.CssSelector("input[type=\"radio\"][value=\"save_device\"]")).Count != 0)
			{
				Print("Checkpoint Type : SaveDevice", 1);
				PrintSeparator(1);
				HandleSaveDevice();
				return true;
			}
			else
			{
				Print("Checkpoint Type : Unknown", 1);
				PrintSeparator(1);
				Print("Unknown Checkpoint Passed", 1);
				_driver.FindElement(By.Id("checkpointSubmitButton"))
						.Click();
				return true;
			}
		}

		private Boolean Handle2ndAuth()
		{
			Boolean first = true;

			do
			{
				if (!first)
				{
					Print("Wrong Authentication Code!", 1);
				}
				first = false;

				Print("Calling Callback For 2ndAuthCode", 1);
				String code = onSecondAuthCodeRequest();
				Print("Trying 2nd Authentication with Credentials", 1);
				Print("AuthCode : " + code, 2);
				PrintSeparator(1);


				Print("Checking If 2nd Authentication Passed By Other Methods (ex. Phone)", 1);
				if (_driver.FindElements(By.Id("approvals_code")).Count == 0)
				{
					Print("2nd Authentication Passed By Other Methods", 1);
					PrintSeparator(1);
					return true;
				}
				Print("2nd Authentication Not Passed By Other Methods", 1);
				PrintSeparator(1);


				Print("Filling AuthCode", 1);
				_driver.FindElement(By.Id("approvals_code"))
						.SendKeys(code);
				Print("Clicking Submit Button", 1);
				_driver.FindElement(By.Id("checkpointSubmitButton"))
						.Click();
				PrintSeparator(1);


				Print("Redirected To " + _driver.Url, 1);
				PrintSeparator(1);


				Print("Checking Checkpoints", 1);
				if (!_driver.Url.Equals("https://www.facebook.com/checkpoint/?next"))
				{
					Print("Finished All Checkpoints", 1);
					return true;
				}
				Print("Checkpoints Left", 1);
				PrintSeparator(1);

			} while (_driver.FindElements(By.Id("approvals_code")).Count != 0);
			Print("2nd Authentication Successfully Finished", 1);

			return true;
		}

		private void HandleSaveDevice()
		{
			Print("Calling Callback For Save Browser", 1);
			if (onBrowserSelectRequest())
			{
				Print("Save Browser Callback Returned True", 1);
				Print("Clicking Option \"Save Device\"", 1);
				_driver.FindElement(By.CssSelector("input[type=\"radio\"][value=\"save_device\"]"))
						.Click();
			}
			else
			{
				Print("Save Browser Callback Returned False", 1);
				Print("Clicking Option \"Don't Save Device\"", 1);
				_driver.FindElement(By.CssSelector("input[type=\"radio\"][value=\"dont_save\"]"))
						.Click();
			}
			Print("Clicking Submit Button", 1);
			_driver.FindElement(By.Id("checkpointSubmitButton"))
					.Click();
			Print("Save Device Checkpoint Successfully Finished", 1);
		}


		public Boolean IsLoggedIn()
		{
			Print("Checking User Login (By Redirection)");
			PrintSeparator();
			_driver.Url = "https://www.facebook.com/login.php";
			_driver.Navigate();
			return !_driver.Url.Equals("https://www.facebook.com/login.php");
		}

		private void Print()
		{
			if (_verbose)
			{
				Console.Out.WriteLine();
			}
		}

		private void Print(String str, int indent = 0)
		{
			if (_verbose)
			{
				StringBuilder s = new StringBuilder();
				for (int i = 0; i < indent; ++i)
				{
					s.Append("\t");
				}
				s.Append(str);
				Console.Out.WriteLine(s.ToString());
			}
		}

		private void PrintSeparator(int indent = 0)
		{
			PrintSeparator(indent, 80 - indent * 4);
		}

		private void PrintSeparator(int indent, int count)
		{
			if (_verbose)
			{
				StringBuilder s = new StringBuilder();
				for (int i = 0; i < indent; ++i)
				{
					s.Append("\t");
				}

				for (int i = 0; i < count; ++i)
				{
					s.Append("=");
				}

				Print(s.ToString());
			}
		}
	}
}
