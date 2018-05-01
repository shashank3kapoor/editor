using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalSimpleEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            var instruction = Convert.ToInt32(Console.ReadLine().Trim());
            List<string> instructions = new List<string>();
            Stack<string> instructionStack = new Stack<string>();
            Stack<string> operandStack = new Stack<string>();
            List<char> valueList = new List<char>();

            try
            {
                for (var i = 0; i < instruction; i++)
                {
                    instructions.Add(Console.ReadLine().Trim());
                }

                for (var i = 0; i < instructions.Count(); i++)
                {
                    var nextInstruction = instructions[i];

                    if (nextInstruction.Contains("add") == true)
                    {
                        var currentOperand = nextInstruction.Substring(3).Trim();
                        instructionStack.Push("add");
                        operandStack.Push(currentOperand.Length.ToString());

                        for (var j = 0; j < currentOperand.Length; j++)
                        {
                            valueList.Add(currentOperand[j]);
                        }
                    }
                    else if (nextInstruction.Contains("delete") == true)
                    {
                        instructionStack.Push("delete");
                        var charToDelete = Convert.ToInt16(nextInstruction.Substring(6).Trim());
                        var charDeleted = "";
                        for (var k = 0; k < charToDelete; k++)
                        {
                            var listLastPosition = valueList.Count() - 1;
                            if (listLastPosition >= 0)
                            {
                                charDeleted += valueList[listLastPosition];
                                valueList.RemoveAt(listLastPosition);
                            }
                            else
                            {
                                break;
                            }
                        }
                        operandStack.Push(charDeleted);
                    }
                    else if (nextInstruction.Contains("print") == true)
                    {
                        var printCharLength = Convert.ToInt16(nextInstruction.Substring(5).Trim());
                        var valueListLength = valueList.Count();
                        if (printCharLength > 0 && valueListLength > 0)
                        {
                            var printOutput = "";
                            for (var l = printCharLength; l > 0; l--)
                            {
                                var printPosition = valueListLength - l;
                                if (printPosition >= 0)
                                {
                                    printOutput += valueList[printPosition];
                                }
                                else
                                {
                                    break;
                                }
                            }
                            Console.WriteLine(printOutput);
                        }
                    }
                    else if (nextInstruction.Contains("undo") == true)
                    {
                        var undoInstruction = instructionStack.Pop();
                        switch (undoInstruction)
                        {
                            case "add":
                                var charToDelete = Convert.ToInt16(operandStack.Pop());
                                for (var k = 0; k < charToDelete; k++)
                                {
                                    var listLastPosition = valueList.Count() - 1;
                                    if (listLastPosition >= 0)
                                    {
                                        valueList.RemoveAt(listLastPosition);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                break;

                            case "delete":
                                var currentOperand = operandStack.Pop();
                                for (var j = currentOperand.Length - 1; j >= 0; j--)
                                {
                                    valueList.Add(currentOperand[j]);
                                }
                                break;

                            default:
                                Console.WriteLine("Unknown Instruction encountered.");
                                break;
                        }
                    }


                }

                Console.WriteLine("Remaining List: ");
                valueList.ForEach(i => Console.Write("{0}", i));
                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
