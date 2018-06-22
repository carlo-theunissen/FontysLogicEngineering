using System;
using System.Diagnostics;
using Logic;
using Logic.Abstract;
using Logic.Decorators;
using Logic.SemanticTableaux;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                

                //Console CODE
                var parser = StringParser.Create("@x.(P(x,|(x,D(d,xgi)))");
             
                var ope = parser.GetOperator();
                (ope as AbstractQuantifierOperator).ChangeVariable('z');
                
                Console.WriteLine(ope.ToLogicString());
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}