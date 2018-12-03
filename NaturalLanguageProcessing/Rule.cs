//
//  Program: Playground
//   Author: Joseph Booth
//  Project: Natural Language Processing Succinctly
//  

using static NaturalLanguageProcessing.Words;

namespace NaturalLanguageProcessing
{
    /// <summary>
    /// Rule -> Rule for determining tags to tweak
    /// </summary>
    /// <example>
    ///     WDT,"who",NN,VB (Question word who, followed by a noun, change to verb)
    /// </example>
    public class Rule
    {
        public Tags_ BeforeTag { get; set; }
        // Can be an empty string, or regular expression, or simply a word
        public string OptionalWord { get; set; }
        public Tags_ AfterTag { get; set; }
        public Tags_ NewTag { get; set; }
    }
}
