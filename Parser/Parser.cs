// Ignore Spelling: lexer DSL
using DSL.Lexer;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.Expressions.BooleanExpressions;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.Instructions.Statements;
using DSL.Evaluator.Instructions.Statements.ConditionalStatements;
using DSL.Evaluator.Instructions.Statements.LoopStatements;
using DSL.Evaluator.Instructions.Statements.SimpleStatements;
using DSL.Evaluator.Expressions.NumberExpressions;
using DSL.Evaluator.Expressions.BooleanExpressions.Comparators;
using DSL.Evaluator.Expressions.Variables;
using DSL.Evaluator.Scope;
using static System.Formats.Asn1.AsnWriter;
using System.ComponentModel.Design;

namespace DSL.Parser
{
    internal class Parser
    {
        private LexerStream stream;
        public IInstruction? CurrentInstruction;
        public Parser(LexerStream stream)
        {
            this.stream= stream;
        }
        #region Expression Parsing
        private IExpression Exp(Scope<IDSLType> scope)
        {
            return Or(scope);
        }
        private IExpression Or(Scope<IDSLType> scope)
        {
            IExpression left = And(scope);
            while (stream.CurrentToken.Type==TokenType.Or)
            {
                stream.Match(TokenType.Or);
                left = new OrOperation(left,And(scope));
            }
            return left;
        }
        private IExpression And(Scope<IDSLType> scope)
        {
            IExpression left = Equality(scope);
            while (stream.CurrentToken.Type==TokenType.And)
            {
                stream.Match(TokenType.And);
                left = new AndOperation(left, Equality(scope));
            }
            return left;
        }
        private IExpression Equality(Scope<IDSLType> scope)
        {
            IExpression left = Compairson(scope);
            TokenType[] allowedTokenTypes = new TokenType[]
            {
                TokenType.Equal,
                TokenType.NotEqual,
            };
            while (allowedTokenTypes.Contains(stream.CurrentToken.Type))
            {
                switch(stream.CurrentToken.Type)
                {
                    case TokenType.Equal:
                        stream.Match(TokenType.Equal);
                        left = new Equal(left, Compairson(scope));
                        break;
                    case TokenType.NotEqual:
                        stream.Match(TokenType.NotEqual);
                        left = new NotEqual(left, Compairson(scope));
                        break;
                }
            }
            return left;
        }
        private IExpression Compairson(Scope<IDSLType> scope)
        {
            IExpression left = Term(scope);
            TokenType[] allowedTokenTypes = new TokenType[]
            {
                TokenType.Less,
                TokenType.Greater,
                TokenType.LessOrEqual,
                TokenType.GreaterOrEqual,
            };
            while (allowedTokenTypes.Contains(stream.CurrentToken.Type))
            {
                switch(stream.CurrentToken.Type)
                {
                    case TokenType.Less:
                        stream.Match(TokenType.Less);
                        left = new Less(left, Term(scope));
                        break;
                    case TokenType.LessOrEqual:
                        stream.Match(TokenType.LessOrEqual);
                        left = new LessOrEqual(left, Term(scope));
                        break;
                    case TokenType.Greater:
                        stream.Match(TokenType.Greater);
                        left =  new Greater(left, Term(scope));
                        break;
                    case TokenType.GreaterOrEqual:
                        stream.Match(TokenType.GreaterOrEqual);
                        left = new GreaterOrEqual(left, Term(scope));
                        break;
                }
            }
            return left;
        }
        private IExpression Term(Scope<IDSLType> scope)
        {
            IExpression left = Factor(scope);
            TokenType[] allowedTokenTypes = new TokenType[]
            {
                TokenType.Sum,
                TokenType.Minus,
            };
            while (allowedTokenTypes.Contains(stream.CurrentToken.Type))
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Sum:
                        stream.Match(TokenType.Sum);
                        left = new PlusOperation(left, Factor(scope));
                        break;
                    case TokenType.Minus:
                        stream.Match(TokenType.Minus);
                        left = new MinusOperation(left, Factor(scope));
                        break;
                }
            }
            return left;
        }
        private IExpression Factor(Scope<IDSLType> scope)
        {
            IExpression left = Power(scope);
            while (stream.CurrentToken.Type==TokenType.Star || stream.CurrentToken.Type==TokenType.Slash)
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Star:
                        stream.Match(TokenType.Star);
                        left = new MultiplicationOperation(left, Power(scope));
                        break;
                    case TokenType.Slash:
                        stream.Match(TokenType.Slash);
                        left = new DivideOperation(left, Power(scope));
                        break;
                }
            }
            return left;
        }
        private IExpression Power(Scope<IDSLType> scope)
        {
            IExpression left = Unary(scope);
            while (stream.CurrentToken.Type==TokenType.Power)
            {
                stream.Match(TokenType.Power);
                left = new PowerOperation(left, Power(scope));
            }
            return left;
        }
        private IExpression Unary(Scope<IDSLType> scope)
        {
            Token current = stream.CurrentToken;
            switch (current.Type)
            {
                case TokenType.Minus:
                    stream.Match(TokenType.Minus);
                    return new OppositeOperator(Literal(scope));
                case TokenType.Not:
                    stream.Match(TokenType.Not);
                    return new NotOperation(Literal(scope));
                default:
                      return Literal(scope); 
            }
        }
        private IExpression Literal(Scope<IDSLType> scope)
        {
            Token current = stream.CurrentToken;
            switch (current.Type)
            {
                case TokenType.Number:
                    return new SimpleExpression(ParseToken(stream.Match(TokenType.Number)));
                case TokenType.Bool:
                    return new SimpleExpression(ParseToken(stream.Match(TokenType.Bool)));
                case TokenType.Identifier:
                    return new Variable(stream.Match(TokenType.Identifier).Value,scope);
                case TokenType.OpenParenthesis:
                    stream.Match(TokenType.OpenParenthesis);
                    IExpression res = Exp(scope);
                    stream.Match(TokenType.ClosedParenthesis);
                    return res;
                case TokenType.OpenSquareBracket:
                    return ParseLIST(scope);
                default:
                    throw new Exception("Expression expceted");
            }
        }
        private IExpression ParseLIST(Scope<IDSLType> scope)
        {
            stream.Match(TokenType.OpenSquareBracket);
            List<IExpression> list = GetListOfExpressions(scope);
            stream.Match(TokenType.ClosedSquareBracket);
            return new ListExpression(list);
        }
        private List<IExpression> GetListOfExpressions(Scope<IDSLType> scope)
        {
            List<IExpression> list = new()
            {
                Exp(scope)
            };
            while (stream.CurrentToken.Type == TokenType.Comma)
            {
                stream.Match(TokenType.Comma);
                list.Add(Exp(scope));
            }

            return list;
        }
        public IDSLType ParseToken(Token t)
        {
            if (t.Type==TokenType.Number)
            {
               Number res = 0;
               if(int.TryParse(t.Value, out var n))
               {
                    res = n;
               }
               else if (float.TryParse(t.Value,out var f))
               {
                    res = f;
               }
               else if(double.TryParse(t.Value,out var d))
               {
                    res = d;
               }
               return res;
            }
            else if(t.Type==TokenType.Bool)
            {
                return (Bool)bool.Parse(t.Value);
            }
            else
            {
                throw new NotImplementedException();
            }
            
        }
        #endregion

        #region Instructions Parsing
        private bool TryParseStatement(Scope<IDSLType> scope,List<IInstruction> instructions)
        {
            switch (stream.CurrentToken.Type)
            {
                case TokenType.Print:
                    instructions.Add(ParsePRINT(scope));
                    return true;
                case TokenType.If:
                    instructions.Add(ParseIF(scope));
                    return true;
                case TokenType.While:
                    instructions.Add(ParseWHILE(scope));
                    return true;
                case TokenType.For:
                    instructions.Add(ParseFor(scope));
                    return true;
                default:
                    return false;
            }
        }
    

        private FunctionCall ParseFunctionCall(Scope<IDSLType> scope)
        {
            IExpression exp = Exp(scope);
            stream.Match(TokenType.dot);
            string functionName = stream.Match(TokenType.Identifier).Value;
            stream.Match(TokenType.OpenParenthesis);
            List<IExpression> parameters = GetListOfExpressions(scope);
            stream.Match(TokenType.ClosedParenthesis);
            return new FunctionCall(exp, functionName, parameters);
        }
        private ForStatement ParseFor(Scope<IDSLType> scope)
        {
            stream.Match(TokenType.For);
            stream.Match(TokenType.OpenParenthesis);
            string forVariable = stream.Match(TokenType.Identifier).Value;
            stream.Match(TokenType.In);
            IExpression list = Exp(scope);
            stream.Match(TokenType.ClosedParenthesis);
            stream.Match(TokenType.OpenCurlyBracket);
            InstructionBlock instructionBlock = ParseInstructionBlock(scope);
            stream.Match(TokenType.ClosedCurlyBracket);
            return new ForStatement(forVariable, list, instructionBlock,scope);
        }
        private WhileStatement ParseWHILE(Scope<IDSLType> scope)
        {
            stream.Match(TokenType.While);
            stream.Match(TokenType.OpenParenthesis);
            IExpression condition = Exp(scope);
            stream.Match(TokenType.ClosedParenthesis);
            stream.Match(TokenType.OpenCurlyBracket);
            InstructionBlock block = ParseInstructionBlock(scope);
            stream.Match(TokenType.ClosedCurlyBracket);
            return new WhileStatement(condition, block);
        }
        private IfStatement ParseIF(Scope<IDSLType> scope)
        {
            stream.Match(TokenType.If);
            stream.Match(TokenType.OpenParenthesis);
            IExpression condition = Exp(scope);
            stream.Match(TokenType.ClosedParenthesis);
            stream.Match(TokenType.OpenCurlyBracket);
            InstructionBlock block = ParseInstructionBlock(scope);
            stream.Match(TokenType.ClosedCurlyBracket);
            return new IfStatement(condition, block);
        }
        private InstructionBlock ParseInstructionBlock(Scope<IDSLType> parentScope)
        {
            List<IInstruction> instructions = new ();
            Scope<IDSLType> scope = new(parentScope);  
            
            List<TokenType> tokens = new()
            {
                TokenType.If,
                TokenType.While,
                TokenType.Print,
                TokenType.Identifier,
                TokenType.For
            };

            while (tokens.Contains(stream.CurrentToken.Type))
            {
                if (TryParseStatement(scope, instructions)) ;
                else if (TryParseAsignation(scope, instructions)) ;
                else if (TryParseFunctionCall(scope, instructions)) ;
                else if (TryParseDecrementIncrementOperators(scope, instructions)) ;
                else throw new Exception("Just statements, calls, asigments, increasing and decrasing operators are valid instructions");
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Print:
                        instructions.Add(ParsePRINT(scope));
                        break;
                    case TokenType.If:
                        instructions.Add(ParseIF(scope));
                        break;
                    case TokenType.While:
                        instructions.Add(ParseWHILE(scope));
                        break;
                    case TokenType.For:
                        instructions.Add(ParseFor(scope));
                        break;  
                    case TokenType.Identifier:
                        instructions.Add(ParseWITHIDINSTRUCTION(scope));
                        break;
                    case TokenType.EOF:
                        instructions.Add(new EndInstruction());
                        break;
                }
            }
            return new InstructionBlock(instructions, scope);
        }

        private bool TryParseDecrementIncrementOperators(Scope<IDSLType> scope, List<IInstruction> instructions)
        {
            throw new NotImplementedException();
        }

        private bool TryParseFunctionCall(Scope<IDSLType> scope, List<IInstruction> instructions)
        {
            throw new NotImplementedException();
        }

        private bool TryParseAsignation(Scope<IDSLType> scope, List<IInstruction> instructions)
        {
            throw new NotImplementedException();
        }

        private VariableDeclarationStatement ParseVariableDeclaration(Scope<IDSLType> scope) 
        {
            string id = stream.Match(TokenType.Identifier).Value;
            stream.Match(TokenType.VariableAssigmnet);
            IExpression exp = Exp(scope);
            stream.Match(TokenType.SemiColon);
            return new VariableDeclarationStatement(scope, id,exp);
        }
        private IInstruction ParseWITHIDINSTRUCTION(Scope<IDSLType> scopeVariables)
        {
            return ParseVariableDeclaration(scopeVariables);
        }
        private PrintStatement ParsePRINT(Scope<IDSLType> scope)
        {
            stream.Match(TokenType.Print);
            stream.Match(TokenType.OpenParenthesis);
            IExpression str = Exp(scope);
            stream.Match(TokenType.ClosedParenthesis);
            stream.Match(TokenType.SemiColon);
            return new PrintStatement(str);
        }
        
        #endregion
        public void NextInstruction()
        {
           CurrentInstruction =  ParseInstructionBlock(null);
        }
    }
}
