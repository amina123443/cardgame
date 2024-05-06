namespace cardgame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("this is a simple card game which uses classes and objects in c#\nfirstly you cerate a deck which can go up from 2 card to 10 cards and pick a selected colour and from 1 to 1 less of the cards in the deck\nif you want to,you can add in a immunity card that we explain later\npretty much from the deck, if you pick a losing card you lose, and if you pick a winning card you win\nif you add an immunity card and you picked it, you have to pick another card but if that card is a losing card, you won't lose and instead you pick another card\nthere can only be a maximen of one immunity card and it can only save you from one lost.");
            Console.WriteLine("press enter to start a new deck");
            Console.ReadLine();
            while (true)
            {
                NewDeck();
                Console.Clear();
            }
        }

        public static bool TestStringForInt(string x)//tests if the string only contains numbers
        {
            try
            {
                int test = Convert.ToInt16(x);
            }
            catch (Exception)

            {
                return false;
            }
            return true;
        }
        public static void NewDeck()
        {
            deck Deck = new(null, 0, 0);//leaves propity emptys for edits

            bool valid;
            do
            {
                Console.WriteLine("what colour should the deck be, there is:\nred\nblue\npurple\nenter the colour name listed and only the colour name");
                string UserColour = Console.ReadLine();
                valid = Deck.setcolour(UserColour);
                if (valid == false)
                {
                    Console.WriteLine("this is an invalid input");
                }
            }
            while (!valid);
            valid = false;
            do
            {
                int usermax;
                Console.WriteLine("how many cards will be in the deck, has to be between 3 cards and 10 cards inclusively and only input a whole number");
                string userMax = Console.ReadLine();
                if (TestStringForInt(userMax) == true)
                {
                    usermax = Convert.ToInt16(userMax);
                }
                else
                {
                    Console.WriteLine("only a whole number, like '4'");
                    continue;
                }

                valid = Deck.SetMax(usermax);
                if (valid == false)
                {
                    Console.WriteLine("this input is out of range");
                }

            }
            while (!valid);
            valid = false;
            int cards;
            do
            {
                Console.WriteLine("how many winning cards will be in the deck\nhas to be between 1,to one less your total cards you just inputed inclusively, in whole number only");
                string usercard = Console.ReadLine();
                if (TestStringForInt(usercard) == true)
                {
                    cards = Convert.ToInt16(usercard);
                }
                else
                {
                    Console.WriteLine("whole numbers only remmeber");
                    continue;
                }
                valid = Deck.SetWinningCards(cards);
                if (valid == false)
                {
                    Console.WriteLine("this card is out limit, to remind you,you have {0} cards in the deck", Convert.ToString(Deck.getMax()));
                }

            }
            while (!valid);
            bool End = false;
            if (Deck.getMax() - Deck.getwinningcards() != 1)//this is so there will always be 1 losing card
            {
                while (!End)
                {

                    Console.WriteLine("do you want to add an immunity card, type 'yes' if you do\ntype 'no' if you don't want to add an immunity card");
                    string option = Console.ReadLine().ToLower();
                    switch (option)
                    {
                        case "yes":
                            do
                            {
                                card(Deck, -1); //-1 means true
                            }
                            while (end() == true);
                            End = true;
                            break;
                        case "no":
                            do
                            {
                                card(Deck, 0);//0 means false
                            }
                            while (end() == true);
                            End = true;
                            break;
                        default:
                            Console.WriteLine("invalid input, try again");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("no space for an immunity card");     
                    do
                    {
                        card(Deck, 0);//0 means false
                    }
                    while (end() == true);
            }


        }

        public static bool end()//instead of using repitison, we return a boolian so that we cerate new sets of card without cerating more of the same procedures taking up memory space
        {
            while (true)
            {
                Console.WriteLine("would you like to play again, type 'yes' if you do\ntype 'no' if you don't");
                string option = Console.ReadLine().ToLower();
                if (option == "yes")
                {
                    while (true)
                    {
                        Console.WriteLine("would you like to use the same deck or a new deck\ntype 'new' for a new deck\n type 'old' to use the current deck");
                        string final = Console.ReadLine().ToLower();
                        if (final == "new")
                        {
                            return false;
                        }
                        else if (final == "old")
                        {
                            return true;
                        }
                        else { Console.WriteLine("this is an invalid input"); }

                    }
                }
                else if (option == "no")
                {
                    System.Environment.Exit(0);

                }
                else
                {
                    Console.WriteLine("this is an invalid input");
                }
            }
        }
        public static void card(deck Unboxing, int imunity)
        {
            Random rng = new();
            int max = Unboxing.getMax();
            cards[] pile = new cards[max];
            imunecard imun = new(-1, null, false);// propity will change soon
            List<int> cardNumbers = new();//to display and check avilable cards
            int empty = max;// how many cards has yet to be asigned a value
            int winning = Unboxing.getwinningcards();
            int imuNumber = -1;//default if there is none
            if (imunity == -1)
            {
                imuNumber = rng.Next(0, max);
                empty = empty - 1;
                imun.setnumber(imuNumber);
                imun.setcolour(Unboxing.getcolour());
            }

            for (int i = 0; i < max; i++)
            {
                cards card = new(0, null, false);
                string colour = Unboxing.getcolour();
                card.setcolour(colour);
                if (i == imuNumber)
                {
                    card.setnumber(-1);//to easily identify the immunity card with the normal cards when debuging 
                    cardNumbers.Add(i);
                    pile[i] = card;
                    continue;
                }
                card.setnumber(i);
                if ((rng.Next(0, 2) == 1 && winning > 0) || empty == winning)
                {
                    card.setwinning(true);

                }
                else
                {
                    card.setwinning(false);
                }
                cardNumbers.Add(i);
                pile[i] = card;
            }

            game(pile, cardNumbers, imun);

        }

        public static void game(cards[] pile, List<int> cardNumbers, imunecard imun)
        {
            List<int> current = cardNumbers;
            bool removed = false;
            bool immune = false;
            int guess;
            while (true)
            {
                Console.WriteLine("pick a card number from this pile, type one of the number to pick the card and only a number");
                foreach (int i in current)
                {
                    Console.WriteLine(Convert.ToString(i + 1));
                }
                Console.WriteLine("");
                while (true)
                {
                    string userguess = Console.ReadLine();
                    if (TestStringForInt(userguess) == true)
                    {
                        guess = Convert.ToInt32(userguess) - 1;

                        if ((search(guess, current) == false && (guess != imun.Getnumber())|| removed==true) || guess <= -1)// the guess <=-1 is there to avoid tricking the search(guess,current) fro returning true because the user could of inputed the value 0, turning it to a -1, in a game where they added a immunity card so there will be a -1 in the list. this is to prevent from the code from crashing
                        {
                            Console.WriteLine("we could not find this card in the pile, make sure your number is in the pile list that we shown you to");
                        }
                        else
                        {
                            current.Remove(guess);
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("this is an invalid input,try again");
                    }


                }
                Console.WriteLine("the card says...");
                
                if(guess != imun.Getnumber())
                { pile[guess].result(); }

                if (guess == imun.Getnumber())
                {
                    imun.result();
                    immune = true;
                    Console.WriteLine("you can now pick 2 cards, one of them has to be a winning card or you lose");
                    removed = true;
                }
                else if (pile[guess].Getwinning() == false && immune == true)
                {
                    Console.WriteLine("but since you have an immune card, you have not lost yet, you have one last chance to find the winning card");
                    immune = false;
                }

                else
                {
                    break;
                }
            }


        }

        public static bool search(int item, List<int> cardNumbers)//linia search to check if the user input was avilable in the pile
        {
            foreach (int card in cardNumbers)
            {
                if (card == item)
                {
                    return true;
                }
            }
            return false;
        }
    }
}