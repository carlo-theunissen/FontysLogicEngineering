using System;
using System.Diagnostics;
using Logic;
using Logic.Decorators;
using Logic.SemanticTableaux;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            //Console CODE
            var parser = StringParser.Create(">( p, p)");
            var ope = parser.GetOperator();
            
            var tableaux = new SemanticTableauxParser(ope);
            
            Console.WriteLine("Yep, it is a: "+ tableaux.IsTautology());

        }
    }
}