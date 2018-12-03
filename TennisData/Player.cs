namespace TennisData
{
    /// <summary>
    /// Player 
    /// </summary>
    public class Player
    {
        public string FullName { get; set; }
        public string Country { get; set; }
        public int Seed { get; set;  }
        public string FirstName
        {
            get
            {
                string[] names = FullName.Split(' ');
                return names[0];
            }
        }
        public string LastName
        {
            get
            {
                string[] names = FullName.Split(' ');
                return names[names.Length - 1];
            }
        }

    }
}
