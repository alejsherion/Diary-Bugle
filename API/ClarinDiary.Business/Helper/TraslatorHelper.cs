using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

namespace ClarinDiary.Business.Helper
{
    public static class TraslatorHelper
    {
        private const string URL_TRANSLATE_GOOGLE_API = "https://translate.googleapis.com/translate_a";

        public static string TraslateText(string contentText, string langOrigin, string langTarget)
        {
            var client = new HttpClient();

            var _contentText = HttpUtility.UrlEncode(contentText.Replace(Environment.NewLine, " "));

            using HttpResponseMessage response = client.GetAsync($"{URL_TRANSLATE_GOOGLE_API}/single?client=gtx&sl={langOrigin}&tl={langTarget}&dt=t&q={_contentText}").Result;
            using HttpContent content = response.Content;
            
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return contentText;

            string serviceResponse = content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject(serviceResponse);

            List<string> traslationResult = new List<string>();
            var resultList = ((result as dynamic)[0] as JArray);
            for (int i = 0; i < resultList.Count; i++)
                traslationResult.Add((resultList[i] as JArray)[0].ToString());

            return string.Join(Environment.NewLine, traslationResult);
        }
    }
}
