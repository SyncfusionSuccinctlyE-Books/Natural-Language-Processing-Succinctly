//
//  Program: Playground
//   Author: Joseph Booth
//  Project: Natural Language Processing Succinctly
//  

using System.Collections.Generic;

namespace NaturalLanguageProcessing
{
    public static class Words
    {

        public static Dictionary<string, string> TreeBankTags;


        public enum Tags_ { XX,    //  Unknown
                            CC,    //  Coordinating conjunction
                            CD,    //  Cardinal number
                            DT,    //  Determiner
                            EX,    //  Existential there
                            FW,    //  Foreign word
                            IN,    //  Preposition or subordinating conjunction
                            JJ,    //  Adjective
                            JJR,   //  Adjective, comparative
                            JJS,   //  Adjective, superlative
                            LS,    //  List item marker
                            MD,    //  Modal
                            NN,    //  Noun, singular or mass
                            NNS,   //  Noun, plural
                            NNP,   //  Proper noun, singular
                            NNPS,  //  Proper noun, plural
                            PDT,   //  Predeterminer
                            POS,   //  Possessive ending
                            PRP,   //  Personal pronoun
                            PRP_,  //  Possessive pronoun
                            RB,    //  Adverb
                            RBR,   //  Adverb, comparative
                            RBS,   //  Adverb, superlative
                            RP,    //  Particle
                            SYM,   //  Symbol
                            TO,    //  to
                            UH,    //  Interjection
                            VB,    //  Verb, base form
                            VBD,   //  Verb, past tense
                            VBG,   //  Verb, gerund or present participle
                            VBN,   //  Verb, past participle
                            VBP,   //  Verb, non-3rd person singular present
                            VBZ,   //  Verb, 3rd person singular present
                            WDT,   //  Wh-determiner
                            WP,    //  Wh-pronoun
                            WP_,   //  Possessive wh-pronoun
                            WRB,   //  Wh-adverb
                            CURR,  //  Currency
                            URL,   //  URL
                            EMAIL, //  Email address
                            PHONE, //  Phone Number,
                            DATE,     //  Date,
                            TIME,     //  Time
                            YEAR,     //  Year
                            CREDIT,   // Credit card
                            SSN,      // Social Security Number 
                            HAPPY,    // Happy emoticon
                            TITLE,    // Personal title  
                            MONTH,    // Month Name,
                            CITY,
                            AIRPORT,
                            PERSON,
                            ORGANIZATION,
                            EVENT
        }


        


    }
}
