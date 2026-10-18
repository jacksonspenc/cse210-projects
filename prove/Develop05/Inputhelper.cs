// So every class reads the input the same way, and prevents a crash if user types in a non number.
public static class InputHelper
{
    public static string ReadText()
    {
        string text = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(text))
        {
            if (text == null)
            {
                // Input stream ended (e.g. piped input ran out).
                return "";
            }
            Console.Write("  Please enter something: ");
            text = Console.ReadLine();
        }
        return text.Trim();
    }
 
    public static int ReadInt(int min, int max)
    {
        while (true)
        {
            string text = Console.ReadLine();
            if (text == null)
            {
                return min;
            }
 
            if (int.TryParse(text.Trim(), out int value) && value >= min && value <= max)
            {
                return value;
            }
 
            Console.Write($"  Please enter a whole number from {min} to {max}: ");
        }
    }
}