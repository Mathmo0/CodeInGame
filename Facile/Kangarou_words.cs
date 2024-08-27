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
    static bool IsInString(string s_bis, string s)
    {
        if(s_bis.Length > s.Length)
            return false;

        int j = 0;

        for (int i = 0; i < s.Length && j < s_bis.Length; i++)
        {
            if (s_bis[j] == s[i])
                j++;
        }
        return (j == s_bis.Length);
    }

    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        List<string> allRes = new List<string>();
        string res = "";
        List<string> truc = new List<string>();
        List<int> already = new List<int>();

        for (int i = 0; i < N; i++)
        {
            res = "";

            string LINES = Console.ReadLine();
            List<string> list = LINES.Split(',').ToList();
            list.Sort((x, y) => string.Compare(y, x));

            for(int j = 0; j < list.Count(); j++)
            {
               truc = new List<string>();

                for(int k = 0; k < list.Count(); k++)
                {
                    if(j == k || list[k].Length > list[j].Length)
                        continue;

                    Console.Error.WriteLine("j = " + list[j] + " | k = " + list[k]);

                    if(IsInString(list[k].Trim(), list[j].Trim()))
                        truc.Add(list[k].Trim());
                }

                if(truc.Count() != 0)
                {
                    truc.Sort();
                    res = list[j] + ": " + string.Join(", ", truc);
                    allRes.Add(res.Trim());
                }
            }

            // if(res == "")
            //     if(!allRes.Any(x => x == "NONE"))
            //         allRes.Add("NONE"); 
        }

        if(allRes.Count == 0)
            Console.WriteLine("NONE");
        else
        {
            allRes.Sort();
            Console.WriteLine(string.Join('\n', allRes));
        }

        // Write an answer using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");
    }
}