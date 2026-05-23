namespace VowelCheck1
{

}

class VowelCheckProgram
{
    static void Main()
    {
        Console.WriteLine("Enter a word or a sentence to check:");
        string input = Console.ReadLine();

        try
        {
            CheckForVowels(input);
            Console.WriteLine("Success! The input string contains vowels.");
        }
        catch (Exception ex)
        {
            
            Console.WriteLine(ex.Message);
        }
    }

    // The required method
    static void CheckForVowels(string text)
    {
        
        char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

       
        bool hasVowel = text.Any(ch => vowels.Contains(ch));

        
        if (!hasVowel)
        {
            throw new Exception("Error: This string does not contain any vowels!");
        }
    }
}