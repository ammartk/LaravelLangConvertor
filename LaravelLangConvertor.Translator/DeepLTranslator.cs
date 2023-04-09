using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using LaravelLangConvertor.Translator.Dto;
using Newtonsoft.Json;
namespace LaravelLangConvertor.Translator
{
    public class DeepLTranslator : ILangTranslator
    {
        string deepLApiKey;
        public DeepLTranslator(string apikey) {
            deepLApiKey = apikey;
        }
        public async Task<List<string>> TranslateToLanguage(Dictionary<string, string> values, string language)
        {

            var httpClient = new HttpClient();
            
            var deepLApiUrl = $"https://api-free.deepl.com/v2/translate?auth_key={deepLApiKey}"; // Replace "fr" with your target language code

            var strings = values.Values.ToList();
            var translations = await TranslateText(httpClient, deepLApiUrl, strings, language);

            // Output the translated key-value pairs
            foreach (var value in translations)
            {
                Console.WriteLine(value.text);
            }
            return translations.Select(x => x.text).ToList();
        }
        private async Task<List<TranslationDto>> TranslateText(HttpClient httpClient, string apiUrl, List<string> strings, string targetLanguage)
        {
            var requestData = new TranslationRequest
            {
                text = strings,
                target_lang = targetLanguage
            };

            var requestBody = JsonConvert.SerializeObject(requestData);
            var response = await httpClient.PostAsync(apiUrl, new StringContent(requestBody, Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();

            TranslationResponseDto responseData = JsonConvert.DeserializeObject<TranslationResponseDto>(responseBody);
            return responseData.translations;
        }
    }
}