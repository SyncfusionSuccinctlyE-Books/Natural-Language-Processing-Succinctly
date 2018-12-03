//
//  Program: Playground
//   Author: Joseph Booth
//  Project: Natural Language Processing Succinctly
//  

using System.Collections.Generic;
using static NaturalLanguageProcessing.Words;

/// <summary>
/// WordInfo object
/// </summary>


namespace NaturalLanguageProcessing
{
    public class WordInfo
    {

        public enum ENTITY_TYPE
        {
            NONE,           // Not an entity
            UNKNOWN,        // Unknown
            PERSON,         // PERSON
            LOCATION,       // CITY, COUNTRY
            ORGANIZATION,   // COMPANY
            EVENT,          //
            YEAR,           // 4 digit year
            CURR,           // Currency
            URL,            // URL
            EMAIL,          // Email address
            PHONE,          // Phone Number,
            DATE,           // Date,
            TIME,           // Time
            CREDIT,         // Credit card
            SSN,            // Social Security Number 
            HAPPY,          // Happy emoticon
            TITLE,          // Personal title  
            MONTH,          // Month Name,
            STATECODE       // State abbreviation
        }

        public string Phrase { get; set; }
        public Tags_ Tag { get; set; }
        public string Concept { get; set; }
        public ENTITY_TYPE TypeOfEntity { get; set; }
        public List<string> ChildTags { get; set; }

        public WordInfo() {
            ChildTags = new List<string>();
        }

    }
}
