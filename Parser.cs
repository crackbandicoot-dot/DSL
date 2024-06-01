using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Instructions;
using DSL.Instructions.Expressions;
using DSL.Instructions.Expressions.BooleanExpressions;

namespace DSL
{
    internal class Parser
    {
        private Lexer lexer;
        public Instruction CurrentInstruction;
        
        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }
        public void NextInstruction()
        {
            switch (lexer.CurrentToken.Type)
            {
                case TokenType.Bool:
                    CurrentInstruction = Disjunction();
                    break;
                case TokenType.OpenParenthesis:
                    CurrentInstruction = Disjunction();
                    break;
                default:
                    throw new Exception("Not Implemented Exception");    
            }
        }
        public Token Match(TokenType tokenType)
        {
            if (lexer.CurrentToken.Type==tokenType)
            {
                Token result = new Token(lexer.CurrentToken);
                lexer.NextToken();
                return result;
            }
            throw new NotImplementedException($"Sintaxis error {tokenType} expected");
           
        }

        public Expression<bool> Conjunction()
        {
            Expression<bool> left = BooleanLiteral();
            while (lexer.CurrentToken.Type == TokenType.And)
            {
                lexer.NextToken();
                left = new AndOperation(left, BooleanLiteral());
            }
            return left;
        }
        public Expression<bool> Disjunction()
        {
            Expression<bool> left = Conjunction();
            while (lexer.CurrentToken.Type==TokenType.Or)
            {
                lexer.NextToken();
                left = new OrOperation(left,Conjunction());
            }
            return left;
        }
      
        public Expression<bool> BooleanLiteral()
        {
            if (lexer.CurrentToken.Type==TokenType.Bool)
            {
                
                return new SimpleExpression<bool>(bool.Parse(Match(TokenType.Bool).Value));
            }
            else if (lexer.CurrentToken.Type==TokenType.OpenParenthesis)
            {
                lexer.NextToken();
                Expression<bool> booleanExpression =Disjunction();
                Match(TokenType.ClosedParenthesis);
                return booleanExpression;
            }
            else if (lexer.CurrentToken.Type==TokenType.Not)
            {
                lexer.NextToken();
                return new Not(BooleanLiteral());
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
 