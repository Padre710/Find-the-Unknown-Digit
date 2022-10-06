using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

//To give credit where credit is due: This problem was taken from the ACMICPC-Northwest
//    Regional Programming Contest. Thank you problem writers.

//You are helping an archaeologist decipher some runes. He knows that this ancient society used a Base 10 system,
//and that they never start a number with a leading zero.
//He's figured out most of the digits as well as a few operators, but he needs your help to figure out the rest.

//The professor will give you a simple math expression, of the form

//[number][op][number]=[number]
//He has converted all of the runes he knows into digits. The only operators he knows are addition (+),
//subtraction(-), and multiplication(*), so those are the only ones that will appear. Each number will
// be in the range from -1000000 to 1000000, and will consist of only the digits 0-9, possibly a leading -,
// and maybe a few ?s. If there are ?s in an expression, they represent a digit rune that the professor
// doesn't know (never an operator, and never a leading -). All of the ?s in an expression will represent 
// the same digit (0-9), and it won't be one of the other given digits in the expression. No number will begin with a 0
// unless the number itself is 0, therefore 00 would not be a valid number.

//Given an expression, figure out the value of the rune represented by the question mark. 
//If more than one digit works, give the lowest one. If no digit works, well, that's bad news for the professor -
//it means that he's got some of his runes wrong. output -1 in that case.

//Complete the method to solve the expression to find the value of the unknown rune.
//The method takes a string as a paramater repressenting the expression and will return an int value representing
//the unknown rune or -1 if no such rune exists.

namespace Find_the_Unknown_Digit
{
    public class Runes
    {
        public static int solveExpression(string u)
        {
            int[] crazy = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };
            int ans = -1;


            // LIMITING THE ARRAY TO VALUES NOT ALREADY IN THE STRING.

            foreach (var x in u)
            {
                int q = (int)x - 48;

                if (crazy.Contains(q))
                {
                    crazy = crazy.Where(val => val != q).ToArray();
                }
            }

            // REPLACING THE '?' WITH A VALUE FROM THE ARRAY.

            foreach (var x in crazy)
            {
                string p = removespace(u);
                p = replace(p, x.ToString());


                if (operators(p) == "+")
                {
                    if ((beforeOp(p) + afterOp(p) == aftereq(p)) && isnotzero(p) == true)
                    {
                        ans = x;
                        break;
                    }
                }
                else if (operators(p) == "*")
                {
                    if ((beforeOp(p) * afterOp(p) == aftereq(p)) && isnotzero(p) == true)
                    {
                        ans = x;
                        break;
                    }
                }
                else
                {
                    if ((beforeOp(p) - afterOp(p) == aftereq(p)) && isnotzero(p) == true)
                    {
                        ans = x;
                        break;
                    }
                }
            }
            return ans;
        }
        public static string removespace(string u)
        {
            string str = string.Empty;
            for (int i = 0; i < u.Length; i++)
            {
                if (u[i] == ' ')
                    continue;
                else
                    str = str + u[i];
            }
            return str;

        }
        public static string replace(string u, string z)
        {
            string str = string.Empty;
            for (int i = 0; i < u.Length; i++)
            {
                if (u[i] == '?')
                    str = str + z;
                else
                    str = str + u[i];
            }
            return str;

        }
        public static string operators(string u)
        {

            if (u.Contains('+'))
                return "+";
            else if (u.Contains('*'))
                return "*";
            else
            {
                return "-";
            }
        }
        public static int beforeOp(string u)
        {
            string no1 = string.Empty;
            no1 = no1 + u[0];
            for (int i = 1; i < u.Length; i++)
            {
                if (u[i] == '*' || u[i] == '+' || u[i] == '-')
                    break;
                else
                    no1 = no1 + u[i];
            }
            int q = int.Parse(no1);
            return q;
        }
        public static int afterOp(string u)
        {


            if (u.Contains('+'))
            {
                int q = position(u, '+');
                string s = u.Substring(q + 1, (position(u, '=') - q - 1));
                int z = int.Parse(s);
                return z;

            }
            else if (u.Contains('*'))
            {
                int q = position(u, '*');
                string s = u.Substring(q + 1, (position(u, '=') - q - 1));
                int z = int.Parse(s);
                return z;
            }
            else
            {
                int q = position(u, '-');
                string s = u.Substring(q + 1, position(u, '=') - q - 1);
                int z = int.Parse(s);
                return z;
            }

        }
        public static int aftereq(string u)
        {
            string s = u.Substring(position(u, '=') + 1);
            int q = int.Parse(s);
            return q;
        }
        public static int position(string u, char z)
        {
            int answer = 0;
            char[] textarray = u.ToCharArray();


            for (int i = 1; i < u.Length; i++)
            {
                if (textarray[i].Equals(z))
                {
                    answer = i;
                    break;
                }
            }
            return answer;
        }
        public static string SbeforeOp(string u)
        {
            string no1 = string.Empty;
            no1 = no1 + u[0];
            for (int i = 1; i < u.Length; i++)
            {
                if (u[i] == '*' || u[i] == '+' || u[i] == '-')
                    break;
                else
                    no1 = no1 + u[i];
            }


            return no1;
        }
        public static string SafterOp(string u)
        {
            if (u.Contains('+'))
            {
                int q = position(u, '+');
                string s = u.Substring(q + 1, (position(u, '=') - q - 1));
                return s;

            }
            else if (u.Contains('*'))
            {
                int q = position(u, '*');
                string s = u.Substring(q + 1, (position(u, '=') - q - 1));
                return s;
            }
            else
            {
                int q = position(u, '-');
                string s = u.Substring(q + 1, position(u, '=') - q - 1);

                return s;
            }
        }
        public static string Saftereq(string u)
        {
            string s = u.Substring(position(u, '=') + 1);
            return s;
        }
        public static bool isnotzero(string u)
        {


            if ((SbeforeOp(u).Length > 1 && SbeforeOp(u)[0] == '0') || (SbeforeOp(u).Length > 2 && SbeforeOp(u)[0] == '-') && SbeforeOp(u)[1] == '0')
            {
                return false;
            }
            else if ((SafterOp(u).Length >1  && SafterOp(u)[0] == '0') || (SafterOp(u).Length > 2 && SafterOp(u)[0] == '-') && SafterOp(u)[1] == '0')
            {
                return false;
            }
            else if ((Saftereq(u).Length > 1 && Saftereq(u)[0] == '0') || (Saftereq(u).Length > 2 && Saftereq(u)[0] == '-') && Saftereq(u)[1] == '0')
            {
                return false;
            }
            else
                return true;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(Runes.solveExpression("1+1=?"));
            Console.WriteLine(Runes.solveExpression("123*45?=5?088"));
            Console.WriteLine(Runes.solveExpression("-5?*-1=5?"));
            Console.WriteLine(Runes.solveExpression("19--45=5?"));
            Console.WriteLine(Runes.solveExpression("??*??=302?"));
            Console.WriteLine(Runes.solveExpression("?*11=??"));
            Console.WriteLine(Runes.solveExpression("??*1=??"));
            Console.WriteLine(Runes.solveExpression("??+??=??"));
            // 2, 6 ,0 -1, 5, 2, 2, -1



        }
    }





}
