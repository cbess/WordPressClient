using System;

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
			var client = new WordPressClient.WordPressClient("http://redgracemedia.com");
			var task = client.GetPosts();
			var posts = await task;
			var count = posts.Count;
		}
	}
}
