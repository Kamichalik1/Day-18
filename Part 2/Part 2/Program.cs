using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part_2
{
    class Program
    {
        static List<string> RPNGenerator(List<string> Tokens, Dictionary<string,int> Precedences)
        {
            List<string> Result = new List<string>();
            Stack<string> OperatorStack = new Stack<string>();

            for (int i = 0; i < Tokens.Count; i++)
            {
                int Token = 0;
                if (int.TryParse(Tokens[i], out Token))
                {
                    Result.Add(Tokens[i]);
                }
                else
                {
                    if (Tokens[i] != "(" && Tokens[i] != ")")
                    {                        
                        while (OperatorStack.Count > 0 && OperatorStack.Peek() != "(" && Precedences[OperatorStack.Peek()] >= Precedences[Tokens[i]])
                        {
                            Result.Add(OperatorStack.Pop());
                        }                        
                        OperatorStack.Push(Tokens[i]);
                    }
                    else if (Tokens[i] == "(")
                    {                        
                        OperatorStack.Push(Tokens[i]);     
                    }
                    else
                    {
                        while (OperatorStack.Peek() != "(")
                        {
                            Result.Add(OperatorStack.Pop());
                        }
                        OperatorStack.Pop();
                    }
                }

            }
            while (OperatorStack.Count > 0)
            {
                Result.Add(OperatorStack.Pop());
            }

            return Result;
        }

        static long RPNCalc(List<string> Tokens)
        {
            Stack<long> Stack = new Stack<long>();

            for (int i = 0; i < Tokens.Count; i++)
            {
                int Token = 0;
                if (int.TryParse(Tokens[i], out Token))
                {
                    Stack.Push(Token);
                }
                else
                {
                    long Operand1 = Stack.Pop();
                    long Operand2 = Stack.Pop();
                    long Result = 0;

                    if (Tokens[i] == "+")
                    {
                        Result = Operand1 + Operand2;
                    }
                    else if (Tokens[i] == "-")
                    {
                        Result = Operand2 - Operand1;
                    }
                    else if (Tokens[i] == "*")
                    {
                        Result = Operand1 * Operand2;
                    }
                    else if (Tokens[i] == "/")
                    {
                        Result = Operand2 / Operand1;
                    }

                    Stack.Push(Result);
                    
                }
            }

            return Stack.Pop();
        }

        static void Main(string[] args)
        {

            string[] Lines = System.IO.File.ReadAllLines(@"E:\Advent Code\Day 18\Data.txt");

            Dictionary<string, int> Precedences = new Dictionary<string, int>();
            Precedences.Add("+", 2);
            Precedences.Add("*", 1);

            long Total = 0;

            for (int i = 0; i < Lines.Length; i++)
            {
                string CleanedData = "";
                for (int j = 0; j < Lines[i].Length; j++)
                {
                    if (Lines[i][j] == '(' || Lines[i][j] == ')')
                    {
                        CleanedData += " ";
                        CleanedData += Lines[i][j].ToString();
                        CleanedData += " ";
                    }
                    else
                    {
                        CleanedData += Lines[i][j].ToString();
                    }
                }
                
                CleanedData = CleanedData.Replace("  ", " ");
                if (CleanedData[0] == ' ')
                {
                    CleanedData = CleanedData.Remove(0, 1);
                }
                if (CleanedData[CleanedData.Length-1] == ' ')
                {
                    CleanedData = CleanedData.Remove(CleanedData.Length-1, 1);
                }
                
                List<string> Tokens = new List<string>(CleanedData.Split(" ".ToCharArray()));
                List<string> RPN = RPNGenerator(Tokens, Precedences);
                long Result = RPNCalc(RPN);
                Total += Result;

            }

            Console.WriteLine(Total);
            Console.ReadKey();

        }

       

    }
}
