using System;
using System.Diagnostics;
using Logic;
using Logic.Decorators;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            //Console CODE
            var parser = StringParser.Create("P(|(a,b), c , d   )");
            var ope = parser.GetOperator();
            Console.WriteLine(ope.ToLogicString());
            Console.WriteLine(ope);
            Console.WriteLine(ope.HasResult());
        }
    }
}