namespace Citd.Game
{
    public class Player
    {
        public string Nick { get; private set; }
        public bool IsReady { get; set; }

        public Player(string nick)
        {
            Nick = nick;
        }
    }
}