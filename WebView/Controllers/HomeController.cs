using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Logic.Abstract;
using Logic.Decorators;
using Logic.interfaces;
using Logic.SemanticTableaux;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebView.json;
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
            model.OriginalFormula = calculatedFormula.ToString();
            model.Logic = calculatedFormula.ToLogicString();
            model.Nandify = calculatedFormula.ToNandify().ToString();
            model.HasResult = calculatedFormula.HasResult();

            var json = JsonCreator.CreateFromBaseOpeator(calculatedFormula);
            model.JsonData = JsonConvert.SerializeObject(json);


            SemanticTableauxParser tableaux;
            if (calculatedFormula.HasResult())
            {
                var stable = new SimplifiedTruthTableCreator(calculatedFormula);
                var table = new TruthTableCreator(calculatedFormula);
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


                tableaux = new SemanticTableauxParser(calculatedFormula);
                model.isTautology = tableaux.IsTautology();
                model.tableauxJsonData =
                    JsonConvert.SerializeObject(JsonCreator.CreateFromTableauxStep(tableaux.GetStep()));
            return View(model);
        }

    }
}