using System;
 
// Scripture Memorizer
//
// For the stretch challenge, whenever you hit enter it makes another word hidden
// that isn't already hidden. I also made the hiddden words keep their puntuation.

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("Proverbs", 3, 5, 6);
        string text = "Trust in the Lord with all thine heart; and lean not unto "
            + "thine own understanding. In all thy ways acknowledge him, and he "
            + "shall direct thy paths.";
        Scripture scripture = new Scripture(reference, text);

        const int WordsToHidePerTurn = 3;
 
        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
 
            if (scripture.IsCompletelyHidden())
            {
                break;
            }
 
            Console.WriteLine("Press enter to continue or type 'quit' to finish:");
            string input = Console.ReadLine();
 
            if (input != null && input.Trim().ToLower() == "quit")
            {
                break;
            }
 
            scripture.HideRandomWords(WordsToHidePerTurn);
        }
    }
}
 