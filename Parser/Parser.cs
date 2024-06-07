using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Lexer;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.Expressions.BooleanExpressions;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.Instructions.Statements;
using DSL.Evaluator.Instructions.Statements.ConditionalStatements;
using DSL.Evaluator.Instructions.Statements.LoopStatements;
using DSL.Evaluator.Instructions.Statements.SimpleStatements;
using System.ComponentModel;



namespace DSL.Parser
{
    internal class Parser
    {
        private DSL.Lexer.Lexer lexer;
        public Instruction CurrentInstruction;

        public Parser(DSL.Lexer.Lexer lexer)
        {
            this.lexer = lexer;
        }
        private Expression<T> ParseExpression<T>(List<List<TokenType>> operatorsHierarchy)
        {
            return ParseExpression<T>(operatorsHierarchy, operatorsHierarchy.Count - 1);
        }
        private Expression<T> ParseExpression<T>(List<List<TokenType>> operatorsHierarchy, int currentLevel)
        {
            // Base Case
            if (currentLevel == 0)
            {
                if (operatorsHierarchy[0].Contains(lexer.CurrentToken.Type))
                {
                    TokenType currentTokenType = lexer.CurrentToken.Type;
                    lexer.NextToken();
                    return Factory<T>.InstaciateUnaryOperator(currentTokenType, ParseExpression<T>(operatorsHierarchy, operatorsHierarchy.Count - 1));
                }
                else if (lexer.CurrentToken.Type == TokenType.OpenParenthesis)
                {
                    lexer.NextToken();
                    Expression<T> result = ParseExpression<T>(operatorsHierarchy, operatorsHierarchy.Count - 1);
                    Match(TokenType.ClosedParenthesis);
                    return result;
                }
                else
                {
                    T value = ParseToken<T>(lexer.CurrentToken.Value);
                    lexer.NextToken();
                    return new SimpleExpression<T>(value);
                }
            }
            else
            {
                Expression<T> left = ParseExpression<T>(operatorsHierarchy, currentLevel - 1);
                while (operatorsHierarchy[currentLevel].Contains(lexer.CurrentToken.Type))
                {
                    TokenType currentTokenType = lexer.CurrentToken.Type;
                    lexer.NextToken();
                    left = Factory<T>.InstaciateBinaryOperator(currentTokenType, left, ParseExpression<T>(operatorsHierarchy, currentLevel - 1));
                }
                return left;
            }
        }
        public static T ParseToken<T>(string token)
        {
            // Use the TryParse pattern to convert the string to the specified type T
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null && converter.IsValid(token))
            {
                return (T)converter.ConvertFromString(token);
            }
            throw new ArgumentException("Invalid token for conversion", nameof(token));
        }

        private WhileStatement ParseWHILE()
        {
            Match(TokenType.While);
            Match(TokenType.OpenParenthesis);
            Expression<bool> condition = ParseBooleanExpression();
            Match(TokenType.ClosedParenthesis);
            Match(TokenType.OpenCurlyBracket);
            InstructionBlock block = ParseInstructionBlock();
            Match(TokenType.ClosedCurlyBracket);
            return new WhileStatement(condition, block);
        }
        private IfStatment ParseIF()
        {
            Match(TokenType.If);
            Match(TokenType.OpenParenthesis);
            Expression<bool> condition = ParseBooleanExpression();
            Match(TokenType.ClosedParenthesis);
            Match(TokenType.OpenCurlyBracket);
            InstructionBlock block = ParseInstructionBlock();
            Match(TokenType.ClosedCurlyBracket);
            return new IfStatment(condition, block);
        }
        private InstructionBlock ParseInstructionBlock()
        {
            List<Instruction> instructions = new List<Instruction>();
            Dictionary<string, object> scopeVariables = new Dictionary<string, object>();
            List<TokenType> tokens = new()
            {
                TokenType.If,
                TokenType.While,
                TokenType.Print,
                TokenType.Identifier
            };

            while (tokens.Contains(lexer.CurrentToken.Type))
            {
                switch (lexer.CurrentToken.Type)
                {
                    case TokenType.If:
                        instructions.Add(ParseIF());
                        break;
                    case TokenType.While:
                        instructions.Add(ParseWHILE());
                        break;
                    case TokenType.Print:
                        instructions.Add(ParsePRINT());
                        break;
                    case TokenType.Identifier:
                        instructions.Add(ParseWITHIDINSTRUCTION(scopeVariables));
                        break;
                }
            }
            return new InstructionBlock(instructions, scopeVariables);
        }
        private VariableDeclarationStatement<T> ParseVariableDeclaration<T>(Dictionary<string, object> scopeVariables)
        {
            string id = Match(TokenType.Identifier).Value;
            Match(TokenType.VariableAssigmnet);
            Expression<T> exp = ParseExpression<T>();
            return new VariableDeclarationStatement<T>(scopeVariables, id, exp);
        }
        private Expression<T> ParseExpression<T>()
        {
            throw new NotImplementedException();
        }
        private Instruction ParseWITHIDINSTRUCTION(Dictionary<string, object> scopeVariables)
        {
            return ParseVariableDeclaration<bool>(scopeVariables);
        }
        private PrintStatement ParsePRINT()
        {
            Match(TokenType.Print);
            Match(TokenType.OpenParenthesis);
            Expression<string> str = ParseStringExpression();
            Match(TokenType.ClosedParenthesis);
            Match(TokenType.SemiColon);
            return new PrintStatement(str);
        }    
        private Expression<string> ParseStringExpression()
        {
            return ParseExpression<string>(new List<List<TokenType>> { new List<TokenType>() });
        }
        private Expression<bool> ParseBooleanExpression()
        {
            return ParseExpression<bool>(new List<List<TokenType>> { new List<TokenType> { TokenType.Not }, new List<TokenType> { TokenType.And }, new List<TokenType> { TokenType.Or } });
        }
        public void NextInstruction()
        {
            switch (lexer.CurrentToken.Type)
            {
                case TokenType.If:
                    CurrentInstruction = ParseIF();
                    break;
                case TokenType.While:
                    CurrentInstruction = ParseWHILE();
                    break;
                case TokenType.Print:
                    CurrentInstruction = ParsePRINT();
                    break;
                case TokenType.EOF:
                    CurrentInstruction = new EndInstruction();
                    break;
                default:
                    throw new Exception("Not Implemented Exception");
            }
        }
        public Token Match(TokenType tokenType)
        {
            if (lexer.CurrentToken.Type == tokenType)
            {
                Token result = new Token(lexer.CurrentToken);
                lexer.NextToken();
                return result;
            }
            throw new NotImplementedException($"Sintax error {tokenType} expected");

        }
    }
}
