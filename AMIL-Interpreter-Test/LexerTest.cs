using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace AmilTest
{
    [TestClass]
    public class LexerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Amil.Lexer lexer = Amil.Amil.AmilLexer;
            IEnumerable<Amil.Token> tokens = lexer.Lex("a = 1;\nif a >= 2 {\nlog hello;\n}");
            /*foreach (Amil.Token token in tokens) {
                Debug.WriteLine(token.Type);
                Debug.WriteLine(token.Value);
            }*/

            List<Tuple<string, string>> expectedTokenVals = new List<Tuple<string, string>>{
                new Tuple<string, string>("NAME", "a"),
                new Tuple<string, string>("EQUAL", "="),
                new Tuple<string, string>("NUMBER", "1"),
                new Tuple<string, string>("SEMICOLON", ";"),
                new Tuple<string, string>("IF", "if"),
                new Tuple<string, string>("NAME", "a"),
                new Tuple<string, string>( "GREATER_EQUAL", ">="),
                new Tuple<string, string>("NUMBER", "2"),
                new Tuple<string, string>("LBRACE", "{"),
                new Tuple<string, string>("LOG", "log"),
                new Tuple<string, string>("NAME", "hello"),
                new Tuple<string, string>("SEMICOLON", ";"),
                new Tuple<string, string>("RBRACE", "}")
            };
            int index = 0;
            foreach(Amil.Token token in tokens)
            {
                if (index < expectedTokenVals.Count)
                {
                    Assert.AreEqual(expectedTokenVals[index].Item1, token.Type);
                    Assert.AreEqual(expectedTokenVals[index].Item2, token.Value);
                }
                else if(index == expectedTokenVals.Count)
                {
                    Assert.IsNull(token);
                }
                else
                {
                    Assert.Fail("Lexer returned more tokens than expected");
                }
                index++;
            }
        }
    }
}
