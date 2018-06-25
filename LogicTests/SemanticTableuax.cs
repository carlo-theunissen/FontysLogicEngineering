using System;
using Logic;
using Logic.SemanticTableaux;
using Xunit;

namespace LogicTests
{
    public class SemanticTableuax
    {
        private void Test(string formula, bool result)
        {
            var parser = StringParser.Create(formula);
             
            var ope = parser.GetOperator();
                
                
            //   Console.WriteLine( ope.Equals(parser2.GetOperator()));
                
            var tableaux = new SemanticTableauxParser(ope);

            Assert.Equal(result, tableaux.IsTautology());
        }

        [Fact]
        public void Easy()
        {
            Test("|(~(a), a)", true);
        }     
        [Fact]
        public void True()
        {
            Test("T", true);
        }        
        [Fact]
        public void NotFalse()
        {
            Test("~(F)", true);
        }        
        [Fact]
        public void Same()
        {
            Test("=(a, a)", true);
        }        
        
        [Fact]
        public void Mixed1()
        {
            Test("=(>(&(a,b),c),>(a,>(b,c)))", true);
        }        
        
        [Fact]
        public void Mixed2()
        {
            Test(">(&(>(~(a),b),>(~(a),~(b))),a)", true);
        }        
        
        [Fact]
        public void Nand()
        {
            Test("%(%(%(a,a),%(a,a)),%(a,a))", true);
        }        
        
        [Fact]
        public void Big()
        {
            Test(">(~(=(|(=(|(|(|(c,>(%(a,b),c)),c),d),|(%(&(c,~(a)),c),>(a,b))),a),>(c,%(a,c)))),%(c,%(&(|(&(&(a,&(c,c)),|(c,a)),~(~(a))),>(~(|(~(%(a,a)),a)),a)),|(%(a,=(%(%(a,|(d,b)),a),d)),|(~(%(&(b,d),|(b,b))),|(>(c,d),c))))))", true);
        }        
        
        [Fact]
        public void BigFalse()
        {
            Test(">(~(=(|(=(|(|(|(c,>(%(a,b),c)),c),d),|(%(&(c,~(a)),c),>(a,b))),a),>(c,%(a,c)))),%(c,%(&(|(&(&(a,&(c,c)),|(c,a)),~(~(a))),>(~(|(~(%(a,a)),a)),a)),|(%(a,=(%(%(a,|(d,b)),a),d)),|(~(%(&(b,d),|(b,b))),|(>(c,d),e))))))", false);
        }        
        
        [Fact]
        public void TrueQuantifier()
        {
            Test(">(!x.(@y.(|(P(x),Q(y)))),|(!u.(P(u)),!v.(Q(v))))", true);
        }
        
        [Fact]
        public void TrueQuantifier2()
        {
            Test(">(!x.(@y.(Q(y,x))),@z.(|(P(z),!u.(Q(z,u)))))", true);
        }
        
        [Fact]
        public void TrueQuantifier3()
        {
            Test("@x,y.(>(F,F))", true);
        }
        
        [Fact]
        public void TrueQuantifier4()
        {
            Test(">(!x.(@y.(P(x,y))),@q.(!p.(P(p,q))))", true);
        }        
        
        [Fact]
        public void TrueQuantifier5()
        {
            Test("|(>(&(@x.(!y.(Q(x,y))),!y.(P(y))),@x.(!y.(Q(x,y)))),!y.(P(y)))", true);
        }        
        
        [Fact]
        public void TrueQuantifier6()
        {
            Test(">(|(!x.(P(x)),!x.(Q(x))),!x.(|(P(x),Q(x))))", true);
        }  
        
        [Fact]
        public void TrueQuantifier7()
        {
            Test(">(!x.(@y.(P(x,y))),@q.(!p.(P(p,q))))", true);
        }        

    }
}