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
		public Int64 Id { get; set; }

		[JsonIgnore]
		public string Full
		{
			get { return (string)AdditionalData["media_details"]["sizes"]["full"]["source_url"]; }
		}

		[JsonExtensionData]
		public IDictionary<string, JToken> AdditionalData { get; private set; }
	}
}
