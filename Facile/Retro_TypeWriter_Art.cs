using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        List<string> T = Console.ReadLine().Split(' ').ToList();
        string[] particular = new string[]{"sp", "bS", "sQ", "nl"};
        string res = "";

        foreach(var c in T)
        {
            Console.Error.WriteLine("string = " + c);
            if(particular.ToList().Any(s => c.Contains(s)))
            {
                string p = particular.ToList().First(s => c.Contains(s));
                int temp = c.IndexOf(p);
                string how = c.Substring(0, temp);
                how = how == "" ? "1" : how;
                Console.Error.WriteLine("number = " + how);
                string aurevoir = "";
                switch(p)
                {
                    case "sp":
                        aurevoir = new string(' ', int.Parse(how));
                        break;

                    case "bS":
                        aurevoir = new string('\\', int.Parse(how));
                        break;

                    case "sQ":
                        aurevoir = new string('\'', int.Parse(how));
                        break;

                    case "nl":
                        aurevoir = new string('\n', int.Parse(how));
                        break;
                    default:
                        res += "pipi";
                        break;
                }
                res += aurevoir;
            }
            else
            {
                string how = c.Substring(0, c.Length - 1);
                //Console.Error.WriteLine("number = " + how);
                string bonjour = new string(c[c.Length - 1], int.Parse(how));
                res += bonjour;
            }
        }

        Console.WriteLine(res);
    }
}