using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenAI_API
{
	public class AnswersRequest
	{

		[JsonProperty("documents")]
		public List<string> Documents { get; set; }

		[JsonProperty("examples")]
		public List<List<string>> Examples { get; set; }

		[JsonProperty("examples_context")]
		public string ExamplesContext { get; set; }

		[JsonProperty("max_tokens")]
		public int MaxTokens { get; set; }

		[JsonProperty("model")]
		public string Model { get; set; }

		[JsonProperty("question")]
		public string Question { get; set; }

		[JsonProperty("search_model")]
		public string SearchModel { get; set; }

		[JsonProperty("stop")]
		public List<string> Stop { get; set; }

		public AnswersRequest(List<string> documents, List<List<string>> examples, string examplesContext, int maxTokens, string model,string question, string searchModel, List<string> stop)
		{
			Documents = documents?.ToList() ?? new List<string>();
			Examples = examples?.ToList() ?? new List<List<string>>();
			ExamplesContext = examplesContext;
			MaxTokens = maxTokens;
			Model = model;
			Question = question;
			SearchModel = searchModel;
			Stop = stop;
		}

	}
}
