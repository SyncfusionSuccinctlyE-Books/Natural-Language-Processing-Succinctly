//
//  Program: Playground
//   Author: Joseph Booth
//  Project: Natural Language Processing Succinctly
//  

using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace NaturalLanguageProcessing
{
    static public class GoogleNLP
    {

        static private string BASE_URL = "https://language.googleapis.com/v1/documents:";
        static private string GOOGLE_KEY = "<< YOUR GOOGLE KEY HERE >>";
        static private Dictionary<string, string> TagMapping = new Dictionary<string, string>
        {
            {"ADJ","JJ" },
            {"ADP","IN" },
            {"ADV","RB" },
            {"CONJ","CC" },
            {"DET","DT" },
            {"NOUN","NN" },
            {"NUM","CD" },
            {"PRON","PRP" },
            {"PUNCT","SYM" },
            {"VERB","VB" },
            {"X","FW" },
        };

        static private dynamic BuildAPICall(string Target, string msg)
        {
            dynamic results = null;
            string result;
            HttpWebRequest NLPrequest = (HttpWebRequest)WebRequest.Create(BASE_URL + Target + "?key=" + GOOGLE_KEY);
            NLPrequest.Method = "POST";
            NLPrequest.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(NLPrequest.GetRequestStream()))
            {
                string json = "{\"document\": {\"type\":\"PLAIN_TEXT\"," +
                                "\"content\":\"" + msg + "\"} }";
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
        //******************************************************************
        // public methods (Google calls)
        //******************************************************************
        static public dynamic analyzeEntities(string msg)
        {
            dynamic ans_ = BuildAPICall("analyzeEntities", msg);
            if (ans_ == null) return null;
            return ans_;
        }
        static public dynamic analyzeSentiment(string msg)
        {
            dynamic ans_ = BuildAPICall("analyzeSentiment", msg);
            if (ans_ == null) return null;
            return ans_;
        }

        static public dynamic analyzeSyntax(string msg)
        {
            dynamic ans_ = BuildAPICall("analyzeSyntax", msg);
            if (ans_ == null) return null;
            return ans_;
        }
        static public dynamic classifyText(string msg)
        {
            dynamic ans_ = BuildAPICall("classifyText", msg);
            if (ans_ == null) return null;
            return ans_;
        }
        static public dynamic analayzeEntitySentiment(string msg)
        {
            dynamic ans_ = BuildAPICall("analyzeEntitySentiment", msg);
            if (ans_ == null) return null;
            return ans_;
        }

        //******************************************************************
        // public methods (Wrapper calls)
        //******************************************************************
        static public string GoogleDetermineLanguage(string msg)
        {
            var ans_ = analyzeSyntax(msg);
            if (ans_ == null) return "??";
            return ans_.language.ToString();
        }

        static public List<string> GoogleExtractSentences(string Paragraph)
        {
            List<string> Sentences_ = new List<string>();
            var ans_ = analyzeSyntax(Paragraph);
            if (ans_ !=null)
            {
                for(int x=0;x<ans_.sentences.Count;x++)
                {
                    Sentences_.Add(ans_.sentences[x].text.content.Value);
                }
            }
            return Sentences_;
        }
        static public List<string> GoogleTaggedWords(string Sentence,bool includeLemma=false)
        {
            List<string> tags = new List<string>();
            var ans_ = analyzeSyntax(Sentence);
            if (ans_ != null)
            {
                for (int x = 0; x < ans_.tokens.Count;x++)
                {
                    string Tag = GoogleTokenToPennTreebank(ans_.tokens[x].partOfSpeech, ans_.tokens[x].text.content.Value.ToLower());
                    if (includeLemma)
                    {
                        Tag += ":" + ans_.tokens[x].lemma.Value;
                    }
                   tags.Add(Tag);
                }
            }
            return tags;
        }


        static private string GoogleTokenToPennTreebank(dynamic Token,string word)
        {
            string ans = "";
            string GoogleTag = Token.tag.Value.ToUpper();

            if (TagMapping.ContainsKey(GoogleTag))
            {
                ans = TagMapping[GoogleTag];
            }
            // Tweak certain tags
            if (ans=="NN")
            {
                string number = Token.number.Value.ToUpper();
                string proper = Token.proper.Value.ToUpper();

                if (number == "PLURAL" && proper == "PROPER" )  { ans = "NNPS"; }    // Plural proper nound
                if (number == "SINGULAR" && proper == "PROPER") { ans = "NNP"; }     // Singular proper nound
                if (ans=="NN" && number=="PLURAL")    { ans = "NNS"; }               // Plural common noun
            };
            if (ans=="VB")
            {
                string tense = Token.tense.Value.ToUpper();
                string person = Token.person.Value.ToUpper();
                ans = "VB";
                if (tense == "PAST") { ans = "VBD"; }    // Past tense verb
                if (tense == "PRESENT") { ans = "VBG"; }    // Present tense verb
            };
            return ans;
        }

    }



}
