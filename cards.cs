namespace cardgame
{
    internal class cards
    {
        int number;
        string colour;
        bool winning;
        public cards(int number, string colour, bool winning)
        {
            this.number = number;
            this.colour = colour;
            this.winning = winning;
        }

        public int Getnumber() { return number; }
        public string Getcolour() { return colour; }
        public bool Getwinning() { return winning; }

        public void setnumber(int num) { number = num; }
        public void setcolour(string col) { colour = col; }
        public void setwinning(bool iswin)
        { winning = iswin; }
        public virtual void result()
        {
            if (winning) { Console.WriteLine("winner!!!"); }
            else { Console.WriteLine("LOSER!!!"); }
        }
    }
}
