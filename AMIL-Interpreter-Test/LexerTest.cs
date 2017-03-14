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
            IEnumerable<Amil.Token> tokens = lexer.Lex("a = 1;\nif a >= 2 {\nprint hello;\n}");
            /*foreach (Amil.Token token in tokens) {
                Debug.WriteLine(token.Type);
                Debug.WriteLine(token.Value);
            }*/

            List<Tuple<Amil.TokenType, string>> expectedTokenVals = new List<Tuple<Amil.TokenType, string>>{
                new Tuple<Amil.TokenType, string>(Amil.TokenType.NAME, "a"),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.EQUAL, "="),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.INTEGER, "1"),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.SEMICOLON, ";"),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.IF, "if"),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.NAME, "a"),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.GREATER_EQUAL, ">="),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.INTEGER, "2"),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.LBRACE, "{"),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.PRINT, "print"),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.NAME, "hello"),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.SEMICOLON, ";"),
                new Tuple<Amil.TokenType, string>(Amil.TokenType.RBRACE, "}")
            };
            int index = 0;
            foreach(Amil.Token token in tokens)
            {
                if (index < expectedTokenVals.Count)
                {
                    Assert.AreEqual(expectedTokenVals[index].Item1, token.Type);
                    Assert.AreEqual(expectedTokenVals[index].Item2, token.Value);
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
