using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WordPressClient
{
	public class WordPressClient
	{
		public HttpClient Client { get; private set; }
		public Uri SiteUri { get; set; }

		public WordPressClient(String siteUrl) : this(new Uri(siteUrl))
		{
			// empty
		}

		public WordPressClient(Uri siteUri)
		{
			SiteUri = siteUri;
			Client = new HttpClient();
			Client.DefaultRequestHeaders.Accept.Clear();
			Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			Client.MaxResponseContentBufferSize = 256000;
		}

		/// <summary>
		/// Gets the blog posts.
		/// </summary>
		/// <returns>The posts.</returns>
		public async Task<List<Post>> GetPosts(int page = 1, int pageSize = 12)
		{
			var builder = new UriBuilder(SiteUri);
			builder.Path = "/wp-json/wp/v2/posts";

			return await GetObjectAsync<List<Post>>(builder.Uri);
		}

		/// <summary>
		/// Gets the media info for the specified Post.
		/// </summary>
		/// <returns>The media information.</returns>
		/// <param name="post">Post</param>
		public async Task<Media> GetMedia(Post post)
		{
			return await GetObjectAsync<Media>(new Uri(post.FeaturedMediaUrl));
		}

		private async Task<T> GetObjectAsync<T>(Uri uri)
		{
			try
			{
				var response = await Client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<T>(content);
				}
			}
			catch (WebException ex)
			{
				Debug.WriteLine("Error: " + ex.Message);
			}

			return default(T);
		}
	}
}
