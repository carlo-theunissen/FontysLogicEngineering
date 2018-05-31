using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Logic.Decorators;
using Microsoft.AspNetCore.Mvc;
using WebView.Models;

namespace WebView.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Console.WriteLine("formula:" + "test");
            return View();
        }
        
        public IActionResult Calculate(string formula)
        {
            if (string.IsNullOrEmpty(formula))
            {
                return RedirectToAction("Index");
            }
            
            var model = new CalculateViewModel();
            
               
            var parser = StringParser.Create(formula);
            var calculatedFormula = parser.GetOperator();
            var nandify = new NandifyDecorator(calculatedFormula);
            model.OriginalFormula = calculatedFormula.ToString();
            model.Logic = calculatedFormula.ToLogicString();
            model.Nandify = nandify.GetOperator().ToString();
            model.HasResult = calculatedFormula.HasResult();
            
            if (calculatedFormula.HasResult())
            {
                var stable = new SimplifiedTruthTableCreator(parser);
                var table = new TruthTableCreator(parser);
                var normal = new DisjunctiveNormalDecorator(table);
                var simplifiedNormal = new DisjunctiveNormalDecorator(stable);
                model.Normalize = normal.GetOperator().ToString();
                model.NormalizeLogicFormat = normal.GetOperator().ToLogicString();
                model.TruthTable = table.GetTable();
                model.SimplifiedTruthTable = stable.GetTable();
                model.Arguments = calculatedFormula.GetArguments();

                model.SimplifiedNormalize = simplifiedNormal.GetOperator().ToString();
                model.SimplifiedNormalizeLogicFormat = simplifiedNormal.GetOperator().ToLogicString();

                model.Hex = table.ToHex();
            }
            return View(model);
        }

    }
}