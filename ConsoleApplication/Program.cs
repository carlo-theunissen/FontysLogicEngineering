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
            var parser = StringParser.Create(">(A,&(>(B,=(A,E)),~(D)))");


            var table = new SimplifiedTruthTableCreator(parser);


            var decorator = new NandifyDecorator(parser.GetOperator());


            var processed = new SimplifiedTruthTableCreator(decorator);


            foreach (var row in table.GetTable())
            {
                foreach (var colomn in row)
                    Console.Write(colomn);
                Console.WriteLine("");
            }

            Console.WriteLine("================");

            foreach (var row in processed.GetTable())
            {
                foreach (var colomn in row)
                    Console.Write(colomn);
                Console.WriteLine("");
            }

            Console.WriteLine(decorator.GetOperator().ToString());
        }
    }
}