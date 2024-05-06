namespace cardgame
{
    sealed class deck
    {
        string colour;
        int winningcards;
        int max;

        public deck(string Colour, int Winningcards, int Max)
        {
            colour = Colour;
            winningcards = Winningcards;
            max = Max;

        }
        public string getcolour()
        {
            return colour;
        }

        public int getwinningcards() { return winningcards; }
        public int getMax() { return max; }
        public bool setcolour(string colour)
        {
            switch (colour)
            {
                case "red":
                    this.colour = "red";
                    return true;
                case "blue":
                    this.colour = "blue";
                    return true;
                case "purple":
                    this.colour = "purple";
                    return true;
                default: return false;
            }
        }

        public bool SetWinningCards(int Winningcards)
        {
            if (Winningcards < max && Winningcards > 0)
            {
                winningcards = Winningcards;
                return true;

            }
            else { return false; }
        }

        public bool SetMax(int Max)

        {
            if (Max > 2 && Max < 11)
            {
                max = Max;
                return true;
            }
            else { return false; }
        }
    }
}
