using System;
 
// Scripture Memorizer
//

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("Proverbs", 3, 5, 6);
        string text = "Trust in the Lord with all thine heart; and lean not unto "
            + "thine own understanding. In all thy ways acknowledge him, and he "
            + "shall direct thy paths.";
        Scripture scripture = new Scripture(reference, text);
    }
}