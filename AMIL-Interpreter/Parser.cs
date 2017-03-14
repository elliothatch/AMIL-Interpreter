using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amil
{
    public class Parser
    {
        public Parser()
        {
        }

        public Statement parseStatement(IEnumerator<Token> enumerator)
        {
            Statement output = null;
            Token token = enumerator.Current;
            switch (token.Type)
            {
                case TokenType.LBRACE:
                    BlockStatement block = new BlockStatement();
                    enumerator.MoveNext();
                    do
                    {
                        block.statements.Add(parseStatement(enumerator));
                    } while (enumerator.Current.Type != TokenType.RBRACE);
                    enumerator.MoveNext();
                    return block; //no semicolon
                case TokenType.IF:
                    enumerator.MoveNext();
                    IfStatement ifStatement = new IfStatement(parseExpression(enumerator), parseStatement(enumerator));
                    return ifStatement; //no semicolon
                case TokenType.VAR:
                    enumerator.MoveNext();
                    output = new DeclarationStatement(parseExpression(enumerator));
                    break;
                case TokenType.PRINT:
                    enumerator.MoveNext();
                    output = new PrintStatement(parseExpression(enumerator));
                    break;
                default:
                    //assume it's an expression
                    output = new ExpressionStatement(parseExpression(enumerator));
                    break;
            }
            
            if (enumerator.Current.Type != TokenType.SEMICOLON)
            {
                throw new Exception(
                    string.Format("Expected semicolon at index {0} (line {1}, column {2}).",
                        enumerator.Current.Position.Index,
                        enumerator.Current.Position.Line,
                        enumerator.Current.Position.Column));
            }
            enumerator.MoveNext();
            return output;
        }

        public Expression parseExpression(IEnumerator<Token> enumerator)
        {
            Expression output = null;
            Token token = enumerator.Current;
            switch(token.Type)
            {
                case TokenType.LPAREN:
                    /*
                    BlockStatement block = new BlockStatement();
                    enumerator.MoveNext();
                    do
                    {
                        block.statements.Add(parseStatement(enumerator));
                    } while (enumerator.Current.Type != TokenType.RBRACE);
                    enumerator.MoveNext();
                    */
                    break;
                case TokenType.NAME:
                    output = new NameExpr(token.Value);
                    break;
                case TokenType.INTEGER:
                    output = new ConstantExpr(new IntegerValue(Int32.Parse(token.Value)));
                    break;
                case TokenType.BOOLEAN:
                    output = new ConstantExpr(new BooleanValue(Boolean.Parse(token.Value)));
                    break;
                default:
                    throw new Exception(
                        string.Format("Invalid token '{0}' at index {1} (line {2}, column {3}).",
                            token.Value,
                            token.Position.Index,
                            token.Position.Line,
                            token.Position.Column));
            }

            enumerator.MoveNext();
            return output;
        }

        public Statement parse(IEnumerable<Token> tokens)
        {
            IEnumerator<Token> enumerator = tokens.GetEnumerator();
            enumerator.MoveNext();
            return parseStatement(enumerator);
        }
    }

    public enum StatementType
    {
        BLOCK,
        DECLARATION,
        IF,
        PRINT,
        EXPRESSION,
    }

    public abstract class Statement
    {
        public readonly StatementType sType;
        public Statement(StatementType sType)
        {
            this.sType = sType;
        }
    }
    public class BlockStatement : Statement
    {
        public List<Statement> statements;
        public BlockStatement()
            : base(StatementType.BLOCK)
        {
            statements = new List<Statement>();
        }

        public void parse(IEnumerator<Token> enumerator)
        {
        }
    }

    public class DeclarationStatement : Statement
    {
        public NameExpr name;
        public DeclarationStatement(Expression name)
            : base(StatementType.DECLARATION)
        {
            if(name.eType != ExpressionType.NAME)
            {
                throw new Exception("Variable declaration must be a name");
            }
            this.name = (NameExpr)name;
        }
    }

    public class IfStatement : Statement
    {
        public Expression condition;
        public Statement body;
        public IfStatement(Expression condition, Statement body)
            : base(StatementType.IF)
        {
            this.condition = condition;
            this.body = body;
        }
    }

    public class PrintStatement : Statement
    {
        public Expression expr;
        public PrintStatement(Expression expr)
            : base(StatementType.PRINT)
        {
            this.expr = expr;
        }
    }

    public class ExpressionStatement : Statement
    {
        public Expression expr;
        public ExpressionStatement(Expression expr)
            : base(StatementType.EXPRESSION)
        {
            this.expr = expr;
        }
    }

    public enum ValueType
    {
        INTEGER,
        BOOLEAN
    }

    public abstract class Value
    {
        public readonly ValueType vType;
        public Value(ValueType vType)
        {
            this.vType = vType;
        }
    }

    public class IntegerValue : Value
    {
        public int n;
        public IntegerValue(int n)
            :base(ValueType.INTEGER)
        {
            this.n = n;
        }
    }

    public class BooleanValue : Value
    {
        public bool b;
        public BooleanValue(bool b)
            : base(ValueType.BOOLEAN)
        {
            this.b = b;
        }
    }


    public enum ExpressionType
    {
        CONSTANT,
        NAME,
        BINARY_OP
    }

    public abstract class Expression
    {
        public readonly ExpressionType eType;
        public Expression(ExpressionType eType)
        {
            this.eType = eType;
        }
    }

    public class ConstantExpr : Expression
    {
        public readonly Value value;
        public ConstantExpr(Value value)
            :base(ExpressionType.CONSTANT)
        {
            this.value = value;
        }
    }

    public class NameExpr : Expression
    {
        public readonly string name;
        public NameExpr(string name)
            :base(ExpressionType.NAME)
        {
            this.name = name;
        }
    }

    class BinaryOpExpr : Expression
    {
        public Expression lhs;
        public Expression rhs;
        public BinaryOpExpr()
            :base(ExpressionType.BINARY_OP)
        {

        }
    }

    /* is a binary op
    class AssignmentExpr : Expression
    {
        public NameExpr name;
        public Expression value;
        public AssignmentExpr(Expression name, Expression value)
            : base(ExpressionType.ASSIGNMENT)
        {
            if (name.eType != ExpressionType.NAME)
            {
                throw new Exception("Assignment LHS must be a name");
            }
            this.name = (NameExpr)name;
            this.value = value;
        }
    }*/

}
