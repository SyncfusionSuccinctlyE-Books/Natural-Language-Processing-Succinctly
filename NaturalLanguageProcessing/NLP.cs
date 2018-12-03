//
//  Program: Playground
//   Author: Joseph Booth
//  Project: Natural Language Processing Succinctly
//  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace NaturalLanguageProcessing
{
    static public class NLP
    {
        private static readonly PluralizationService ps;
        private static List<Rule> Tweaks;

        static NLP()
        {
            CultureInfo ci = new CultureInfo("en-us");
            ps = PluralizationService.CreateService(ci);

            Tweaks = new List<Rule>();
            Tweaks.Add(new Rule
            {
                BeforeTag = Words.Tags_.WDT,
                OptionalWord = "who",
                AfterTag = Words.Tags_.NN,
                NewTag = Words.Tags_.VB
            }
            );
            Tweaks.Add(new Rule
            {
                BeforeTag = Words.Tags_.DT,
                OptionalWord = "",
                AfterTag = Words.Tags_.VB,
                NewTag = Words.Tags_.NN
            }
            );

        }
        static public List<string> SimpleSentencesSplit(string Paragraph)
        {
        char[] EndCharacters = new char[] { '.', '!', '?', ';' };
        string[] Abbreviations =
            new string[] { "Mr.", "Mrs.", "Dr.","Ms.","Sr.","Jr.","etc.",
                        "Sun.","Mon.","Tue.","Wed.","Thu.","Fri.","Sat.",
                        "Jan.","Feb.","Mar.","Apr.","May.","Jun.","Jul.",
                        "Aug.","Sep.","Oct.","Nov.","Dec.",
                        "www.",".com",".org",".net"};
        char[] CharSep = new char[] { '.', '!', '?', ';' };
        foreach (string curAbbr in Abbreviations)
        {
            Paragraph = Regex.Replace(Paragraph, curAbbr, curAbbr.Replace(".", "~"),
                    RegexOptions.IgnoreCase);
        }
        List<string> Sentences_ = new List<string>();
        string curSentence;
        string[] Sentences = Paragraph.Split(CharSep,
                                StringSplitOptions.RemoveEmptyEntries);
        foreach (string sentence in Sentences)
        {
            curSentence = sentence.Replace("~", ".").Trim();
            Sentences_.Add(curSentence);
        }
        return Sentences_;

        }
        /// <summary>
        /// Extract sentences from a paragraph of text
        /// </summary>
        /// <param name="Paragraph">Paragraph string</param>
        /// <returns>List of sentences</returns>
        static public List<string> ExtractSentences(string Paragraph)
        {
            List<string> Sentences_ = new List<string>();
            List<string> Sections_ = new List<string>();
            if (!string.IsNullOrEmpty(Paragraph))
            {

                string punctuation = @"(\S.+?[.!?\u203D\u2E18\u203C\u2047-\u2049])(?=\s+|$)";

                // Split by new line character
                List<string> FirstPass = Regex.Split(Paragraph, @"((?:\r ?\n |\r)+)", RegexOptions.IgnorePatternWhitespace).
                                   Where(s => s != Environment.NewLine && !string.IsNullOrEmpty(s)).ToList<string>();

                foreach (string curSentence in FirstPass)
                {
                    string[] chunks = Regex.Split(curSentence, punctuation);
                    Sections_.AddRange(chunks.ToList<string>());
                }
            }

            Regex rAbbrevs = new Regex(@"\b(dr|mrs|mr|ms|assn|dept|corp|rte|ave|blvd|hwy|www|com|edu|gov|"+
                                        "jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)[.!?‽⸘‼⁇-⁉] *$", RegexOptions.IgnoreCase);

            string acronyms = @"^(?:[A-Z]\.){2,}$";
            Sections_ = Sections_.Where(s => s.Length > 0).ToList<string>(); // Remove nulls
            if (Sections_.Count > 0)
            {
                for (int x = 0; x < Sections_.Count; x++)
                {
                    string curPart = Sections_[x];          // Current word 
                    string nextPart = "";                   // Next word


                    if (x + 1 < Sections_.Count)
                    {
                        nextPart = Sections_[x + 1];
                        if (nextPart.Length > 0 && (rAbbrevs.IsMatch(curPart) || curPart.Length == 1 ) )
                        {
                            Sections_[x + 1] = curPart + nextPart;
                        }
                        else
                        {
                            if (curPart.Trim().Length > 0)
                            {

                                int pv = Sentences_.Count - 1;
                                if (pv >= 0 && Regex.IsMatch(Sentences_[pv], acronyms))
                                {
                                    Sentences_[pv] += curPart;
                                }
                                else
                                {
                                    Sentences_.Add(curPart);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Last item, add to the sentence stack
                        if (curPart.Trim().Length > 0)
                        {
                            Sentences_.Add(curPart);
                        }
                    }
                }
            }
            return Sentences_;
        }

        /// <summary>
        /// Extract words from a sentence
        /// </summary>
        /// <param name="Sentence">Sentence string</param>
        /// <returns>List of words from sentence</returns>
        static public List<string> ExtractWords(string Sentence,bool ExpandCont=false)
        {
            List<string> Words_ = new List<string>();
            var punctuation = Sentence.Where(Char.IsPunctuation).Distinct().ToArray();
            Words_ = Sentence.Split().Select(x => x.Trim(punctuation)).ToList<string>();
            // Expand contractions?
            if (ExpandCont) {
                return ExpandContractions(Words_);
            }
            return Words_;
        }

        static public List<string> ExpandContractions(List<string> words_)
        {
            List<string> ans_ = new List<string>();
            Regex rgx = new Regex(@"^((\w+)\'(ve|ll|t|d|re))$");
            for (int x = 0; x < words_.Count; x++)
            {
                MatchCollection matches_ = rgx.Matches(words_[x]);
                if (matches_.Count > 0)
                {
                    int lst = matches_[0].Groups.Count - 1;
                    ans_.Add(matches_[0].Groups[lst-1].Value);  // Get word
                    string cont = matches_[0].Groups[lst].Value.ToLower();
                    if (cont == "ve") { ans_.Add("have"); }
                    if (cont == "ll") { ans_.Add("will"); }
                    if (cont == "t") { ans_.Add("not"); }
                    if (cont == "d") { ans_.Add("could"); }
                    if (cont == "re") { ans_.Add("are"); }
                }
                else
                {
                    ans_.Add(words_[x]);
                }
            }
            return ans_;
        }


        static public List<WordInfo> NamedEntities(List<WordInfo> WordTags)
        {
            List<WordInfo> ParsedEntries = WordTags;
            return ParsedEntries;
        }

    }
}
