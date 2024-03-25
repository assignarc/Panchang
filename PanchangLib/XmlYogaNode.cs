using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    public class XmlYogaNode
    {
       
        public string sourceRef;       // source reference (text:verse)
        public string sourceText;      // source rule (english)
        public string sourceItxText;   // source rule (eng / sans)
        public string mhoraRule;       // rule in mhora format
        public string result;          // results
        public string yogaCat;         // yoga category
        public string yogaName;        // short desc of yoga (1-2 words)
    }

}
