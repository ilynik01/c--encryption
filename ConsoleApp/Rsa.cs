using System.Numerics;

namespace ConsoleApp;

public static class Rsa
{
    public class SharedList
    {
        //  list of primes that is used for the validation and bruteforce
        public static List<ulong> UsedPrimes = new List<ulong>();
    }
    
    public static void DoRsa()
    {
        // Recommended input
        //  p = 12107; // 7   7919  12107
        //  q = 15269; // 19  6841  15269
        
        // 1. Get primes p and q. Validate them
        ulong p;
        ulong q;
        while (true)
        {
            p = GetPrime("prime number p");
            q = GetPrime("prime number q");
            // cannot be bigger than max ulong value
            if ((p > ulong.MaxValue / q) && (q > ulong.MaxValue / p))
            {
                Console.WriteLine("ERROR! p or q is too big for program to work with it!");
            }
            else break;
        }
        
        // 2. Count the keys
        ulong n = p * q;
        var m = (p - 1) * (q - 1);

        Console.WriteLine($"p: {p}");
        Console.WriteLine($"q: {q}");
        Console.WriteLine($"n: {n}");
        Console.WriteLine($"m: {m}");
        
        // Find coprime e
        ulong e = 0;
        for (e = 2; e < m; e++)
        {
            // e coprime to m. ie GCD is 1
            if (Gcd(m, e) == 1) break;
        }
        Console.WriteLine($"e: {e}");

        // Find d
        ulong d;
        try
        {
            d = FindD(m, e);
            Console.WriteLine($"d: {d}");
        }
        catch (Exception expt)
        {
            Console.WriteLine(expt);
            throw;
        }

        Console.WriteLine($" ------- Key Generation done: ------- ");
        Console.WriteLine($" Public key: n = {n} e = {e}");
        Console.WriteLine($" Private key: n = {n} d = {d}");
        Console.WriteLine($" ------------------------------------ ");

        // 3. Get numerical message
        ulong encMessage;
        while (true)
        {
            encMessage = GetULongVal($"message. NB! It should be smaller than 'n' = {n}");
            // Message value must be number less than n.
            if (encMessage < n)
            {
                break;
            }
            Console.WriteLine("ERROR! Message value is larger than n!");
        }
        
        
        // 4.  Encryption / Decryption 
       try
       {
           ulong cipher = DoModularExponentiation(encMessage, e, n);
           Console.WriteLine($"_____ Encryption Answer ______");
           Console.WriteLine($"cipher: {cipher}");
           
           ulong decMessage = DoModularExponentiation(cipher, d, n);
           Console.WriteLine($"_____ Decryption Answer ______");
           Console.WriteLine($"plainOutput: {decMessage}");
           Console.WriteLine("______________________________________________________________________________");
       }
       catch (Exception expt)
       {
           Console.WriteLine("ERROR! " + expt.Message);
       }
 
    }
    
    
    public static bool IsPrime(ulong num)
    {
        if (num == 1)
        {
            return false;
        }
        
        foreach (ulong pr in Rsa.SharedList.UsedPrimes)
        {
            if (pr > num)
            {
                break;
            }
            
            if (num % pr == 0 && num != pr)
            {
                return false;
            }
        }
        return true;
    }


    public static ulong GetPrime(string valName)
    {
        while (true)
        {
            ulong num = GetULongVal(valName);
            
            // Validate if num is prime
            // Generate list of primes until num
            for (ulong i = 2; i <= num; i++)
            {
                if (IsPrime(i) && !SharedList.UsedPrimes.Contains(i))
                {
                    SharedList.UsedPrimes.Add(i);
                }
            }
            // Check if num exists in prime list
            if (SharedList.UsedPrimes.Contains(num))
            {
                return num;
            }
            
            Console.WriteLine($"ERROR! This number is not prime! Try again!");
        }
    }
    
    
    
    
    public static ulong GetULongVal(string valName)
    {
        while (true)
        {
            Console.Write($"Provide {valName}: ");
            string? str = Console.ReadLine();
            if (ulong.TryParse(str, out ulong val))
            {
                return val;
            }
            
            Console.WriteLine("ERROR! Invalid input of ulong! Try again!");
        }
    }
  
    public static ulong DoModularExponentiation(ulong mBase, ulong mExp, ulong mMod)
    {
        // small validation
        if (mMod == 0)
        {
            throw new ArgumentException("Modulo cannot be equal to 0");
        }
        if (mExp == 0 && mMod == 1) return 0;
        
        ulong mAns = 1;
        for (ulong i = mExp; i > 0; i--)
        {
            mAns = (mAns * mBase) % mMod;
            // Console.WriteLine("mAns = "+mAns);
        }
        
        return mAns;
    }
    
    
    public static ulong FindD(ulong m, ulong e)
    {
        for (int k = 0; k < int.MaxValue; k++)
        {
            if ((1 + (ulong)k * m) % e == 0)
            {
                return (1 + (ulong)k * m) / e;
            }
        }

        throw new ApplicationException("D not found");
    }

    public static ulong Gcd(ulong a, ulong b)
    {
        // If a < b, exchange them
        if (a < b)
        {
            (a, b) = (b, a);
        }
        // gcd(a, 0) = gcd(0, a) = |a|
        if (b == 0)
        {
            return a;
        }
            
        return Gcd(b, a % b);
    }
    
    
}