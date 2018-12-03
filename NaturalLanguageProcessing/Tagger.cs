//
//  Program: Playground
//   Author: Joseph Booth
//  Project: Natural Language Processing Succinctly
//  

using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System;

namespace NaturalLanguageProcessing
{
    static public class Tagger
    {

        private static PluralizationService ps;
        private static string TreebankTagList = "|CC|CD|DT|EX|FW|IN|JJ|JJR|JJ|LS|MD|NN|NNS|NNP|NNPS|PDT|POS|"+
                                                "PRP|PRP$|RB|RBR|RBS|RP|SYM|TO|UH|VB|VBZ|VBP|VBD|VBN|VBG|WDT|WP|WP$|WRB|";

        public  enum ErrorCode
            {   ADDED_OK,
                INVALID_REGEX,
                INVALID_TAG,
                DUPLICATE_ENTRY
            }
        static Tagger()
        {
            CultureInfo ci = new CultureInfo("en-us");
            ps = PluralizationService.CreateService(ci);
        }

        static private Dictionary<string, string> TransformRules = new Dictionary<string, string>
        {
            { "WP,NN"     ,"NN=VB" },   // Who spoke at the rally?
            { "DT,VB"     ,"VB=NN" },   // The play really resonated
            { "JJ,VB"     ,"VB=NN" },   // The play really resonated
            { "DT,JJ,VB"  ,"JJ=NN" },   // The rich pay more taxes
            { "DT,RB,JJ"  ,"RB=JJ" },   // The very hungry dog
            { "DT,JJ+,VB" ,"VB=NN" }    // The amazing score was breathtaking
        };

