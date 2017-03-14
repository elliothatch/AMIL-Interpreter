using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace AmilTest
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void ParserTest1()
        {
            Amil.Lexer lexer = Amil.Amil.AmilLexer;
            Amil.Parser parser = new Amil.Parser();
            Amil.Statement statement = parser.parse(lexer.Lex("{var a; if 0 { print true; }"));
            Console.WriteLine(statement.ToString());
        }
    }
}
