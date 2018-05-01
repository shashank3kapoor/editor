using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEditor
{
    class Solution
    {
        static void Main(string[] args)
        {
            var instruction = Convert.ToInt32( Console.ReadLine().Trim() );
            List<string> instructions = new List<string>();
            
            try
            {
                for (var i = 0; i < instruction; i++)
                {
                    instructions.Add( Console.ReadLine().Trim() );
                }

                IEditor editorService = new EditorService();

                for (var i=0; i<instructions.Count(); i++)
                {
                    var nextCommand = editorService.checkNextInstruction(instructions[i]);

                    switch(nextCommand)
                    {
                        case "add":
                            editorService.add();
                            break;
                        case "delete":
                            editorService.delete();
                            break;
                        case "print":
                            editorService.print();
                            break;
                        case "undo":
                            editorService.undo();
                            break;
                        default:
                            Console.WriteLine("\nUnknown Instruction encountered.");
                            break;
                    }
                }

                editorService.printCurrentList();
                Console.WriteLine("\nPress Enter to exit.");
                Console.ReadLine();

            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }

    public interface IEditor
    {
        void add();

        void delete();

        void print();

        void undo();

        string checkNextInstruction(string userIntput);

        void printCurrentList();
    }

    public class EditorService : IEditor
    {
        private Stack<string> instructionStack;
        private Stack<string> operandStack;
        private List<char> valueList;
        private string nextInstruction;

        public string checkNextInstruction(string userIntput)
        {
            nextInstruction = userIntput;
            
            if (nextInstruction.Contains("add") == true)
            {
                return "add";
            }
            else if(nextInstruction.Contains("delete") == true)
            {
                return "delete";
            }
            else if (nextInstruction.Contains("print") == true)
            {
                return "print";
            }
            else if (nextInstruction.Contains("undo") == true)
            {
                return "undo";
            }
            else
            {
                return "unknown";
            }

        }

        public void printCurrentList()
        {
            Console.WriteLine("\nCurrent List:");
            valueList.ForEach(i => Console.Write("{0}", i));
        }

        public EditorService()
        {
            instructionStack = new Stack<string>();
            operandStack = new Stack<string>();
            valueList = new List<char>();
        }
        public void add()
        {
            var currentOperand = nextInstruction.Substring(3).Trim();
            instructionStack.Push("add");
            operandStack.Push(currentOperand.Length.ToString());

            for (var j = 0; j < currentOperand.Length; j++)
            {
                valueList.Add(currentOperand[j]);
            }
        }

        public void delete()
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

        public void print()
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
                        printOutput = "Print position out-of-bound.";
                        break;
                    }
                }
                Console.WriteLine(printOutput);
            }
        }

        public void undo()
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
}