        static private Dictionary<string, string> Taglist = new Dictionary<string, string>
        {
            { @"(?i)^[a-z]{2,}ing$","VBG" },                                  // Present tense verb
            { @"(?i)^[a-z]{2,}[^aeiou]ed$","VBD" },                           // Past tense verb
            { @"^(((\d{1,3})(,\d{3})*)|(\d+))(.\d+)?$", "CD" },               // Numbers,
            { @"(?i)^[a-z]{2,}[s|ss|sh|ch|x|z|]es$","NNS" },                  // Plural nouns 
            { @"(?i)^[a-z]{1,}[aeiou]ves$","NNS" },                           // Plural nouns 
            { @"(?i)^[a-z]{2,}\'s$","POS" },                                  // Possessive 
            { @"(?i)^(the|an|a)$","DT" },                                     // Determiners
            { @"(?i)^(for|and|nor|but|or|yet|so)$","DT" },                    // Conjuctions
            { @"(?i)^(I|me|we|us|you|they|him|her|them|this)$","PRP" },       // Pronouns
            { @"(?i)^(of|on|at|unless|before|after|with|in)$","IN"},          // Prepositions
            { @"(?i)^(to)$","TO" },                                           // To
            { @"(?i)^(who|whom|what)$","WP" }                                 // Question words
        };
        static private Dictionary<string, string> MyDictionary = new Dictionary<string, string>
        {
            {"aboard","RB"},
            {"almost","RB"},
            {"always","RB"},
            {"and/or","CC"},
            {"ate","VBD"},
            {"bad","JJ"},
            {"became","VBD"},
            {"began","VBD"},
            {"best","JJS"},
            {"better","JJR"},
            {"bought","VBD"},
            {"brought","VBD"},
            {"built","VBD"},
            {"came","VBD"},
            {"comes","VB"},
            {"costly","JJ"},
            {"deadly","JJ"},
            {"did","VBD"},
            {"et","CC"},
            {"fast","JJ"},
            {"faster","JJR"},
            {"fastest","JJS"},
            {"fell","VBD"},
            {"felt","VBD"},
            {"for","CC"},
            {"found","VBD"},
            {"french","JJ" },
            {"friendly","JJ"},
            {"gave","VBD"},
            {"get","VB"},
            {"good","JJ"},
            {"got","VBD"},
            {"great","JJ"},
            {"greater","JJR"},
            {"greatest","JJS"},
            {"grew","VBD"},
            {"had","VBD"},
            {"has","VB"},
            {"half","JJ"},
            {"heard","VBD"},
            {"held","VBD"},
            {"his","PRP$"},
            {"hungry","JJ"},
            {"indoor","JJ"},
            {"is","VB"},
            {"just","RB"},
            {"kept","VBD"},
            {"knew","VBD"},
            {"later","RB"},
            {"learnt","VBD"},
            {"least","JJS"},
            {"led","VBD"},
            {"left","VBD"},
            {"less","JJ"},
            {"lesser","JJR"},
            {"let","VBD"},
            {"likely","JJ"},
            {"long","JJ"},
            {"longer","JJR"},
            {"longest","JJS"},
            {"lost","VBD"},
            {"lovely","JJ"},
            {"low","JJ"},
            {"lower","JJR"},
            {"lowest","JJS"},
            {"made","VBD"},
            {"meant","VBD"},
            {"met","VBD"},
            {"more","RB"},
            {"most","RB"},
            {"never","RB"},
            {"new","JJ"},
            {"newer","JJR"},
            {"newest","JJS"},
            {"nice","JJ"},
            {"nicer","JJR"},
            {"nicest","JJS"},
            {"nor","CC"},
            {"now","RB"},
            {"often","RB"},
            {"old","JJ"},
            {"older","JJR"},
            {"oldest","JJS"},
            {"only","RB"},
            {"open","VB"},
            {"or","CC"},
            {"paid","VBD"},
            {"pay","VB"},
            {"play","VB"},
            {"put","VBD"},
            {"quick","JJ"},
            {"quicker","JJR"},
            {"quickest","JJS"},
            {"quickly","RB"},
            {"quite","RB"},
            {"rather","RB"},
            {"really","RB"},
            {"rich","JJ"},
            {"sad","JJ"},
            {"sadder","JJR"},
            {"saddest","JJS"},
            {"said","VBD"},
            {"sat","VBD"},
            {"saw","VBD"},
            {"seldom","RB"},
            {"sent","VBD"},
            {"set","VBD"},
            {"simple","JJ"},
            {"simpler","JJR"},
            {"simplest","JJS"},
            {"slow","JJ"},
            {"slower","JJR"},
            {"slowest","JJS"},
            {"small","JJ"},
            {"smaller","JJR"},
            {"smallest","JJS"},
            {"so","CC"},
            {"sometimes","RB"},
            {"spent","VBD"},
            {"stood","VBD"},
            {"thought","VBD"},
            {"today","RB"},
            {"tomorrow","RB"},
            {"took","VBD"},
            {"ugly","JJ"},
            {"understood","VBD"},
            {"unlikely","JJ"},
            {"very","RB"},
            {"was","VBD"},
            {"weekly","JJ"},
            {"well","RB"},
            {"went","VBD"},
            {"were","VBD"},
            {"wild","JJ"},
            {"wilder","JJR"},
            {"wildest","JJS"},
            {"winningest","JJS"},
            {"won","VBD"},
            {"worse","JJR"},
            {"worst","JJS"},
            {"wrote","VBD"},
            {"yesterday","RB"},
            {"yet","CC"},
            {"young","JJ"},
            {"younger","JJR"},
            {"youngest","JJS"}
        };

