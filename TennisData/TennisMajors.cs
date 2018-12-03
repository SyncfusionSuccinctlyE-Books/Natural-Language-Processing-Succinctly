using NaturalLanguageProcessing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisData
{
    /// <summary>
    /// Tennis Majors - Compute various answers from a set of all tennis grand slam tournament
    /// </summary>
    static public class TennisMajors
    {
        private static List<Tournament> TennisResults;

        private static int TournamentYear;
        private static string Tournament;
        private static string Gender;
        private static string PlayerName;
        private static string LastVerb;
        private static Random rnd;

        static TennisMajors() {
            TennisResults = new List<Tournament>();
            LoadDataSet();

            int MM = DateTime.Now.Month;
            if (MM<=2) { Tournament = "AUS"; };
            if (MM>3 && MM <= 5) { Tournament = "FRENCH"; };
            if (MM> 5 && MM <= 7) { Tournament = "WIMBLEDON"; };
            if (MM>7) { Tournament = "USOPEN"; };
            Gender = "B";
            rnd = new Random();
            LastVerb = "WON";
        }

        static public string GetResponse(List<string> Words_,List<string> Tags_)
        {
            string ans_ = "";
            string GuessYear = "LAST";
            string curWord = "";
            for (int x = 0; x < Tags_.Count; x++)
            {
                curWord = Words_[x].Trim().ToLower();
                if (Tags_[x] == "YEAR")
                {
                    TournamentYear = Convert.ToInt16(Words_[x]);
                    GuessYear = "";
                }
                if (Tags_[x] == "EVENT")
                {
                    Tournament = Words_[x];
                    if (Tournament.Contains("WIMBLEDON")) { Tournament = "WIMBLEDON"; }
                    if (Tournament.Contains("FRENCH")) { Tournament = "FRENCH"; }
                    if (Tournament.Contains("US ")) { Tournament = "USOPEN"; }
                    if (Tournament.Contains("AUS ")) { Tournament = "AUS"; }
                }
                if (Tags_[x].StartsWith("VB"))
                {
                    LastVerb = Words_[x].ToUpper();
                }
                if (Tags_[x] == "PERSON")
                {
                    PlayerName = Words_[x].ToUpper();
                }
                if ("|first|earliest|".IndexOf(curWord)>=0)
                {
                    GuessYear = "FIRST";
                }
                if (Tags_[x]=="RB" && curWord=="most" )
                {
                    LastVerb = "MOST:" + LastVerb;
                }
            }
            if (GuessYear=="FIRST")
            {
                TournamentYear = 0;
            }
            if (GuessYear == "LAST")
            {
                TournamentYear = 9999;
            }


            // Simple questions (who won or who lost)
            if (LastVerb == "WON")
            {
                if (Gender == "B")
                {
                    ans_ = TennisData.TennisMajors.WhoWon(Tournament, TournamentYear, "M");
                    string ansW = TennisData.TennisMajors.WhoWon(Tournament, TournamentYear, "F");
                    if (ansW.Length>0) { ans_ += " and " + ansW; }
                }
                else
                {
                    ans_ = TennisData.TennisMajors.WhoWon(Tournament, TournamentYear, Gender);
                }
            }
            if (LastVerb == "LOST")
            {
                if (Gender == "B")
                {
                    ans_ = TennisData.TennisMajors.WhoLost(Tournament, TournamentYear, "M");
                    string ansW = TennisData.TennisMajors.WhoLost(Tournament, TournamentYear, "F");
                    if (ansW.Length > 0) { ans_ += " and " + ansW; }
                }
                else
                {
                    ans_ = TennisData.TennisMajors.WhoLost(Tournament, TournamentYear, Gender);
                }
            }
            if (LastVerb.StartsWith("MOST"))
            {
                string[] keys_ = LastVerb.Split(':');
                if (keys_.Length==1)
                {
                    LastVerb = "WON";
                }
                else
                {
                    LastVerb = keys_[1];
                }
                if (LastVerb == "WON") {
                    ans_ = MostWins(Tournament, "M");
                }
                if (LastVerb == "LOST")
                {
                    ans_ = MostLosses(Tournament, "M");
                }

            }

            if (ans_.Length < 1) { ans_ = "I don't know..."; }

            return ans_;
        }

        static public string WhoWon(string Tournament, int Year, string Gender)
        {
            string[] PossibleReplies = {
                      "{0} was the {1}'s winner",
                      "{0} won the {1}'s",
                      "{0} won on the {1}'s side",
                      "{0} defeated {2} in the {1}'s draw",
                      "{0} won in {3} sets over {2}",
                      "{0} won in a {4} match over {2}"
                    };
            string ans_ = "";
            Tournament Results_ = GetResults(Tournament, Year, Gender);
            if (Results_ != null)
            {
                string GenderText = "men";
                if (Gender=="F") { GenderText = "women"; }
                int reply = rnd.Next(1, PossibleReplies.Length) - 1;

                string SetText = Results_.SetsPlayed.ToString();
                // Speak some tennis vocabulary
                if (Results_.SetsPlayed == 3 && Gender=="M") { SetText = "straight"; }
                if (Results_.SetsPlayed == 2 && Gender == "F") { SetText = "straight"; }
                int TotalGamesWon = GamesWon(Results_.FinalScore);
                int TotalGamesLost = GamesLost(Results_.FinalScore);
                string MatchRating = MatchRate(SetText == "straight", TotalGamesWon, TotalGamesLost);

                reply = 5;
                

                ans_ = string.Format(PossibleReplies[reply], 
                       Results_.Winner.FullName, 
                       GenderText,
                       Results_.RunnerUp.FullName,
                       SetText,
                       MatchRating);
            }
            return ans_;
        }

        static public string WhoLost(string Tournament, int Year, string Gender)
        {
            string[] PossibleReplies = {
                      "{0} lost on the {1}'s side",
                      "{0} lost the {1}'s draw",
                      "{0} lost in the {1}'s final"
                    };
            string ans_ = "";
            Tournament Results_ = GetResults(Tournament, Year, Gender);
            if (Results_ != null)
            {
                string GenderText = "men";
                if (Gender == "F") { GenderText = "women"; }

                int reply = rnd.Next(1, PossibleReplies.Length) - 1;
                ans_ = string.Format(PossibleReplies[reply], Results_.RunnerUp.FullName, GenderText);
            }
            return ans_;
        }

        static public int GamesWon(string Scores)
        {
            int TotalWon = 0;
            string[] Sets_ = Scores.Split(',');
            foreach(string CurSet in Sets_)
            {
                string[] WinLoss = CurSet.Split('–');

                if (WinLoss.Count()==2)
                {
                    TotalWon += Convert.ToInt16(WinLoss[0]);
                }
            }
            return TotalWon;
        }
        static public int GamesLost(string Scores)
        {
            int TotalLost = 0;
            string[] Sets_ = Scores.Split(',');
            foreach (string CurSet in Sets_)
            {
                string[] WinLoss = CurSet.Split('–');
                if (WinLoss.Count() == 2)
                {
                    // Deal with tiebreakers
                    int x = WinLoss[1].IndexOf("(");
                    if (x>0) { WinLoss[1] = WinLoss[1].Substring(0, x - 1); }
                    TotalLost += Convert.ToInt16(WinLoss[1]);
                }
            }

            return TotalLost;
        }
        static public string MatchRate(bool StraightSets,int GamesWon,int GamesLost)
        {
            string ans_ = "Good";
            if(!StraightSets)
              {  ans_ = "close";
                 if (GamesWon-GamesLost<4) { ans_ = "extremely close"; }
              }
            else
            {
                if (GamesWon > GamesLost*2.5)
                {
                    ans_ = "one-sided";
                }
            }
            return ans_;
        }

        static public string PlayerWins(string PlayerName,string Tournament)
        {
            string ans = PlayerName + " has never won.";
            int NumTimes_ = TennisResults.Where(x => x.Name.ToLower() == Tournament.ToLower() 
                            && x.Winner.FullName.ToUpper() == PlayerName.ToUpper()).Count();
            if (NumTimes_>0)
            {
                if (NumTimes_ == 1) { ans = PlayerName + " has won once"; }
                if (NumTimes_ == 2) { ans = PlayerName + " has won twice"; }
                if (NumTimes_>2) { ans = PlayerName + " has won " + NumTimes_.ToString() + " times"; }
            }
            return ans;
        }
        static public string PlayerLosses(string PlayerName, string Tournament)
        {
            string ans = PlayerName + " has never reached the final.";
            int NumWins_ = TennisResults.Where(x => x.Name.ToLower() == Tournament.ToLower()
                            && x.Winner.FullName.ToUpper() == PlayerName.ToUpper()).Count();
            int NumLost_ = TennisResults.Where(x => x.Name.ToLower() == Tournament.ToLower()
                            && x.RunnerUp.FullName.ToUpper() == PlayerName.ToUpper()).Count();

            // At least player reached the final
            if (NumWins_+NumLost_ > 0)
            {
                if (NumLost_ < 1) { ans = PlayerName + " has never lost in the finals"; }
                if (NumLost_ == 1 && NumWins_ < 1) { ans = PlayerName + " lost all the only time they reached the final"; }
                if (NumLost_ > 1 && NumWins_ < 1) { ans = PlayerName + " lost all " + (NumLost_).ToString() + " times they played"; }
                if (NumLost_ > 0 && NumWins_ > 0) { ans = PlayerName + " lost " + (NumLost_) + " times in " + (NumLost_ + NumWins_).ToString()+" trips to the finals"; }
            }
            return ans;
        }


        static Tournament GetResults(string Tournament, int Year, string Gender)
        {
            if (Year < 1)
            {
                Tournament FirstOne = TennisResults.Where(x => x.Name.ToLower() == Tournament.ToLower()).OrderBy(x => x.Year).First();
                Year = FirstOne.Year;
            }
            if (Year >9999)
            {
                Tournament LastOne = TennisResults.Where(x => x.Name.ToLower() == Tournament.ToLower()).OrderBy(x => x.Year).Last();
                Year = LastOne.Year;
            }


            Tournament Fnd = TennisResults.FirstOrDefault(x => x.Year == Year && x.Name == Tournament && x.Gender == Gender);
            return Fnd;

        }
        static public string FinalScore(string Tournament, int Year, string Gender)
        {
            string ans_ = "";
            Tournament Results_ = GetResults(Tournament, Year, Gender);
            if (Results_ != null)
            {
                ans_ = Results_.FinalScore;
            }
            return ans_;
        }
        static public string MostWins(string Tournament,string Gender)
        {
            string Winningest = "";
            var ans_ = TennisResults.Where(x => x.Name.ToLower() == Tournament.ToLower() && x.Gender == Gender).
                          GroupBy(x => x.Winner.FullName, (key, values) => new { Player = key, Count = values.Count() }).OrderByDescending(y=>y.Count);
            var MostWins = ans_.FirstOrDefault();
            if (MostWins != null)
            {
                Winningest = MostWins.Player + " has won " + MostWins.Count + " times";
            }
            return Winningest;
        }
        static public string MostLosses(string Tournament, string Gender)
        {
            string Losses = "";
            var ans_ = TennisResults.Where(x => x.Name.ToLower() == Tournament.ToLower() && x.Gender == Gender).
                          GroupBy(x => x.RunnerUp.FullName, (key, values) => new { Player = key, Count = values.Count() }).OrderByDescending(y => y.Count);
            var MostLosses = ans_.FirstOrDefault();
            if (MostLosses != null)
            {
                Losses = MostLosses.Player + " has lost " + MostLosses.Count + " times";
            }

            return Losses;
        }
        static public void LoadDataSet()
        {
            if (TennisResults.Count < 1)
            {
                List<string> Players = new List<string>();
                TennisResults = new List<Tournament>();
                string[] lines = System.IO.File.ReadAllLines(@"Tennis.txt");
                for (var x = 0; x < lines.Length; x++)
                {
                    string[] CurrentData = lines[x].Split('|');
                    Tournament CurTournament = new Tournament
                    {
                        Name = CurrentData[0].ToUpper().Trim(),
                        Gender = CurrentData[1].ToUpper().Trim(),
                        Year = Convert.ToInt16(CurrentData[2]),
                        FinalScore = CurrentData[9],
                        Winner = new Player(),
                        RunnerUp = new Player()
                    };
                    CurTournament.Winner.FullName = CurrentData[3].Trim();
                    CurTournament.Winner.Seed = Convert.ToInt16(CurrentData[4]);
                    CurTournament.Winner.Country = CurrentData[5].Trim();

                    CurTournament.RunnerUp.FullName = CurrentData[6].Trim();
                    CurTournament.RunnerUp.Seed = Convert.ToInt16(CurrentData[7]);
                    CurTournament.RunnerUp.Country = CurrentData[8].Trim();
                    TennisResults.Add(CurTournament);
                    Players.Add(CurTournament.Winner.FullName);
                    Players.Add(CurTournament.RunnerUp.FullName);
                }
                var UniquePlayers = Players.Distinct();
                foreach (string FullName in UniquePlayers)
                {
                    Entities.NamedEntities.Add(FullName, "PERSON");
                }

            }
        }

    }
}
