using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqLambdaProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Console.WriteLine("\nEnter square side length (comma separated for multiple):");
                //var perimeterSq = Console.ReadLine().Trim().Split(',')
                //    .ToList().Select(x => Convert.ToInt16(x) )
                //    .Where(x => x % 2 != 1)
                //    .Select(x =>
                //    {
                //        return x * 4;
                //    }).ToList();

                //Console.WriteLine("\nSquare Perimeter List:");
                //perimeterSq.ForEach(i => Console.Write("\n{0}", i));

                //Console.WriteLine("\n\nEnter width (comma separated for multiple)");
                //int[] inputArray = { 6, 3, 4, 5, 2 }; 
                //var perimeterRg = inputArray.Where( w => w % 2 != 1)
                //    .Select(
                //        b => {
                //            var l = b + 3;
                //            return (l * 2) + (b * 2);
                //        }
                //    ).ToList();
                //perimeterRg.Sort();
                //perimeterRg.ToArray();

                //int[] output = { };
                //for( var i=0; i< perimeterRg.Count(); i++)
                //{
                //    output[i] = perimeterRg[i];
                //}


                //Console.WriteLine("\nRectangle Perimeter List:");
                //perimeterRg.ForEach(i => Console.Write("\n{0}", i));

                Console.WriteLine("\nEnter Number:");
                var numberToReverse = Convert.ToInt32(Console.ReadLine().Trim());

                var remdr = 0;
                while (numberToReverse > 0)
                {
                    remdr = (remdr * 10) + (numberToReverse % 10);
                    numberToReverse = numberToReverse / 10;
                }

                Console.Write("\nNew Number: {0}", remdr);
                Console.ReadLine();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
