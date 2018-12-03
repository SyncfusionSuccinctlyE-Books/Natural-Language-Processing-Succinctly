//
//  Program: Playground
//   Author: Joseph Booth
//  Project: Natural Language Processing Succinctly
//  

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NaturalLanguageProcessing
{
    static public class Phrases
    {


        static private Dictionary<string, string> PhraseRules = new Dictionary<string, string>
        {
            { "WP,RB*,VBD"    ,"QP" },  // Question about past
            { "WP,RB*,VB"     ,"QP" },   // Question phrase
            { "DR*,JJ*,NN"    ,"NP" },   // Noun phrase
            { "RB*,VB"        ,"VP" },   // Verb phrase

        };

        static public List<WordInfo> BuildPhrases(List<string> Words_,
                                      List<string> Tags_)
        {
            List<WordInfo> WordPhrases = new List<WordInfo>();

            foreach (KeyValuePair<string, string> pair in PhraseRules)
            {
                string[] tags = pair.Key.ToString().Split(',');
                string lastTag = tags.Last();
                string firstTag = tags.First();
                int EndPos = Tags_.IndexOf(lastTag);
                if (EndPos>=0) { 
                    int StartPos = Tags_.IndexOf(firstTag, 0, EndPos);
                    if (EndPos > StartPos && StartPos >= 0)
                    {
                        string TagsBetween = "";
                        for (int y = StartPos + 1; y < EndPos; y++)
                        {
                            TagsBetween += Tags_[y] + ",";
                        }
                        if (tags.Count() == 3)
                        {
                            if (string.IsNullOrWhiteSpace(TagsBetween)) { TagsBetween = ","; }
                            if (MatchPattern(tags[1], TagsBetween))
                            {
                                WordInfo Phrase = new WordInfo();
                                Phrase.Phrase = pair.Value;
                                Phrase.Concept = Words_[EndPos].ToLower();
                                for (int z = StartPos; z <= EndPos; z++)
                                {
                                    Phrase.ChildTags.Add(Tags_[z] + "|" + Words_[z]);
                                    Tags_[z] = "*";
                                }
                                WordPhrases.Add(Phrase);
                            }
                        }
                    }
                }
            }
            return WordPhrases;
        }


        static private bool MatchPattern(string Pattern, string tagsBetween)
        {
            bool ans = false;
            string lastchar = Pattern.Substring(Pattern.Length - 1);
            // Regex search patterns
            if ("+*?".IndexOf(lastchar) >= 0)
            {
                string regex = @"(" + Pattern.Replace(lastchar, ",)") + lastchar;
                ans = Regex.IsMatch(tagsBetween, regex);
            }
            else
            {
                ans = tagsBetween.StartsWith(Pattern);
            }
            return ans;
        }

    }
}
