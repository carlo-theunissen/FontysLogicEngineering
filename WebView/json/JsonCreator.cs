using System;
using System.Linq;
using Logic.interfaces;
using Logic.SemanticTableaux;

namespace WebView.json
{
    public class JsonCreator
    {
        public static JsonModel CreateFromBaseOpeator(IAsciiBasePropositionalOperator baseOperator)
        {
            var json = new JsonModel();
            var text = new TextModel();
            text.title = baseOperator.ToName();
            json.text = text;
            if (baseOperator.GetChilds() !=  null)
            {
                json.children = baseOperator.GetChilds().Where(x => x != null).Select(CreateFromBaseOpeator).ToList();
            }
            return json;
        }

        public static JsonModel CreateFromTableauxStep(TableuaxStep step)
        {
            var json = new JsonModel();
            var text = new TextModel();
            text.title =  step.GetOperators().Aggregate("",
                (current, asciiBasePropositionalOperator) =>
                    current + " , " + asciiBasePropositionalOperator.ToLogicString());
            text.title = text.title.Substring(3, text.title.Length - 3);
          //  text.title = (step.HasNext() ? "[noNext]" : "[!NEXT!]") + " " + text.title;
            json.text = text;
            if (!step.IsClosed())
            {
                Console.WriteLine("NOT CLOSED");
            }
            if (step.GetChilds() !=  null)
            {
                json.children = step.GetChilds().Where(x => x != null).Select(CreateFromTableauxStep).ToList();
            }
            return json;
        }
    }
}