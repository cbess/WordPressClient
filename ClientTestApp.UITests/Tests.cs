﻿using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;
using WordPressClient;

namespace ClientTestApp.UITests
{
	[TestFixture]
	public class Tests
	{
		iOSApp app;

		[SetUp]
		public void BeforeEachTest()
		{
			app = ConfigureApp.iOS.StartApp();
		}

		[Test]
		public void ViewIsDisplayed()
		{
			AppResult[] results = app.WaitForElement(c => c.Child("UIView"));
			app.Screenshot("First screen.");

			Assert.IsTrue(results.Any());
		}

		[Test]
		public async void GetPosts()
		{
			var client = new WordPressClient.WordPressClient("http://demo.wp-api.org");
			var task = client.GetPosts();
			var posts = await task;

			Assert.GreaterOrEqual(posts.Count, 1, "Not enough posts returned by the website.");
		}
	}
}

