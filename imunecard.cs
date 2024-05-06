namespace cardgame
{
    internal class imunecard : cards
    {
        public imunecard(int number, string colour, bool winning) : base(number, colour, winning) { }
        public sealed override void result()
        {
            Console.WriteLine("this card will save you from losing once");
        }
    }
}