        /// <summary>
        /// Return the best guess tag for a word
        /// </summary>
        /// <param name="Word_"></param>
        /// <returns></returns>
        static public string TagWord(string Word_)
        {
            string tag = "NN";               // Assume its a noun
            // Check patterns
            foreach (var RegEx in Taglist)
            {
                if (Regex.IsMatch(Word_, RegEx.Key))
                {
                    tag = RegEx.Value;
                    break;
                }
            }
            // Lookup word
            string WordLower = Word_.ToLower();
            if (MyDictionary.ContainsKey(WordLower))
            {
                tag = MyDictionary[WordLower];
            }
            if (ps.IsPlural(WordLower) && tag=="NN")
            {
                tag = "NNS";
            }
            // If nothing found, maybe it is a proper noun?
            if (tag=="NN" && Regex.IsMatch(Word_, @"[A-Z]{1}[a-z\-]{2,}"))
            {
                tag = "NNP";
            }
            return tag;
        }

        /// <summary>
        /// Add Tag
        /// </summary>
        /// <param name="Expression">Regular Expression to check</param>
        /// <param name="tag">Treebank Tag code</param>
        /// <returns></returns>
        static public ErrorCode AddExpression(string pattern,string tag)
        {
            ErrorCode ans = ErrorCode.ADDED_OK;
            if (TreebankTagList.IndexOf(tag) < 0)  
            {
                return ErrorCode.INVALID_TAG;
            }
            try
            {
                new Regex(pattern);
            }
            catch {
                ans = ErrorCode.INVALID_REGEX;
            }

            return ans;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Expression"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        static public ErrorCode AddToDictionary(string Word_, string tag,bool Overwrite=false)
        {
            ErrorCode ans = ErrorCode.ADDED_OK;
            if (TreebankTagList.IndexOf(tag)<0)
            {
                return ErrorCode.INVALID_TAG;
            }
            string WordLower = Word_.ToLower();
            if (MyDictionary.ContainsKey(WordLower))
            {
                if (Overwrite)
                {
                    MyDictionary[WordLower] = tag;
                    return ErrorCode.ADDED_OK; 
                }
                return ErrorCode.DUPLICATE_ENTRY;
            }
            MyDictionary.Add(WordLower, tag);

            return ans;
        }

        static public List<string> RevisedTags(List<string> Words_,List<string> Tags_)
        {
            List<string> revised = Tags_;
            for(int x=0;x<revised.Count-1;x++)
            {
                string curTag = revised[x];    
                foreach (KeyValuePair<string, string> pair in TransformRules)
                {
                    string[] tagList = pair.Key.Split(',');                     // Get list of tags to look for
                    if (tagList[0] == curTag)                                   // Does rule begin with this tag
                    {
                        int EndPos = revised.IndexOf(tagList.Last(), x + 1);    // Find ending tag after current tag
                        if (EndPos>0)
                        {
                            string[] newTags = pair.Value.Split('=');
                            string ThisTag = newTags[0];                        // This tag gets replaced  
                            string Becomes = newTags[1];                        // with this tag
                            int ThisPos = revised.IndexOf(ThisTag, x + 1);      // Tag we want to change, if pattern found  
                            int NumTagsBetween = (EndPos - x) - 1;
                            if (NumTagsBetween < 1 && tagList.Count() == 2)     // Sequential tags
                            {
                                revised[ThisPos] = Becomes;
                                break;
                            }
                            if (tagList.Count() == 3)
                            {
                                string TagsBetween = "";
                                for (int y = x + 1; y < EndPos; y++)
                                {
                                    TagsBetween += revised[y] + ",";
                                }
                                if (MatchMiddlePattern(tagList[1],TagsBetween))
                                {
                                    revised[ThisPos] = Becomes;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return revised;
        }

        static private bool MatchMiddlePattern(string Pattern,string tagsBetween)
        {
            bool ans= false;
            string lastchar = Pattern.Substring(Pattern.Length - 1);
            // Regex search patterns
            if ("+*?".IndexOf(lastchar)>=0)
            {
                string regex = @"^(" + Pattern.Replace(lastchar, "," + lastchar) + ")$";
                ans =Regex.IsMatch(tagsBetween, regex);
            }
            else
            {
                ans = tagsBetween.StartsWith(Pattern);
            }
            return ans;
        }

    }
}
