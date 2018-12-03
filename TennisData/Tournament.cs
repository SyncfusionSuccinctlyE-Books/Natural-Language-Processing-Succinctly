namespace TennisData
{
    /// <summary>
    /// Tournament
    /// </summary>
    public class Tournament
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string Gender { get; set; }
        public string FinalScore { get; set; }
        public Player Winner { get; set; }
        public Player RunnerUp { get; set; }
        public int SetsPlayed
        {
            get 
            {
                string[] ans_ = FinalScore.Split(',');
                return ans_.Length;
            }
        }
    }
}
