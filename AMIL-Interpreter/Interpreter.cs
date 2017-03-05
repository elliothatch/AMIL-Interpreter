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
                    lg.Add("IF", new Regex(@"if"));
                    lg.Add("ELSE", new Regex(@"else"));
                    lg.Add("LOG", new Regex(@"log"));
                    lg.Add("LPAREN", new Regex(@"\("));
                    lg.Add("RPAREN", new Regex(@"\)"));
                    lg.Add("LBRACE", new Regex(@"{"));
                    lg.Add("RBRACE", new Regex(@"}"));
                    lg.Add("EQUAL", new Regex(@"="));
                    lg.Add("GREATER_EQUAL", new Regex(@">="));
                    lg.Add("SEMICOLON", new Regex(@";"));
                    lg.Add("NUMBER", new Regex(@"\d+"));
                    lg.Add("NAME", new Regex(@"[a-zA-z_][a-zA-Z0-9_]*"));
                    AmilLexerInstance = lg.Build();
                }
                return AmilLexerInstance;
            }
        }
    }
    public class Interpreter
    {
        
    }
    
    public class TokenPosition
    {
        private int currentColumn;
        private int currentIndex;
        private int currentLine;

        public TokenPosition(int currentIndex, int currentLine, int currentColumn)
        {
            this.currentIndex = currentIndex;
            this.currentLine = currentLine;
            this.currentColumn = currentColumn;
        }

        public int Column { get; set; }
        public int Index { get; set; }
        public int Line { get; set; }
    }

    

    public class Token
    {
        public TokenPosition Position { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public Token(string type, string value, TokenPosition position)
        {
            Type = type;
            Value = value;
            Position = position;
        }
    }

    public class TokenDefinition
    {
        public string Type { get; set; }
        public Regex Pattern { get; set; }
        public bool Ignore { get; set; }

        public TokenDefinition(string type, Regex pattern, bool ignore)
        {
            Type = type;
            Pattern = pattern;
            Ignore = ignore;
        }
    }

    public class LexerGenerator
    {
        protected List<TokenDefinition> tokenDefinitions;
        public LexerGenerator()
        {
            tokenDefinitions = new List<TokenDefinition>();
        }

        public void Add(string type, Regex pattern)
        {
            tokenDefinitions.Add(new TokenDefinition(type, pattern, false));
        }

        public void Ignore(Regex pattern)
        {
            tokenDefinitions.Add(new TokenDefinition("IGNORE", pattern, true));
        }

        public Lexer Build()
        {
            return new Lexer(this.tokenDefinitions);
        }

    }

    public class Lexer {

        protected List<TokenDefinition> tokenDefinitions;

        public Lexer(List<TokenDefinition> tokenDefinitions)
        {
            this.tokenDefinitions = tokenDefinitions;
        }

        public IEnumerable<Token> Lex(string source)
        {
            int currentIndex = 0;
            int currentLine = 1;
            int currentColumn = 0;

            while (currentIndex < source.Length)
            {
                TokenDefinition matchedDefinition = null;
                int matchLength = 0;

                foreach (var rule in tokenDefinitions)
                {
                    var match = rule.Pattern.Match(source, currentIndex);

                    if (match.Success && (match.Index - currentIndex) == 0)
                    {
                        matchedDefinition = rule;
                        matchLength = match.Length;
                        break;
                    }
                }

                if (matchedDefinition == null)
                {
                    throw new Exception(string.Format("Unrecognized symbol '{0}' at index {1} (line {2}, column {3}).", source[currentIndex], currentIndex, currentLine, currentColumn));
                }
                else
                {
                    var value = source.Substring(currentIndex, matchLength);

                    if (!matchedDefinition.Ignore)
                        yield return new Token(matchedDefinition.Type, value, new TokenPosition(currentIndex, currentLine, currentColumn));

                    var endOfLineMatch = new Regex(@"(\n |\r |\r\n)").Match(value);
                    if (endOfLineMatch.Success)
                    {
                        currentLine += 1;
                        currentColumn = value.Length - (endOfLineMatch.Index + endOfLineMatch.Length);
                    }
                    else
                    {
                        currentColumn += matchLength;
                    }

                    currentIndex += matchLength;
                }
            }

            yield return null;
        }
    }

}
