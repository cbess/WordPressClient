using System;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WordPressClient
{
	[JsonObject]
	public class Post
	{
		[JsonProperty("id")]
		public Int64 Id { get; private set; }

		[JsonProperty("link")]
		public string Link { get; private set; }

		[JsonProperty("date")]
		public DateTime PublishDate { get; private set; }

		[JsonIgnore]
		public string Title
		{ 
			get { return GetRenderedString("title"); }
		}

		[JsonIgnore]
		public string Html
		{ 
			get { return GetRenderedString("content"); }
		}

		[JsonIgnore]
		public string Excerpt 
		{
			get { return GetRenderedString("excerpt"); }
		}

		[JsonIgnore]
		public string FeaturedMediaUrl
		{
			get 
			{
				try
				{
					return (string)AdditionalData["_links"]["wp:featuredmedia"][0]["href"];
				}
				catch (NullReferenceException ex)
				{
					Debug.WriteLine(ex.Message);
					return null;
				}
			}
		}

		[JsonExtensionData]
		public IDictionary<string, JToken> AdditionalData { get; private set; }

		/// <summary>
		/// Gets the rendered string from the AdditionalData.
		/// </summary>
		/// <returns>The rendered string.</returns>
		/// <param name="field">Field name.</param>
		private string GetRenderedString(string field)
		{
			return (string)AdditionalData[field]["rendered"];
		}
	}
}
