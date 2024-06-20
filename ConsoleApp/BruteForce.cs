namespace ConsoleApp;


public class BruteForce
{
     public static void DoBruteForceDecryption()
     {
         ulong n;
         ulong p;
         ulong q;
         while (true)
         {
             try
             {
                 n = Rsa.GetULongVal("number n");
                 p = GetPrimes(n);
                 q = n / p;
                 break;
             }
             catch (Exception expt)
             {
                 Console.WriteLine(expt);
                 Console.WriteLine("Try again!");
             }
         }

         ulong m = (p - 1) * (q - 1);
         ulong e;
         while (true)
         {
             e = Rsa.GetULongVal("number e");
             if (Rsa.Gcd(m, e) == 1)
             { 
                 break;
             }
             Console.WriteLine("Invalid number e provided! Try again!");
         }
         Console.WriteLine($"m: {m}");
         Console.WriteLine($"e: {e}");

 
         
         // Find d
         ulong d;
         try
         {
             d = Rsa.FindD(m, e);
             Console.WriteLine($"d: {d}");
         }
         catch (Exception expt)
         {
             Console.WriteLine(expt);
             throw;
         }
         
         Console.WriteLine($" ------- Key Breaking is done: ------- ");
         Console.WriteLine($" Private key: n = {n} d = {d}");
         Console.WriteLine($" ------------------------------------ ");
         
         
         ulong cipher = Rsa.GetULongVal("cypher value");
         try
         {
             ulong plainOutput = Rsa.DoModularExponentiation(cipher, d, n);
             Console.WriteLine($"_____ Bruteforced message ______");
             Console.WriteLine($"message: {plainOutput}");
             Console.WriteLine("______________________________________________________________________________");
           
         }
         catch (Exception  expt)
         {
             Console.WriteLine("ERROR! " + expt.Message);
         }

     }
     
     
     
     
     
     private static ulong GetPrimes(ulong p1p2)
     {
        // generate list of prime numbers until sqrt(p1p2)
        for (ulong i = 2; i <= (ulong)Math.Sqrt(p1p2); i++)
        {
            if (Rsa.IsPrime(i) && !Rsa.SharedList.UsedPrimes.Contains(i))
            {
                Rsa.SharedList.UsedPrimes.Add(i);
            }
        }
        
        //  divide p1p2 by each prime number in the list until result is the second prime number
        ulong pr1 = 0;
        ulong pr2 = 0;

        foreach (var pr in Rsa.SharedList.UsedPrimes)
        {
            if (pr > (ulong)Math.Sqrt(p1p2))
            {
                break;
            }
            
            if (p1p2 % pr == 0)
            {
                pr1 = pr;
                pr2 = p1p2 / pr;
                if (!Rsa.IsPrime(pr2))
                {
                    pr1 = 0;
                }
                break;
            }
        }
        
        if (pr1 != 0)
        {
            Console.WriteLine($"--- Intermediate Bruteforce Results ---");
            Console.WriteLine($"n = {p1p2}");
            Console.WriteLine($"number n is made of:");
            Console.WriteLine($"p = {pr1}");
            Console.WriteLine($"q = {pr2}");
            Console.WriteLine($"ie {pr1} * {pr2} = {p1p2}");
        }
        else
        {
            throw new ArgumentException ($"ERROR! Invalid n value. Not a prime number multiplication result!");
        }
        return pr1;
     }
     
         


    

}