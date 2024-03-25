

using System;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using org.transliteral.panchang;
using BaseFindYogas = org.transliteral.panchang.FindYogas;

namespace mhora
{
	

	/*
	 * Here's the syntax that we take
	 */

	/* Here's a call-stack type walkthrough of this class
	 This is particularly useful because some of the functions are poorly names
		evaluate yoga ( full user-specified rule )
		Phase 1: Generate basic parse tree. i.e. each bracketed portion forms one node
			- FindYogas.generateSimpleParseTree (wrapper)
				- FindYogas.generateSimpleParseTreeForNode (worker function)
		
		Phase 2: Expand each of these nodes. This involves taking each leaf node, which
			may contain implicit if-blocks ex <graha:sun,moon> in <rasi:ari,2nd>,
			evaluating some values (2nd=gem), and expanding this into its 4 node equivalent
			- FindYogas.expandSimpleNodes (wrapper)
				- FindYogas.simplifyBasicNode (simplify these <lordof:<rasi:blah>>) exps
					- FindYogas.simplifyBasicNodeTerm (simplify each single term)
						- FindYogas.replaceBasicNodeTerm (replacement parser)
				- FindYogas.expandSimpleNode (implicit binary expansion)
				
		Phase 3: The real evaluation
			Here we simply walk the parse tree, calling ourserves recursively and evaluating
			our &&, ||, ! and (true, false) semantics
			- ReduceTree
				- Recursively call ReduceTree as needed
				- evaluateNode (take simple node, and authoritatively return trueor false
	*/




	public class FindYogas : BaseFindYogas
	{	

		
		public FindYogas(Horoscope _h, Division __dtype) : base(_h,__dtype)
		{
			
		}

		public override void LogMessage(string mesage)
		{
			MessageBox.Show(mesage);
        }
    }


}
