using ClarinDiary.Business.DTO;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClarinDiary.Business.Helper
{
    public static class TraslatorHelper
    {
        private const string URL_TRANSLATE_GOOGLE_API = "https://google-translate1.p.rapidapi.com/language/translate/v2";
        private const string BASE_API_KEY = "ddc0777517msh5a6a2cfb0517842p1aa8f3jsna25bebd9395c";


        public static string TraslateText(string contentText, string langOrigin, string langTarget)
        {
            var client = new RestClient(URL_TRANSLATE_GOOGLE_API);
            var request = new RestRequest(Method.POST);
            request.AddHeader("x-rapidapi-host", "google-translate1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", BASE_API_KEY);
            request.AddHeader("accept-encoding", "application/gzip");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"source={langOrigin}&q={contentText}&target={langTarget}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            TranslationDataDTO translationData;
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return contentText;

            translationData = JsonConvert.DeserializeObject<TranslationDataDTO>(response.Content);
            return translationData.data.translations[0].translatedText;
        }
    }
}
