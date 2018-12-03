//
//  Program: Playground
//   Author: Joseph Booth
//  Project: Natural Language Processing Succinctly
//  

using System.Net;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NaturalLanguageProcessing
{
    static public class MicrosoftNLP
    {

        private static readonly string API_KEY = "<< YOUR MICROSOFT KEY HERE >>";
        private static readonly string ENDPOINT = "https://westcentralus.api.cognitive.microsoft.com/text/analytics/v2.0";
        static private dynamic BuildAPICall(string Target, string msg)
        {
            dynamic results = null;
            string result;
            HttpWebRequest NLPrequest = (HttpWebRequest)WebRequest.Create(ENDPOINT + "/"+Target );
            NLPrequest.Method = "POST";
            NLPrequest.ContentType = "application/json";
            NLPrequest.Headers.Add("Ocp-Apim-Subscription-Key", API_KEY);

            using (var streamWriter = new StreamWriter(NLPrequest.GetRequestStream()))
            {
                string json = "{\"documents\": [ {\"id\":\"1\"," +
                                "\"text\":\"" + msg + "\"}] }";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var NLPresponse = (HttpWebResponse)NLPrequest.GetResponse();
            if (NLPresponse.StatusCode == HttpStatusCode.OK)
            {
                using (var streamReader = new StreamReader(NLPresponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                results = JsonConvert.DeserializeObject<dynamic>(result);
            }
            return results;
        }


        static public string DetectLanguage(string text)
        {
            var ans = "";
            var results = BuildAPICall("languages", text);
            try
            {
                ans =results.documents[0].detectedLanguages.First.name.Value;
            }
            catch
            {
                ans = "UNKNOWN";
            }
            return ans.ToString();
        }

        static public double analyzeSentiment(string msg)
        {
            double ans = 0;
            var results = BuildAPICall("sentiment", msg);
            try
            {
                ans = Convert.ToDouble(results.documents[0].score);
            }
            catch
            {
                ans = -1;
            }
            return ans;
        }

        static public List<string> classifyText(string text)
        {
            List<string> ans = new List<string>();
            var results = BuildAPICall("keyPhrases", text);
            try
            {
                foreach (var curPhrase in results.documents[0].keyPhrases)
                {
                    ans.Add(curPhrase.Value);
                }
            }
            catch
            {
            }
            return ans;
        }
        static public List<string> analyzeEntities(string text)
        {
            List<string> ans = new List<string>();
            var results = BuildAPICall("entities", text);
            try
            {
                foreach (var curEntity in results.documents[0].entities)
                {
                    ans.Add(curEntity.name.Value);
                }
            }
            catch
            {
            }
            return ans;
        }


    }
}
