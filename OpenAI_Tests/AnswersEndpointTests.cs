using NUnit.Framework;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenAI_Tests
{
	public class AnswersEndpointTests
	{
		[SetUp]
		public void Setup()
		{
			OpenAI_API.APIAuthentication.Default = new OpenAI_API.APIAuthentication("");
		}

		[Test]
		public void TestBasicSearch()
		{
			var api = new OpenAI_API.OpenAIAPI(engine: Engine.Curie);

			Assert.IsNotNull(api.Search);

			AnswersRequest req = new AnswersRequest(new List<string>() { "file-qWZE2taY2W0fkaQBgq0moPZq" }, new List<List<string>>() { (new List<string>() { "What is human life expectancy in the United States?", "78 years." }) }, "In 2017, U.S. life expectancy was 78.6 years.", 5, "curie", "is the policy covering fire", "ada", new List<string>() { "\n", "<|endoftext|>" });

			var result = api.Answers.GetSearchResultsAsync(req).Result.Answers.FirstOrDefault();
			Assert.IsNotNull(result);
			Assert.AreEqual("Yes.", result);
		}

		// TODO: More tests needed but this covers basic functionality at least

	}
}