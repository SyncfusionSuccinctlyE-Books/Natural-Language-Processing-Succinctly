//
//  Program: Playground
//   Author: Joseph Booth
//  Project: Natural Language Processing Succinctly
//  

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NaturalLanguageProcessing
{
    static public class Entities
    {

        static public Dictionary<string, string> NamedEntities = new Dictionary<string, string>
        {
            { "US OPEN"         ,"EVENT" },   // 
            { "FRENCH OPEN"     ,"EVENT" },   // 
            { "AUSTRALIAN OPEN" ,"EVENT" },   // 
            { "WIMBLEDON"       ,"EVENT" },   // 
            { "WIMBLEDONS"      ,"EVENT" },   // 
            { "MARDI GRAS"      ,"EVENT" },   // 
            { "MICROSOFT"       ,"ORG" },     // 
            { "GOOGLE"          ,"ORG" },     // 
        };

        static public Dictionary<string, string> Patterns =   new Dictionary<string, string>
        { 
            { @"^\$(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$","CURRENCY"  },
            { @"(?i)^[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu)$","URL"   },
            { @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$","EMAIL" },
            { @"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$","PHONE" },
            { @"^((\d{2})|(\d))\/((\d{2})|(\d))\/((\d{4})|(\d{2}))$","DATE" },
            { @"^(\d{4})$","YEAR"  },
            { @"(?i)^(Mr|Ms|Miss|Ms)$","TITLE"},
            { @"^(\d+)$","CD"},
            { @"(?i)^(Jan(uary)?|Feb(ruary)?|Mar(ch)?|Apr(il)?|May|Jun(e)?|Jul(y)?|Aug(ust)?|Sep(tember)?|Sept|Oct(ober)?|Nov(ember)?|Dec(ember)?)$","MONTH" }
        };

        static public Dictionary<string, string> TagPatterns = new Dictionary<string, string>
        {
            {"TITLE|NNP|NNP","PERSON" },
            {"TITLE|NNP","PERSON" }
        };


        static public void UpdateNamedEntities(List<string> Words_, List<string> Tags_)
        {
            var WordArray = Words_.ToArray();
            // Named entities
            foreach (KeyValuePair<string, string> pair in NamedEntities)
            {
                string[] tags = pair.Key.ToString().Split(' ');
                int nSize = tags.Length;
                int nStart = Array.FindIndex(WordArray, ne => ne.Equals(tags[0], StringComparison.InvariantCultureIgnoreCase));
                if (nStart >= 0)
                {

                    if (nStart + nSize <= Words_.Count)      // Do we have enough words left to fill pattern?
                    {
                        bool FoundIt = true;
                        int TagPos = 1;
                        for (int x = nStart + 1; x < nStart + nSize; x++)
                        {

                            if (Words_[x].ToUpper() != tags[TagPos].ToUpper())
                            {
                                FoundIt = false;
                                break;
                            }
                            TagPos++;
                        }

                        if (FoundIt)
                        {
                            Tags_[nStart] = pair.Value;
                            Words_[nStart] = pair.Key;
                            if (nSize > 1 && (nSize + nStart <= Words_.Count))
                            {
                                Tags_.RemoveRange(nStart + 1, nSize - 1);
                                Words_.RemoveRange(nStart + 1, nSize - 1);
                            }

                        }
                    }
                }
            }
            // Regular expression check
            for (int x = 0; x < Words_.Count; x++)
            {
                string curWord = Words_[x];
                foreach (KeyValuePair<string, string> pair in Patterns)
                {
                  if (Regex.IsMatch(curWord, pair.Key))
                  {
                       Tags_[x]= pair.Value;
                       break;
                  }
                }
            }
            // Patterns
            foreach (KeyValuePair<string, string> pair in TagPatterns)
            {
                string[] tags = pair.Key.ToString().Split('|');
                string EntityValue = "";
                int nSize = tags.Length;
                int nStart = Tags_.IndexOf(tags[0]);
                if (nStart >= 0)
                {
                    if (nStart + nSize <= Tags_.Count)      // Do we have enough words left to fill pattern?
                    {
                        EntityValue += Words_[nStart];
                        bool FoundIt = true;
                        int TagPos = 1;
                        for (int x = nStart + 1; x < nStart + nSize; x++)
                        {
                            if (Tags_[x] != tags[TagPos])
                            {
                                FoundIt = false;
                                break;
                            }
                            EntityValue += " " + Words_[x];
                            TagPos++;
                        }

                        if (FoundIt)
                        {
                            Tags_[nStart] = pair.Value;
                            Words_[nStart] = EntityValue;
                            if (nSize > 1 && (nSize + nStart <= Words_.Count))
                            {
                                Tags_.RemoveRange(nStart + 1, nSize - 1);
                                Words_.RemoveRange(nStart + 1, nSize - 1);
                            }

                        }
                    }
                }
            }


        }
    }
}
