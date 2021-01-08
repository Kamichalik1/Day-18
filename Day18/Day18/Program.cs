using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {
        public struct ParenthsisData
        {
            public List<string> Contents;
            public int LastParenthesisIndex;            

            public ParenthsisData(List<string> Contents, int LastParenthesisIndex)
            {
                this.LastParenthesisIndex = LastParenthesisIndex;
                this.Contents = Contents;
            }

        }

        static void Main(string[] args)
        {

            

            string[] Lines = System.IO.File.ReadAllLines(@"E:\Advent Code\Day 18\Data.txt");


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
                    CleanedData = CleanedData.Remove(0,1);
                }
                Total += Compile(CleanedData.Split(" ".ToCharArray()));
                Console.WriteLine(Compile(CleanedData.Split(" ".ToCharArray())));
            }
            Console.WriteLine(Total);
            Console.Read();
            
            
            long Compile(string[] Data)
            {
                long Value;
                int StartIndex = 0;

                if (Data[0] == "(")
                {
                    ParenthsisData parenthsisData = GetParenthesisContents(Data, 1);
                    Value = Compile(parenthsisData.Contents.ToArray());
                    StartIndex = parenthsisData.LastParenthesisIndex;
                }
                else
                {
                    Value = Convert.ToInt32(Data[0]);
                }
                
                for (int i = StartIndex; i < Data.Length; i++)
                {                    
                    if (Data[i] == "+")
                    {
                        if (Data[i + 1] != "(")
                        {
                            Value += Convert.ToInt32(Data[i + 1]);
                        }
                        else
                        {
                            ParenthsisData parenthsisData = GetParenthesisContents(Data, i + 2);
                            i = parenthsisData.LastParenthesisIndex;
                            Value += Compile(parenthsisData.Contents.ToArray());                            
                        }
                    }
                    else if (Data[i] == "*")
                    {
                        if (Data[i + 1] != "(")
                        {
                            Value *= Convert.ToInt32(Data[i + 1]);
                        }
                        else
                        {
                            ParenthsisData parenthsisData = GetParenthesisContents(Data, i + 2);
                            i = parenthsisData.LastParenthesisIndex;
                            Value *= Compile(parenthsisData.Contents.ToArray());
                        }
                    }
                }
                return Value;
            }

            ParenthsisData GetParenthesisContents(string[] Data, int StartIndex)
            {
                List<string> ParenthesisContents = new List<string>();
                int OpeningsCounter = 1;
                int ClosingsCounter = 0;
                for (int j = StartIndex; j < Data.Length; j++)
                {
                    if (Data[j] == "(")
                    {
                        OpeningsCounter++;

                    }
                    else if (Data[j] == ")")
                    {
                        ClosingsCounter++;
                    }


                    if (ClosingsCounter == OpeningsCounter)
                    {
                        return new ParenthsisData(ParenthesisContents, j);
                    }
                    else
                    {
                        ParenthesisContents.Add(Data[j]);
                    }
                }
                return new ParenthsisData(ParenthesisContents, StartIndex);
            }

        }
    }
}
