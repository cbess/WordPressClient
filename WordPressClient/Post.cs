using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WordPressClient
{
	[JsonObject]
	public class Post
	{
		[JsonProperty("id")]
		public Int64 Id { get; set; }

		[JsonProperty("link")]
		public string Link { get; set; }

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

		[JsonExtensionData]
		private IDictionary<string, JToken> AdditionalData { get; set; }

		/// <summary>
		/// Gets the rendered string from the AdditionalData.
		/// </summary>
		/// <returns>The rendered string.</returns>
		/// <param name="field">Field name.</param>
		private string GetRenderedString(string field)
		{
			var excerptData = AdditionalData[field] as IDictionary<string, JToken>;
			return (string)excerptData["rendered"];
		}
	}
}
