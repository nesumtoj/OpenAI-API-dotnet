using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API
{
	/// <summary>
	/// The API lets you do semantic search over documents. This means that you can provide a query, such as a natural language question or a statement, and find documents that answer the question or are semantically related to the statement. The “documents” can be words, sentences, paragraphs or even longer documents. For example, if you provide documents "White House", "hospital", "school" and query "the president", you’ll get a different similarity score for each document. The higher the similarity score, the more semantically similar the document is to the query (in this example, “White House” will be most similar to “the president”).
	/// </summary>
	public class AnswersEndpoint
	{
		OpenAIAPI Api;

		/// <summary>
		/// Constructor of the api endpoint.  Rather than instantiating this yourself, access it through an instance of <see cref="OpenAIAPI"/> as <see cref="OpenAIAPI.Answers"/>.
		/// </summary>
		/// <param name="api"></param>
		internal AnswersEndpoint(OpenAIAPI api)
		{
			this.Api = api;
		}

		public async Task<AnswersResult> GetSearchResultsAsync(AnswersRequest request)
		{
			if (Api.Auth?.ApiKey is null)
			{
				throw new AuthenticationException("You must provide API authentication.");
			}

			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Api.Auth.ApiKey);
			client.DefaultRequestHeaders.Add("User-Agent", "ebo-clinet-openai");

			string jsonContent = JsonConvert.SerializeObject(request, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore });
			var stringContent = new StringContent(jsonContent, UnicodeEncoding.UTF8, "application/json");

			var response = await client.PostAsync($"https://api.openai.com/v1/answers", stringContent);
			if (response.IsSuccessStatusCode)
			{
				string resultAsString = await response.Content.ReadAsStringAsync();
				var res = JsonConvert.DeserializeObject<AnswersResult>(resultAsString);

				//try
				//{
				//	res.Organization = response.Headers.GetValues("Openai-Organization").FirstOrDefault();
				//	res.RequestId = response.Headers.GetValues("X-Request-ID").FirstOrDefault();
				//	res.ProcessingTime = TimeSpan.FromMilliseconds(int.Parse(response.Headers.GetValues("Openai-Processing-Ms").First()));
				//}
				//catch (Exception) { }

				if (res.Answers == null || res.Answers.Count == 0)
					throw new HttpRequestException("API returnes no search results.  HTTP status code: " + response.StatusCode.ToString() + ". Response body: " + resultAsString);

				return res;
			}
			else
			{
				throw new HttpRequestException("Error calling OpenAi API to get completion.  HTTP status code: " + response.StatusCode.ToString() + ". Request body: " + jsonContent);
			}
		}

	}
}