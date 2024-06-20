using System.Text;

namespace ConsoleApp;


public static class Program
{
    private static bool pressed = false;
    
    static void Main(string[] args)
    {
        
        Console.CancelKeyPress += CtrlCAction;
        Console.OutputEncoding = Encoding.Unicode;
        Console.WriteLine("______________________________________________________________________________");
        
        while (true)
        {
            Console.WriteLine("Do you want to do RSA (R) or brute force (B) or see the list of primes (P)?");
            Console.Write("Answer with R, B, P or ctrl + c to exit: ");
            var choice = Console.ReadLine()?.ToUpper().Trim();
            if (choice == "R")
            {
                Console.WriteLine(">>> RSA algorithm was chosen >>>");
                Rsa.DoRsa();
            }
            else if (choice == "B")
            {
                Console.WriteLine(">>> Brute force of the RSA algorithm was chosen >>>");
                BruteForce.DoBruteForceDecryption();
            }
            else if (choice == "P")
            {
                Console.WriteLine(">>> Seeing the list of primes was chosen >>>");
                Check.CheckList();
            }
            else
            {
                Console.WriteLine(">>> Bad input! Try again or ctrl+c to exit. <<<");
            }
        }
    }
    
    private static void CtrlCAction(object sender, ConsoleCancelEventArgs args)
    { 
        Console.Write("\nExiting. Goodbye");
        pressed = true;
        Environment.Exit(0);
    }
    
    
}

    
    
    
    
    
    
    
    
    
    
// n - modulus