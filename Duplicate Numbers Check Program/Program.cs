
    
namespace Duplicate_Numbers_Check_Program
{

}

    class DuplicateCheckProgram
    {
        static void Main()
        {
            Console.WriteLine("Enter a list of integers separated by spaces (e.g., 1 2 3 4):");
            string input = Console.ReadLine();

            
            string[] tokens = input.Split(' ');
            List<int> numbers = new List<int>();
            HashSet<int> uniqueNumbers = new HashSet<int>();

            try
            {
                foreach (string token in tokens)
                {
                    if (int.TryParse(token, out int num))
                    {
                        
                        if (!uniqueNumbers.Add(num))
                        {
                            throw new Exception($"Error: The number ({num}) is a duplicate! Duplicates are not allowed.");
                        }
                        numbers.Add(num);
                    }
                }

                Console.WriteLine("Success! All numbers are unique and no duplicates were found.");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }
    }
