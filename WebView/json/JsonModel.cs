using System.Collections.Generic;

namespace WebView.json
{
    public class JsonModel
    {
        public TextModel text { get; set; }
        public IList<JsonModel> children { get; set; }
    }
}