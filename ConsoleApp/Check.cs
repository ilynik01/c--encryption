namespace ConsoleApp;

public class Check
{
    public static void CheckList()
    {
        Console.WriteLine("----Accumulated list of prime numbers used for validation and bruteforce:----");
        foreach (ulong t in Rsa.SharedList.UsedPrimes)
        {
            Console.Write($"{t}, ");
        }
        if (Rsa.SharedList.UsedPrimes.Count == 0)
        {
            Console.WriteLine("Currently the list is empty.");
        }
        Console.WriteLine("\n______________________________________________________________________________");
        
    }

}