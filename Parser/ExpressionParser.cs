using DSL.Evaluator.Expressions.BooleanExpressions.Comparators;
using DSL.Evaluator.Expressions.BooleanExpressions;
using DSL.Evaluator.Expressions.DotChainExpressions;
using DSL.Evaluator.Expressions.ListExpression;
using DSL.Evaluator.Expressions.NumberExpressions;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.Scope;
using DSL.Lexer;

namespace DSL.Parser
{
    internal partial class Parser
    {
        private readonly LexerStream stream;
        public IInstruction? CurrentInstruction;
        
        public Parser(LexerStream stream)
        {
            this.stream = stream;
        }
        private IExpression Exp(Scope<IDSLType> scope)
        {
            return Or(scope);
        }
        private IExpression Or(Scope<IDSLType> scope)
        {
            IExpression left = And(scope);
            while (stream.Match(TokenType.Or))
            {
                stream.Eat(TokenType.Or);
                left = new OrOperation(left, And(scope));
            }
            return left;
        }
        private IExpression And(Scope<IDSLType> scope)
        {
            IExpression left = Equality(scope);
            while (stream.Match(TokenType.And))
            {
                stream.Eat(TokenType.And);
                left = new AndOperation(left, Equality(scope));
            }
            return left;
        }
        private IExpression Equality(Scope<IDSLType> scope)
        {
            IExpression left = Compairson(scope);
            while (stream.Match(TokenType.Equal, TokenType.NotEqual))
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Equal:
                        stream.Eat(TokenType.Equal);
                        left = new Equal(left, Compairson(scope));
                        break;
                    case TokenType.NotEqual:
                        stream.Eat(TokenType.NotEqual);
                        left = new NotEqual(left, Compairson(scope));
                        break;
                }
            }
            return left;
        }
        private IExpression Compairson(Scope<IDSLType> scope)
        {
            IExpression left = Term(scope);
            while (stream.Match(TokenType.Less, TokenType.Greater, TokenType.LessOrEqual, TokenType.GreaterOrEqual))
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Less:
                        stream.Eat(TokenType.Less);
                        left = new Less(left, Term(scope));
                        break;
                    case TokenType.LessOrEqual:
                        stream.Eat(TokenType.LessOrEqual);
                        left = new LessOrEqual(left, Term(scope));
                        break;
                    case TokenType.Greater:
                        stream.Eat(TokenType.Greater);
                        left = new Greater(left, Term(scope));
                        break;
                    case TokenType.GreaterOrEqual:
                        stream.Eat(TokenType.GreaterOrEqual);
                        left = new GreaterOrEqual(left, Term(scope));
                        break;
                }
            }
            return left;
        }
        private IExpression Term(Scope<IDSLType> scope)
        {
            IExpression left = Factor(scope);
            while (stream.Match(TokenType.Sum, TokenType.Minus))
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Sum:
                        stream.Eat(TokenType.Sum);
                        left = new PlusOperation(left, Factor(scope));
                        break;
                    case TokenType.Minus:
                        stream.Eat(TokenType.Minus);
                        left = new MinusOperation(left, Factor(scope));
                        break;
                }
            }
            return left;
        }
        private IExpression Factor(Scope<IDSLType> scope)
        {
            IExpression left = Power(scope);
            while (stream.Match(TokenType.Star, TokenType.Slash))
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Star:
                        stream.Eat(TokenType.Star);
                        left = new MultiplicationOperation(left, Power(scope));
                        break;
                    case TokenType.Slash:
                        stream.Eat(TokenType.Slash);
                        left = new DivideOperation(left, Power(scope));
                        break;
                }
            }
            return left;
        }
        private IExpression Power(Scope<IDSLType> scope)
        {
            IExpression left = Unary(scope);
            while (stream.Match(TokenType.Power))
            {
                stream.Eat(TokenType.Power);
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
                    stream.Eat(TokenType.Minus);
                    return new OppositeOperator(DotChainingPattern(scope));
                case TokenType.Not:
                    stream.Eat(TokenType.Not);
                    return new NotOperation(DotChainingPattern(scope));
                default:
                    return DotChainingPattern(scope);
            }
        }
        private IExpression DotChainingPattern(Scope<IDSLType> scope)
        {
            IExpression left = Literal(scope);
            while (stream.Match(TokenType.dot, TokenType.OpenSquareBracket))
            {
                if (stream.Match(TokenType.OpenSquareBracket))
                {
                    stream.Eat(TokenType.OpenSquareBracket);
                    IExpression index = Exp(scope);
                    stream.Eat(TokenType.ClosedSquareBracket);
                    if (stream.Match(TokenType.VariableAssigmnet))
                    {
                        stream.Eat(TokenType.VariableAssigmnet);
                        IExpression value = Exp(scope);
                        left = new FunctionCall(left, "Set", new List<IExpression> { index, value });
                    }
                    else
                    {
                        left = new FunctionCall(left, "Get", new List<IExpression> { index });
                    }
                }
                else
                {
                    stream.Eat(TokenType.dot);
                    string method_or_propertyID = stream.Eat(TokenType.Identifier).Value;
                    if (stream.Match(TokenType.OpenParenthesis))
                    {
                        List<IExpression> args;
                        stream.Eat(TokenType.OpenParenthesis);
                        if (stream.Match(TokenType.ClosedParenthesis))
                        {
                            args = new List<IExpression>();
                        }
                        else
                        {
                            args = GetListOfExpressions(scope);
                        }
                        stream.Eat(TokenType.ClosedParenthesis);
                        left = new FunctionCall(left, method_or_propertyID, args);
                    }
                    else
                    {
                        if (stream.Match(TokenType.VariableAssigmnet))
                        {
                            stream.Eat(TokenType.VariableAssigmnet);
                            left = new PropertySetter(left, method_or_propertyID, Exp(scope));
                        }
                        else
                        {
                            left = new PropertyGetter(left, method_or_propertyID);
                        }

                    }
                }
            }
            return left;
        }
        private IExpression Literal(Scope<IDSLType> scope)
        {
            //TODO
            //Agregar Predicados
            Token current = stream.CurrentToken;
            switch (current.Type)
            {
                case TokenType.Number:
                    return new SimpleExpression(ParseToken(stream.Eat(TokenType.Number)));
                case TokenType.Bool:
                    return new SimpleExpression(ParseToken(stream.Eat(TokenType.Bool)));
                case TokenType.Identifier:
                    return new Variable(stream.Eat(TokenType.Identifier).Value, scope);
                case TokenType.OpenParenthesis:
                    stream.Eat(TokenType.OpenParenthesis);
                    IExpression res = Exp(scope);
                    stream.Eat(TokenType.ClosedParenthesis);
                    return res;
                case TokenType.OpenSquareBracket:
                    return ParseLIST(scope);
                default:
                    throw new Exception("Expression expceted");
            }
        }
        private IExpression ParseLIST(Scope<IDSLType> scope)
        {
            List<IExpression> list;
            stream.Eat(TokenType.OpenSquareBracket);
            if (stream.Match(TokenType.ClosedSquareBracket))
            {
                list = new List<IExpression>();
                stream.Eat(TokenType.ClosedSquareBracket);
            }
            else
            {
                list = GetListOfExpressions(scope);
                stream.Eat(TokenType.ClosedSquareBracket);
            }
            return new ListExpression(list);
        }
        private List<IExpression> GetListOfExpressions(Scope<IDSLType> scope)
        {
            List<IExpression> list = new()
            {
                Exp(scope)
            };
            while (stream.Match(TokenType.Comma))
            {
                stream.Eat(TokenType.Comma);
                list.Add(Exp(scope));
            }

            return list;
        }
        public IDSLType ParseToken(Token t)
        {
            if (t.Type == TokenType.Number)
            {
                Number res = 0;
                if (int.TryParse(t.Value, out var n))
                {
                    res = n;
                }
                else if (float.TryParse(t.Value, out var f))
                {
                    res = f;
                }
                else if (double.TryParse(t.Value, out var d))
                {
                    res = d;
                }
                return res;
            }
            else if (t.Type == TokenType.Bool)
            {
                return (Bool)bool.Parse(t.Value);
            }
            else
            {
                throw new NotImplementedException();
            }

        }
    }
}
