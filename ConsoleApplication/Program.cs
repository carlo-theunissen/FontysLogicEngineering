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
            try
            {
                Console.WriteLine("Hello World!");
                

                //Console CODE
                var parser = StringParser.Create(
                    ">(!x.(@y.(|(P(x),Q(y)))),|(!u.(P(u)),!v.(Q(v))))");
             
                var ope = parser.GetOperator();
                
                
             //   Console.WriteLine( ope.Equals(parser2.GetOperator()));
                
                var tableaux = new SemanticTableauxParser(ope);

                Console.WriteLine("Yep, it is a: " + tableaux.IsTautology());
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}