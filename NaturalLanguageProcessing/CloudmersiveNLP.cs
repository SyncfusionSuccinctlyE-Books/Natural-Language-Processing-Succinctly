//
//  Program: CloudmersiveNLP
//   Author: Joseph Booth
//  Project: Natural Language Processing Succinctly
//  

using System;
using System.Collections.Generic;
using System.Linq;
using Cloudmersive.APIClient.NET.NLP.Api;
using Cloudmersive.APIClient.NET.NLP.Client;
using Cloudmersive.APIClient.NET.NLP.Model;

/// <summary>
/// Cloudmersive NLP - Part of Natural Language Succinctly (Syncfusion)
/// </summary>

namespace NaturalLanguageProcessing
{
    /// <summary>
    /// Cloudmersive class library calls
    /// </summary>
    static public class CloudmersiveNLP
    {
        private static string APIKey = "<< YOUR KEY HERE >>";

        static public void SetAPIKey(string myKey)
        {
            if (!string.IsNullOrEmpty(myKey))
            {
                APIKey = myKey;
            }
        }
        //************************************************************************
        // Cloudmersive calls
        //************************************************************************
        static public LanguageDetectionResponse DetectLanguage(string text)
        {
            // Configure API key authorization: Apikey
            Configuration.Default.AddApiKey("Apikey", APIKey);

            var apiInstance = new LanguageDetectionApi();       // Which API to call
            try
            {
                // Detect language of text
                LanguageDetectionResponse result = apiInstance.LanguageDetectionPost(text);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        static public string ExtractEntities(string text)
        {
            // Configure API key authorization: Apikey
            Configuration.Default.AddApiKey("Apikey", APIKey);

            var apiInstance = new ExtractEntitiesStringApi(); 
            try
            {
                string result = apiInstance.ExtractEntitiesStringPost(text);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        static public List<string> ExtractSentences(string Paragraph)
        {
            Configuration.Default.AddApiKey("Apikey", APIKey);
            var apiInstance = new SentencesApi();
            try
            {
                // Extract sentences from string
                string result = apiInstance.SentencesPost(Paragraph);
                string[] Sentences_ = result.Replace("\\r", "").Split(new String[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int x = 0; x < Sentences_.Length; x++)
                {
                    Sentences_[x] = Sentences_[x].Replace("\"", "");
                }
                return Sentences_.ToList<string>();
            }
            catch (Exception)
            {
                return null;
            }
        }
        static public string TaggedPartofSpeechJSON(string text)
        {
            Configuration.Default.AddApiKey("Apikey", APIKey);
            var apiInstance = new PosTaggerStringApi();

            try
            {
                // Part-of-speech tag a string
                var result = apiInstance.PosTaggerStringPost(text);
                return result.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
        static public List<string> GetWords(string text)
        {
            Configuration.Default.AddApiKey("Apikey", APIKey);
            var apiInstance = new WordsApi();

            try
            {
                string result = apiInstance.WordsGetWordsString(text);
                List<string> Words_ = result.Replace("\"", "").Split(',').ToList<string>();
                return Words_;

            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
