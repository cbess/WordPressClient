using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
		public async Task<List<Post>> GetPosts()
		{
			var builder = new UriBuilder(SiteUri);
			builder.Path = "/wp-json/wp/v2/posts";

			var response = await Client.GetAsync(builder.Uri);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<List<Post>>(content);
			}

			return null;
		}
	}
}
