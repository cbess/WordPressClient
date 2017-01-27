using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WordPressClient
{
	[JsonObject]
	public class Media
	{
		[JsonProperty("id")]
		public Int64 Id { get; private set; }

		[JsonProperty("source_url")]
		public string SourceUrl { get; private set; }

		/// <summary>
		/// Gets the full size image URL path.
		/// </summary>
		/// <value>The full size URL path.</value>
		[JsonIgnore]
		public string OriginalUrl
		{
			get { return (string)AdditionalData["media_details"]["sizes"]["full"]["source_url"]; }
		}

		[JsonExtensionData]
		public IDictionary<string, JToken> AdditionalData { get; private set; }
	}
}
