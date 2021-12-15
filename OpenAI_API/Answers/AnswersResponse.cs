using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenAI_API
{
	public class AnswersResult
	{

		[JsonProperty("answers")]
		public List<string> Answers { get; set; }

		[JsonProperty("completion")]
		public string Completion { get; set; }

		[JsonProperty("model")]
		public string Model { get; set; }

		[JsonProperty("object")]
		public string Object { get; set; }

		[JsonProperty("search_model")]
		public string SearchModel { get; set; }


		[JsonProperty("selected_documents")]
		public List<SelectedDocuments> SelectedDocuments { get; set; }

	}

	public class SelectedDocuments
	{

		[JsonProperty("document")]
		public double Document { get; set; }


		[JsonProperty("text")]
		public string Text { get; set; }
	}


}
