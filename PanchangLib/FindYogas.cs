

using System;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace org.transliteral.panchang
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




	public class FindYogas
	{	

		Horoscope horoscope = null;
		public Node rootNode = null;
		ZodiacHouse zodiacLagna = null;
		Division divisionType = null;

		static public void Test (Horoscope h, Division division)
		{
			FindYogas fy = new FindYogas(h, division);
			//fy.evaluateYoga ("gr<sun> in hse <1st>");
			//fy.evaluateYoga ("  gr<sun> in hse <1st>  ");
			//fy.evaluateYoga ("( gr<sun> in   hse <1st> )");
			//fy.evaluateYoga ("(gr<sun> in hse <1st>)");
			//fy.evaluateYoga ("(gr<sun> in hse <1st> )  ");

			//fy.evaluateYoga ("<gr:sun,moon,mars,ketu> in <rasi:1st,2nd,3rd,4th,5th,6th,7th,8th>");
			//fy.evaluateYoga ("<gr:mer> with <gr:<lordof:ari>>");
			//fy.evaluateYoga ("&&(<gr:mer> with <gr:<lordof:ari>>)(birth in <time:day>)");
			//fy.evaluateYoga ("&&(<gr:mer> with <gr:<lordof:ari>>)(birth in <time:night>)");
 
			//fy.evaluateYoga ("<gr:mer> in <rasi:leo>");
			//fy.evaluateYoga ("rasi@(<gr:mer> in <rasi:leo>)");
			//fy.evaluateYoga ("navamsa@(<gr:mer> in <rasi:leo>)");
			//fy.evaluateYoga ("rasi@(<gr:mer> in <rasi:can>)");
			//fy.evaluateYoga ("navamsa@(<gr:mer> in <rasi:can>)");
			//fy.evaluateYoga ("&&(rasi@(<gr:mer> in <rasi:leo>))(d9@(<gr:mer> in <rasi:can>)))");

			
			//fy.evaluateYoga ("<gr:<dispof:mer>> is <gr:moon>");
			//fy.evaluateYoga ("d9@(<gr:<dispof:<dispof:mer>>> is <gr:moon>)");
			//fy.evaluateYoga ("<gr:<d9@dispof:merc>> with <gr:sun>");
			//fy.evaluateYoga ("&&(<gr:sun,moon,mars> in <rasi:1st,1st,ari> with <gr:moon> and <gr:jup,pis>)(<gr:moon> in <rasi:2nd>)");
			//fy.evaluateYoga ("(&& (gr<sun> in hse<1st>) (mid term) (gr<moon> in  hse<2nd> ) )");
		}


		public FindYogas(Horoscope h, Division d)
		{
			horoscope = h;
			divisionType = d;
			zodiacLagna = horoscope.GetPosition(Body.Name.Lagna).ToDivisionPosition(divisionType).ZodiacHouse;
		}


		XmlYogaNode xmlNode = null;
		
		public string GetRuleName ()
		{
			if (xmlNode == null || xmlNode.horaRule == null)
				return "";
			return xmlNode.horaRule;
			
		}
        public bool EvaluateYoga(XmlYogaNode node)
		{
			xmlNode = node;
			return this.EvaluateYoga(node.horaRule);
		}

		public bool EvaluateYoga (string rule)
		{
			rootNode = new Node (null, rule, this.divisionType);

            Logger.Info(String.Format("Evaluating yoga .{0}.", rule));
			this.GenerateSimpleParseTree();
			this.ExpandSimpleNodes();
			bool bRet = this.ReduceTree();

            Logger.Info(String.Format("Final: {0} = {1}", bRet, rule));
			return bRet;
		}


		public string TrimWhitespace (string sCurr)
		{
			// remove leading & trailing whitespaces
			sCurr = Regex.Replace(sCurr, @"^\s*(.*?)\s*$", "$1");

			// remove contiguous whitespace
			sCurr = Regex.Replace(sCurr, @"(\s+)", " ");

			return sCurr;
		}

		public string PeelOuterBrackets (string sCurr)
		{
			// remove leading "(" and whitespace
			sCurr = Regex.Replace(sCurr, @"^\s*\(\s*", "");
			// remove trailing ")" and whitespace
			sCurr = Regex.Replace(sCurr, @"\s*\)\s*$", "");
			return sCurr;
		}

		public string[] GetComplexTerms (string sInit)
		{
			ArrayList al = new ArrayList();

			int level = 0;
			int start = 0;
			int end = 0;

			for (int i=0; i<sInit.Length; i++)
			{
				char curr = sInit[i];

				// we're only concerned about the grouping
				if (curr != '(' && curr != ')')
					continue;

				if (curr == '(')
				{
					if (++level == 1)
						start = i;
				}

				if (curr == ')')
				{
					if (level-- == 1)
					{
						end = i;
						string sInner = sInit.Substring(start, end-start+1);
						al.Add(sInner);
					}
				}

				if (level == 0 && curr != '(' && curr != ')')
					throw new YogasParseException("Found unexpected char outside parantheses");
			}

			if (level > 0)
				throw new YogasParseException("Unmatched parantheses");

			return (string[])al.ToArray(typeof(string));
		}

		public bool CheckBirthTime (string sTime)
		{
			switch (sTime)
			{
				case "day":
					return horoscope.IsDayBirth();
				case "night":
					return !horoscope.IsDayBirth();
				default:
					Logger.Info("Unknown birth time: " + sTime + this.GetRuleName());
					return false;
			}
		}
		public bool EvaluateNode (Node node)
		{
			Debug.Assert (node.type == Node.EType.Single);

			string cats = "";
			string[] simpleTerms = node.term.Split(new char[] {' '});
			string[] simpleVals = new string[simpleTerms.Length];
			for (int i=0; i<simpleTerms.Length; i++)
			{
				cats += " " + this.getCategory(simpleTerms[i]);
				simpleVals[i] = (string)this.GetValues(simpleTerms[i])[0];
			}
			cats = this.TrimWhitespace(cats);

			Body.Name b1, b2, b3;
			ZodiacHouseName zh1, zh2;
			int hse1, hse2;

			Division evalDiv = node.dtype;
			switch (cats)
			{
				case "gr: in rasi:":
				case "gr: in house:":
					b1 = this.StringToBody(simpleVals[0]);
					zh1 = this.StringToRasi(simpleVals[2]);
					if (horoscope.GetPosition(b1).ToDivisionPosition(evalDiv).ZodiacHouse.Value == zh1)
						return true;
					return false;
				case "gr: in mt":
				case "gr: in moolatrikona":
					b1 = this.StringToBody(simpleVals[0]);
					return horoscope.GetPosition(b1).ToDivisionPosition(evalDiv).IsInMoolaTrikona();
				case "gr: in exlt":
				case "gr: in exaltation":
					b1 = this.StringToBody(simpleVals[0]);
					return horoscope.GetPosition(b1).ToDivisionPosition(evalDiv).IsExaltedPhalita();
				case "gr: in deb":
				case "gr: in debilitation":
					b1 = this.StringToBody(simpleVals[0]);
					return horoscope.GetPosition(b1).ToDivisionPosition(evalDiv).IsDebilitatedPhalita();
				case "gr: in own":
				case "gr: in ownhouse":
				case "gr: in own house":
					b1 = this.StringToBody(simpleVals[0]);
					return horoscope.GetPosition(b1).ToDivisionPosition(evalDiv).IsInOwnHouse();
				case "gr: is gr:":
					b1 = this.StringToBody(simpleVals[0]);
					b2 = this.StringToBody(simpleVals[2]);
					if (b1 == b2)
						return true;
					return false;
				case "gr: with gr:":
					b1 = this.StringToBody(simpleVals[0]);
					b2 = this.StringToBody(simpleVals[2]);
					if (horoscope.GetPosition(b1).ToDivisionPosition(evalDiv).ZodiacHouse.Value ==
						horoscope.GetPosition(b2).ToDivisionPosition(evalDiv).ZodiacHouse.Value)
						return true;
					return false;
				case "gr: asp gr:":
					b1 = this.StringToBody(simpleVals[0]);
					b2 = this.StringToBody(simpleVals[2]);
					if (horoscope.GetPosition(b1).ToDivisionPosition(evalDiv).GrahaDristi(
						horoscope.GetPosition(b2).ToDivisionPosition(evalDiv).ZodiacHouse))
						return true;
					return false;
				case "gr: in house: from rasi:":
					b1 = this.StringToBody(simpleVals[0]);
					hse1 = this.StringToHouse(simpleVals[2]);
					zh1 = this.StringToRasi(simpleVals[4]);
					if (horoscope.GetPosition(b1).ToDivisionPosition(evalDiv).ZodiacHouse.Value ==
						new ZodiacHouse(zh1).Add(hse1).Value)
						return true;
					return false;
				case "gr: in house: from gr:":
					b1 = this.StringToBody(simpleVals[0]);
					hse1 = this.StringToHouse(simpleVals[2]);
					b2 = this.StringToBody(simpleVals[4]);
					return horoscope.GetPosition(b1).ToDivisionPosition(evalDiv).ZodiacHouse.Value ==
						horoscope.GetPosition(b2).ToDivisionPosition(evalDiv).ZodiacHouse.Add(hse1).Value;
				case "graha in house: from gr: except gr:":
					hse1 = this.StringToHouse(simpleVals[2]);
					b1 = this.StringToBody(simpleVals[4]);
					b2 = this.StringToBody(simpleVals[6]);
					zh1 = horoscope.GetPosition(b1).ToDivisionPosition(evalDiv).ZodiacHouse.Add(hse1).Value;
					for (int i = (int)Body.Name.Sun; i<= (int)Body.Name.Lagna; i++)
					{
						Body.Name bExc = (Body.Name) i;
						if (bExc != b2 &&
							horoscope.GetPosition(bExc).ToDivisionPosition(evalDiv).ZodiacHouse.Value == zh1)
							return true;
					}
					return false;
				case "rasi: in house: from rasi:":
					zh1 = this.StringToRasi(simpleVals[0]);
					hse1 = this.StringToHouse(simpleVals[2]);
					zh2 = this.StringToRasi(simpleVals[4]);
					if (new ZodiacHouse(zh1).Add(hse1).Value == zh2)
						return true;
					return false;
				case "birth in time:":
					return this.CheckBirthTime(simpleVals[2]);
				default:
					Logger.Info("Unknown rule: " + cats + this.GetRuleName());
					return false;
			}
		}

		public bool ReduceTree (Node node)
		{
            Logger.Info(String.Format("Enter ReduceTree {0} {1}", node.type, node.term));
			bool bRet = false;
			switch (node.type)
			{
				case Node.EType.Not:
					Debug.Assert(node.children.Length == 1);
					bRet = !(this.ReduceTree(node.children[0]));
					goto reduceTreeDone;
				case Node.EType.Or:
					for (int i=0; i<node.children.Length; i++)
					{
						if (this.ReduceTree(node.children[i]) == true)
						{
							bRet = true; 
							goto reduceTreeDone;
						}
					}
					bRet = false;
					goto reduceTreeDone;
				case Node.EType.And:
					for (int i=0; i<node.children.Length; i++)
					{
						if (this.ReduceTree(node.children[i]) == false)
						{
							bRet = false;
							goto reduceTreeDone;
						}
					}
					bRet = true;
					goto reduceTreeDone;
				default:
				case Node.EType.Single:
					bRet = this.EvaluateNode(node);
					goto reduceTreeDone;
			}
			reduceTreeDone:
            Logger.Info(String.Format("Exit ReduceTree {0} {1} {2}", node.type, node.term, bRet));
			return bRet;
		}

		public bool ReduceTree ()
		{
			return this.ReduceTree(this.rootNode);
		}

		public void GenerateSimpleParseTreeForNode (Queue queue, Node node)
		{
			string text = node.term;

			// remove general whitespace
			text = this.TrimWhitespace(text);
		
			bool bOpen = Regex.IsMatch(text, @"\(");
			bool bClose = Regex.IsMatch(text, @"\)");

			Match mDiv = Regex.Match(text, @"^([^&!<\(]*@)");
			if (mDiv.Success)
			{
				node.dtype = this.StringToDivision(mDiv.Groups[1].Value);
				text = text.Replace(mDiv.Groups[1].Value, "");
             Logger.Info(String.Format("Match. Replaced {0}. Text now {1}", 
								mDiv.Groups[1].Value, text));
            }

            // already in simple format
            if (false == bOpen && false == bClose)
			{
				node.type = Node.EType.Single;
				node.term = text;
                Logger.Info(String.Format("Need to evaluate simple node {0}", text));
				return;
			}

			// Find operator. One of !, &&, ||
			if (text[0] == '!')
			{
				Node notChild = new Node(node, text.Substring(1, text.Length-1), node.dtype);
				queue.Enqueue(notChild);

				node.type = Node.EType.Not;
				node.addChild(notChild);
				return;
			}

			if (text[0] == '&' && text[1] == '&')
				node.type = Node.EType.And;
			
			else if (text[0] == '|' && text[1] == '|')
				node.type = Node.EType.Or;
			
			// non-binary term with brackets. Peel & reparse
			else
			{
				node.term = this.PeelOuterBrackets(text);
				queue.Enqueue(node);
			}


			// Parse terms with more than one subterm
			if (node.type == Node.EType.And ||
				node.type == Node.EType.Or)
			{
				string[] subTerms = this.GetComplexTerms(text);
				foreach (string subTerm in subTerms)
				{
					Node subChild = new Node(node, subTerm, node.dtype);
					queue.Enqueue(subChild);
					node.addChild(subChild);
				}
			}

            Logger.Info(String.Format("Need to evaluate complex node {0}", text));
		}

		public void GenerateSimpleParseTree ()
		{
			Queue q = new Queue();
			q.Enqueue(rootNode);

			while (q.Count > 0)
			{
				Node n = (Node)q.Dequeue();
				if (n == null)
					throw new Exception("FindYogas::generateSimpleParseTree. Dequeued null");

				this.GenerateSimpleParseTreeForNode(q, n);
			}
		}


		public Body.Name StringToBody (string s)
		{
			switch (s)
			{
				case "su": case "sun": return Body.Name.Sun;
				case "mo": case "moo": case "moon": return Body.Name.Moon;
				case "ma": case "mar": case "mars": return Body.Name.Mars;
				case "me": case "mer": case "mercury": return Body.Name.Mercury;
				case "ju": case "jup": case "jupiter": return Body.Name.Jupiter;
				case "ve": case "ven": case "venus": return Body.Name.Venus;
				case "sa": case "sat": case "saturn": return Body.Name.Saturn;
				case "ra": case "rah": case "rahu": return Body.Name.Rahu;
				case "ke": case "ket": case "ketu": return Body.Name.Ketu;
				case "la": case "lag": case "lagna": case "asc": return Body.Name.Lagna;
				default:
                    Logger.Info("Unknown body: " + s + this.GetRuleName());
					return Body.Name.Other;
			}
		}
		public Division StringToDivision (string s)
		{
			// trim trailing @
			s = s.Substring(0, s.Length-1);

			DivisionType _dtype;
			switch (s)
			{
				case "rasi": case "d-1": case "d1": _dtype = DivisionType.Rasi; break;
				case "navamsa": case "d-9": case "d9": _dtype = DivisionType.Navamsa; break;
				default:
					Logger.Info("Unknown division: " + s + this.GetRuleName());
					_dtype = DivisionType.Rasi;
					break;
			}
			return new Division(_dtype);
		}
		public ZodiacHouseName StringToRasi (string s)
		{
			switch (s)
			{
				case "ari": return ZodiacHouseName.Ari;
				case "tau": return ZodiacHouseName.Tau;
				case "gem": return ZodiacHouseName.Gem;
				case "can": return ZodiacHouseName.Can;
				case "leo": return ZodiacHouseName.Leo;
				case "vir": return ZodiacHouseName.Vir;
				case "lib": return ZodiacHouseName.Lib;
				case "sco": return ZodiacHouseName.Sco;
				case "sag": return ZodiacHouseName.Sag;
				case "cap": return ZodiacHouseName.Cap;
				case "aqu": return ZodiacHouseName.Aqu;
				case "pis": return ZodiacHouseName.Pis;
				default:
					Logger.Info("Unknown rasi: " + s + this.GetRuleName());
					return ZodiacHouseName.Ari;
			}
		}
		public int StringToHouse (string s)
		{
			int tempVal = 0;

			switch (s)
			{
				case "1": case "1st": tempVal = 1; break;
				case "2": case "2nd": tempVal = 2; break;
				case "3": case "3rd": tempVal = 3; break;
				case "4": case "4th": tempVal = 4; break;
				case "5": case "5th": tempVal = 5; break;
				case "6": case "6th": tempVal = 6; break;
				case "7": case "7th": tempVal = 7; break;
				case "8": case "8th": tempVal = 8; break;
				case "9": case "9th": tempVal = 9; break;
				case "10": case "10th": tempVal = 10; break;
				case "11": case "11th": tempVal = 11; break;
				case "12": case "12th": tempVal = 12; break;
			}
			return tempVal;
		}
		public string ReplaceBasicNodeCat (string category)
		{
			switch (category)
			{
				case "simplelordof:":
				case "lordof:":
				case "dispof:":
				//case "grahasin":
					return "";
				default:
					return category;
			}
		}
		public string ReplaceBasicNodeTermHelper (Division d, string category, string value)
		{
			int tempVal = 0;
			ZodiacHouseName zh;
			Body.Name b;
			switch (category)
			{
				case "rasi:": case "house:": case "hse:":
					tempVal = this.StringToHouse(value);
					if (tempVal > 0)
						return zodiacLagna.Add(tempVal).ToString().ToLower();
				switch (value)
				{
					case "kendra":
						return "1st,4th,7th,10th";
				}
					break;
			 	case "gr:": case "graha:":
				switch (value)
				{
					case "ben":
						return "mer,jup,ven,moo";
				}
					break;
				case "rasiof:":
					b = this.StringToBody(value);
					return horoscope.GetPosition(b).ToDivisionPosition(d).ZodiacHouse.Value
						.ToString().ToLower();
				case "lordof:":
					tempVal = this.StringToHouse(value);
					if (tempVal > 0)
						return horoscope.LordOfZodiacHouse(zodiacLagna.Add(tempVal), d).ToString().ToLower();
					zh = this.StringToRasi(value);
					return horoscope.LordOfZodiacHouse(zh, d).ToString().ToLower();
				case "simplelordof:":
					tempVal = this.StringToHouse(value);
					if (tempVal > 0)
						return horoscope.LordOfZodiacHouse(zodiacLagna.Add(tempVal), d).ToString().ToLower();
					zh = this.StringToRasi(value);
					return Basics.SimpleLordOfZodiacHouse(zh).ToString().ToLower();
				case "dispof:":
					b = this.StringToBody(value);
					return horoscope.LordOfZodiacHouse(
						horoscope.GetPosition(b).ToDivisionPosition(d).ZodiacHouse, d)
						.ToString().ToLower();
			}
			return value;
		}

		public string GetDivision (string sTerm)
		{
			Match mDiv = Regex.Match (sTerm, "<(.*)@");
			if (mDiv.Success)
				return mDiv.Groups[1].Value.ToLower();
			return "";
		}
		public string getCategory (string sTerm)
		{
			// Find categofy
			Match mCat = Regex.Match (sTerm, "<.*@(.*:)");
			if (mCat.Success == false)
				mCat = Regex.Match (sTerm, "<(.*:)");

			if (mCat.Success)
				return mCat.Groups[1].Value.ToLower();
			else
				return sTerm;
		}
		public ArrayList GetValues (string sTerm)
		{
			// Find values. Find : or , on the left
			ArrayList alVals = new ArrayList();
			MatchCollection mVals = Regex.Matches (sTerm, "[:,]([^<:,>]*)");
			if (mVals.Count >= 1)
			{
				foreach (Match m in mVals)
					alVals.Add (m.Groups[1].Value.ToLower());
			}
			else
			{
				alVals.Add(sTerm);
			}
			return alVals;

		}
		public string ReplaceBasicNodeTerm (Division d, string sTerm)
		{
			string sDiv = this.GetDivision(sTerm);
			string sCat = this.getCategory(sTerm);
			ArrayList alVals = this.GetValues(sTerm);

			Hashtable hash = new Hashtable();
			foreach (string s in alVals)
			{
				string sRep = this.ReplaceBasicNodeTermHelper(d, sCat, s);
				if (!hash.ContainsKey(sRep))
					hash.Add(sRep, null);
			}

			bool bStart = false;
			string sNew = this.ReplaceBasicNodeCat(sCat);
			bool sPreserveCat = sNew.Length == 0;

			if (false == sPreserveCat) sNew = "<" + sNew;
			
			ArrayList alSort = new ArrayList();
			foreach (string s in hash.Keys)
				alSort.Add(s);
			alSort.Sort();

			foreach (string s in alSort)
			{
				if (bStart == true)
					sNew += "," + s;
				else
					sNew += s;
				bStart = true;
			}
			if (false == sPreserveCat)
				sNew += ">";

            Logger.Info(String.Format("{0} evals to {1}", sTerm, sNew));
			return sNew;
		}

		public string SimplifyBasicNodeTerm (Node n, string sTerm)
		{

			while (true)
			{
                Logger.Info(String.Format("Simplifying basic term: .{0}.", sTerm));		
				Match m = Regex.Match(sTerm, "<[^<>]*>");

				// No terms found. Nothing to do.
				if (m.Success == false)
					return sTerm;

				Division d = n.dtype;
				string sInner = m.Value;

				// see if a varga was explicitly specified
				Match mDiv = Regex.Match(sInner, "<([^:<>]*@)");
				if (mDiv.Success == true)
				{
					d = this.StringToDivision(mDiv.Groups[1].Value);
					sInner.Replace(mDiv.Groups[1].Value, "");
				}

				// Found a term, evaluated it. Nothing happened. Done.
				string newInner = this.ReplaceBasicNodeTerm(d, sInner);

                Logger.Info(String.Format("{0} && {1}", newInner.Length, m.Value.Length));

				if (newInner.ToString() == m.Value.ToLower())
					return sTerm;

				// Replace the current term and continue along merrily
				sTerm = sTerm.Replace(m.Value, newInner);
			}
		}

		public void SimplifyBasicNode (Queue queue, Node node)
		{
			// A simple wrapper that takes each individual whitespace
			// separated term, and tries to simplify it down to bare
			// bones single stuff ready for true / false evaluation
			string cats = "";

			string sNew = "";
			string[] simpleTerms = node.term.Split(new char[] {' '});
			for (int i=0; i<simpleTerms.Length; i++)
			{
				simpleTerms[i] = this.SimplifyBasicNodeTerm(node, simpleTerms[i]);
				sNew += " " + simpleTerms[i];
				cats += " " + this.getCategory(simpleTerms[i]);
			}

			node.term = this.TrimWhitespace(sNew);

			cats = this.TrimWhitespace(cats);
			Logger.Info(String.Format("Cats = {0}", cats));

		}

		public void ExpandSimpleNode (Queue queue, Node node)
		{
			// <a,b,> op <d,e> 
			// becomes
			// ||(<a> op <e>)(<a> op <e>)(<b> op <d>)(<b> op <e>)

			Node.EType eLogic = Node.EType.Or;
            Logger.Info(String.Format("Inner logic: n.term is {0}", node.term));
			if (node.term[0] == '&' && node.term[1] == '&')
			{
				eLogic = Node.EType.And;
				node.term = this.TrimWhitespace(node.term.Substring(2, node.term.Length-2));
			} 
			else if (node.term[0] == '|' && node.term[1] == '|')
			{
				node.term = this.TrimWhitespace(node.term.Substring(2, node.term.Length-2));
			}
            Logger.Info(String.Format("Inner logic: n.term is now {0}", node.term));

			// find num Vals etc
			string[] simpleTerms = node.term.Split(new char[] {' '});
			string[] catTerms = new string[simpleTerms.Length];
			int[] simpleTermsValues = new int[simpleTerms.Length];
			ArrayList[] simpleTermsRealVals = new ArrayList[simpleTerms.Length];

			int numExps = 1;

			// determine total # exps
			for (int i=0; i<simpleTerms.Length; i++)
			{
				catTerms[i] = this.getCategory(simpleTerms[i]);
				simpleTermsRealVals[i] = this.GetValues(simpleTerms[i]);
				simpleTermsValues[i] = simpleTermsRealVals[i].Count;
				if (simpleTermsValues[i] > 1)
					numExps *= simpleTermsValues[i];
			}

            Logger.Info(String.Format("Exp: {0} requires {1} exps", node.term, numExps));

			// done
			if (numExps <= 1)
				return;

			string[] sNew = new string[numExps];

			// use binary reduction. first term repeats n times, then n/2 etc.
			// "binary" actualy n-ary on number of possible values
			int _numConc = numExps;
			for (int i=0; i<simpleTerms.Length; i++)
			{
				// if more than one value, n-ary reduction
				if (simpleTermsValues[i] > 1)
					_numConc /= simpleTermsValues[i];

				// determine repeat count. with one value, assign to 1
				int numConc = _numConc;
				if (simpleTermsValues[i] == 1)
					numConc = 1;

				// baseIndex increments to numConc after each iteration
				// continue till we fill the list
				int baseIndex =0;
				int valIndex = 0;
				while (baseIndex < numExps)
				{
					for (int j=0; j<numConc; j++)
					{
						int ix = valIndex;
						if (simpleTermsValues[i] == 1) ix = 0;
						sNew[baseIndex+j] += " ";
						if (catTerms[i][catTerms[i].Length-1] == ':')
							sNew[baseIndex+j] += "<" + catTerms[i] + simpleTermsRealVals[i][ix] + ">";
						else
							sNew[baseIndex+j] += simpleTermsRealVals[i][ix];
					}
					baseIndex += numConc;
					valIndex++;
					if (valIndex == simpleTermsValues[i])
						valIndex=0;
				}			
			}

			node.type = eLogic;
			for (int i=0; i<sNew.Length; i++)
			{
				Node nChild = new Node(node, this.TrimWhitespace(sNew[i]), node.dtype);
				node.addChild(nChild);
                Logger.Info(String.Format("sNew[{0}]: {1}", i, sNew[i]));
			}
		}

		public void ExpandSimpleNodes ()
		{
			Queue q = new Queue();
			q.Enqueue(rootNode);

			while (q.Count > 0)
			{
				Node n = (Node)q.Dequeue();
				if (n == null)
					throw new Exception("FindYogas::expandSimpleNodes. Dequeued null");

				if (n.type == Node.EType.Single)
				{
					this.SimplifyBasicNode(q, n);
				}
				else
				{
					foreach (Node nChild in n.children)
						q.Enqueue (nChild);
				}
			}


			q.Enqueue(rootNode);
			while (q.Count > 0)
			{
				Node n = (Node)q.Dequeue();
				if (n.type == Node.EType.Single)
				{
					this.ExpandSimpleNode(q,n);
				}
				else
				{
					foreach (Node nChild in n.children)
						q.Enqueue(nChild);
				}
			}

		}
	}


}
