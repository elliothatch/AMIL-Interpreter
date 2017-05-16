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

        [TestMethod]
        public void ParserTest2()
        {
            Amil.Lexer lexer = Amil.Amil.AmilLexer;
            Amil.Parser parser = new Amil.Parser();
            Amil.Statement statement = parser.parse(lexer.Lex("{ print 1+2+3+4+5; }"));
            Console.WriteLine(statement.ToString());
        }

        public void CompileTest1()
        {

        }
    }
}
