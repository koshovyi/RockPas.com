using RockPaperScissors.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RockPaperScissors.Services
{

	public class GoogleRecaptchaService
	{

		private const string API_VERIFICATION_LINK = "https://www.google.com/recaptcha/api/siteverify";

		private readonly IHttpClientFactory _clientFactory;

		public GoogleRecaptchaService(IHttpClientFactory clientFactory)
		{
			this._clientFactory = clientFactory;
		}

		/// <summary>
		/// Googlel's answer
		/// </summary>
		protected class ReCaptchaResponse
		{
			public bool success { get; set; }

			public string challenge_ts { get; set; }

			public string hostname { get; set; }

			public string[] errorcodes { get; set; }
		}

		public async Task<bool> ValidateRecaptcha(string ApiSecretKey, string TokenResponse)
		{
			if (string.IsNullOrEmpty(TokenResponse))
				throw new ArgumentException(nameof(TokenResponse));

			using (HttpClient webClient = this._clientFactory.CreateClient())
			{
				FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
				{
					new KeyValuePair<string, string>("secret", ApiSecretKey),
					new KeyValuePair<string, string>("response", TokenResponse)
				});
				HttpResponseMessage response = await webClient.PostAsync(API_VERIFICATION_LINK, content);
				string json = await response.Content.ReadAsStringAsync();

				ReCaptchaResponse reCaptchaResponse = JsonConvert.FromMessage<ReCaptchaResponse>(json);
				if (reCaptchaResponse != null && reCaptchaResponse.success)
					return true;
			}

			return false;
		}

	}

}
