using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Amil
{
    public class Amil
    {
        private static Lexer AmilLexerInstance;

        public static Lexer AmilLexer
        {
            get
            {
                if (AmilLexerInstance == null)
                {
                    LexerGenerator lg = new LexerGenerator();
                    lg.Ignore(new Regex(@"\s+"));
                    lg.Add(TokenType.VAR, new Regex(@"var"));
                    lg.Add(TokenType.IF, new Regex(@"if"));
                    lg.Add(TokenType.ELSE, new Regex(@"else"));
                    lg.Add(TokenType.PRINT, new Regex(@"print"));
                    lg.Add(TokenType.LPAREN, new Regex(@"\("));
                    lg.Add(TokenType.RPAREN, new Regex(@"\)"));
                    lg.Add(TokenType.LBRACE, new Regex(@"{"));
                    lg.Add(TokenType.RBRACE, new Regex(@"}"));
                    lg.Add(TokenType.EQUAL, new Regex(@"="));
                    lg.Add(TokenType.PLUS, new Regex(@"\+"));
                    lg.Add(TokenType.MINUS, new Regex(@"-"));
                    lg.Add(TokenType.TIMES, new Regex(@"\*"));
                    lg.Add(TokenType.EQUAL_TO, new Regex(@"=="));
                    lg.Add(TokenType.GREATER_EQUAL, new Regex(@">="));
                    lg.Add(TokenType.SEMICOLON, new Regex(@";"));
                    lg.Add(TokenType.INTEGER, new Regex(@"\d+"));
                    lg.Add(TokenType.BOOLEAN, new Regex(@"(true)|(false)"));
                    lg.Add(TokenType.NAME, new Regex(@"[a-zA-z_][a-zA-Z0-9_]*"));
                    AmilLexerInstance = lg.Build();
                }
                return AmilLexerInstance;
            }
        }
    }
    public class Interpreter
    {
        
    }
  
}
