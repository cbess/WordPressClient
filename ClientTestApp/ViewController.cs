using System;
using System.Diagnostics;

using UIKit;

namespace ClientTestApp
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			actionButton.TouchUpInside += (sender, e) => actionPressed();
		}

		public async void actionPressed()
		{
			var client = new WordPressClient.WordPressClient("http://demo.wp-api.org");
			var posts = await client.GetPosts();
			var post = posts[0];
			var media = await client.GetMedia(post);

			Debug.WriteLine("Done");
		}
	}
}
