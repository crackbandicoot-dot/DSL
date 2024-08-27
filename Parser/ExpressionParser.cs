using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.AST.Expressions.AnonimusTypeExpression;
using DSL.Evaluator.AST.Expressions.AssignationExpressions;
using DSL.Evaluator.AST.Expressions.BooleanExpressions;
using DSL.Evaluator.AST.Expressions.BooleanExpressions.Comparators;
using DSL.Evaluator.AST.Expressions.DotChainExpressions;
using DSL.Evaluator.AST.Expressions.LambdaExpressions;
using DSL.Evaluator.AST.Expressions.ListExpression;
using DSL.Evaluator.AST.Expressions.NumberExpressions;
using DSL.Evaluator.AST.Expressions.Scope;
using DSL.Evaluator.AST.Expressions.StringExpressions;
using DSL.Evaluator.AST.Expressions.TernaryExpressions;
using DSL.Evaluator.AST.Expressions.TypeRestrictionExpression;
using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.AST.Instructions.Statements;
using DSL.Lexer;
using System;
using System.Collections.Generic;
using System.Globalization;
namespace DSL.Parser
{
    internal partial class ProgramParser
    {
        private readonly LexerStream stream;

        public ProgramParser(LexerStream stream)
        {
            this.stream = stream;
        }
        private IExpression Exp(Scope scope)
        {
            return Assignation(scope);
        }
        private IExpression Assignation(Scope scope)
        {
            IExpression left = TernaryOperation(scope);
            while (stream.Match(TokenType.VariableAssigmnet, TokenType.SumAssigment,
                TokenType.MinusAssigment,TokenType.StarAssigment))
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.VariableAssigmnet:
                        stream.Eat(TokenType.VariableAssigmnet);
                        left = new Assignation(left, Assignation(scope));
                        break;
                    case TokenType.SumAssigment:
                        stream.Eat(TokenType.SumAssigment);
                        left = new Assignation(left, new PlusOperation(left, Assignation(scope)));
                        break;
                    case TokenType.MinusAssigment:
                        stream.Eat(TokenType.SumAssigment);
                        left = new Assignation(left, new MinusOperation(left, Assignation(scope)));
                        break;
                    case TokenType.StarAssigment:
                        stream.Eat(TokenType.StarAssigment);
                        left = new Assignation(left, new MultiplicationOperation(left, Assignation(scope)));
                        break;
                }

            }

            return left;
        }
        private IExpression TernaryOperation(Scope scope)
        {
            IExpression condition = Or(scope);
            if (stream.Match(TokenType.Questioning))
            {
                stream.Eat(TokenType.Questioning);
                IExpression trueOption = Exp(scope);
                stream.Eat(TokenType.PropertyAssigment);
                IExpression falseOption = Exp(scope);
                return new TernaryExpression(condition, trueOption, falseOption);
            }
            return condition;
        }
        private IExpression Or(Scope scope)
        {
            IExpression left = And(scope);
            while (stream.Match(TokenType.Or))
            {
                stream.Eat(TokenType.Or);
                left = new OrOperation(left, And(scope));
            }
            return left;
        }
        private IExpression And(Scope scope)
        {
            IExpression left = Equality(scope);
            while (stream.Match(TokenType.And))
            {
                stream.Eat(TokenType.And);
                left = new AndOperation(left, Equality(scope));
            }
            return left;
        }
        private IExpression Equality(Scope scope)
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
        private IExpression Compairson(Scope scope)
        {
            IExpression left = Concatenation(scope);
            while (stream.Match(TokenType.Less, TokenType.Greater, TokenType.LessOrEqual, TokenType.GreaterOrEqual))
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Less:
                        stream.Eat(TokenType.Less);
                        left = new Less(left, Concatenation(scope));
                        break;
                    case TokenType.LessOrEqual:
                        stream.Eat(TokenType.LessOrEqual);
                        left = new LessOrEqual(left, Concatenation(scope));
                        break;
                    case TokenType.Greater:
                        stream.Eat(TokenType.Greater);
                        left = new Greater(left, Concatenation(scope));
                        break;
                    case TokenType.GreaterOrEqual:
                        stream.Eat(TokenType.GreaterOrEqual);
                        left = new GreaterOrEqual(left, Concatenation(scope));
                        break;
                }
            }
            return left;
        }
        private IExpression Concatenation(Scope scope)
        {
            IExpression left = Term(scope);
            while (stream.Match(TokenType.Concatenation, TokenType.ConcatenationWithSpaces))
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Concatenation:
                        stream.Eat(TokenType.Concatenation);
                        left = new ConcatenationOperation(left, Term(scope));
                        break;
                    case TokenType.ConcatenationWithSpaces:
                        stream.Eat(TokenType.ConcatenationWithSpaces);
                        left = new ConcatenationWithSpacesOperation(left, Term(scope));
                        break;
                }
            }
            return left;
        }
        private IExpression Term(Scope scope)
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
        private IExpression Factor(Scope scope)
        {
            IExpression left = Power(scope);
            while (stream.Match(TokenType.Star, TokenType.Slash, TokenType.Modulo))
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
                    case TokenType.Modulo:
                        stream.Eat(TokenType.Modulo);
                        left = new ModuloOperation(left, Power(scope));
                        break;
                }
            }
            return left;
        }
        private IExpression Power(Scope scope)
        {
            IExpression left = Unary(scope);
            while (stream.Match(TokenType.Power))
            {
                stream.Eat(TokenType.Power);
                left = new PowerOperation(left, Power(scope));
            }
            return left;
        }
        private IExpression Unary(Scope scope)
        {
            Token current = stream.CurrentToken;
            switch (current.Type)
            {
                case TokenType.Minus:
                    stream.Eat(TokenType.Minus);
                    return new OppositeOperator(FieldAcces(scope));
                case TokenType.Not:
                    stream.Eat(TokenType.Not);
                    return new NotOperation(FieldAcces(scope));
                default:
                    return FieldAcces(scope);
            }
        }
        private IExpression FieldAcces(Scope scope)
        {
            IExpression left = Atom(scope);
            while (stream.Match(TokenType.dot,
                TokenType.OpenSquareBracket,
                TokenType.Decrement, TokenType.Increment))
            {
                if (stream.MatchPrefix(  // .id'('
                    TokenType.dot, TokenType.Identifier,
                    TokenType.OpenParenthesis)
                    )
                {
                    left = FunctionCall(left, scope);
                }
                else if (stream.MatchPrefix
                    (TokenType.dot, TokenType.Identifier)) //.id
                {
                    left = PropertyGetter(left, scope);
                }
                else if (stream.Match(TokenType.Increment))
                {
                    left = Increment(left, scope);
                }
                else if (stream.Match(TokenType.Decrement))
                {
                    left = Decrement(left, scope);
                }
                else //[
                {
                    left = Indexer(left, scope);
                }
            }
            return left;
        }
        private IExpression Decrement(IExpression left, Scope scope)
        {
            stream.Eat(TokenType.Decrement);
            return new DecrementOperator(left);
        }
        private IExpression Increment(IExpression left, Scope scope)
        {
            stream.Eat(TokenType.Increment);
            return new IncrementOperation(left);
        }
        private IExpression Atom(Scope scope)
        {
            Token current = stream.CurrentToken;
            switch (current.Type)
            {
                case TokenType.NumberType or TokenType.BooleanType or TokenType.StringType:
                    return new TypeRestrictionExpression(stream.Eat(TokenType.NumberType, TokenType.BooleanType, TokenType.StringType).Value);
                case TokenType.OpenCurlyBracket:
                    return AnonimusTypeExpression(scope);
                case TokenType.String:
                    return new SimpleExpression(ParseToken(stream.Eat(TokenType.String)));
                case TokenType.Number:
                    return new SimpleExpression(ParseToken(stream.Eat(TokenType.Number)));
                case TokenType.Bool:
                    return new SimpleExpression(ParseToken(stream.Eat(TokenType.Bool)));
                case TokenType.Identifier:
                    return new Variable(stream.Eat(TokenType.Identifier), scope);
                case TokenType.OpenParenthesis:
                    if (
                        stream.MatchPrefix
                        (
                        TokenType.OpenParenthesis, //()
                        TokenType.ClosedParenthesis
                        )
                        ||
                        stream.MatchPrefix
                        (TokenType.OpenParenthesis, //(id)=>
                        TokenType.Identifier,
                        TokenType.ClosedParenthesis,
                        TokenType.FunctionAssigment)
                        || stream.MatchPrefix
                        (TokenType.OpenParenthesis,
                        TokenType.Identifier,      //(id,
                        TokenType.Comma
                        )
                        )
                    {
                        return LambdaExpression(scope);
                    }
                    else
                    {
                        return Group(scope);
                    }

                case TokenType.OpenSquareBracket:
                    return List(scope);
                default:
                    throw new Exception($"Expression expceted in{stream.CurrentToken.Pos}");
            }
        }
        #region Auxiliar Methods
        private IExpression Indexer(IExpression left, Scope scope)
        {
            stream.Eat(TokenType.OpenSquareBracket);
            IExpression exp = Exp(scope);
            stream.Eat(TokenType.ClosedSquareBracket);
            return new PropertyGetter(left, "Indexer", new List<IExpression> { exp });

        }
        private IExpression PropertyGetter(IExpression left, Scope scope)
        {
            stream.Eat(TokenType.dot);
            string ID = stream.Eat(TokenType.Identifier).Value;
            return new PropertyGetter(left, ID);
        }
        private IExpression FunctionCall(IExpression left, Scope scope)
        {
            stream.Eat(TokenType.dot);
            string id = stream.Eat(TokenType.Identifier).Value;
            List<IExpression> args;
            stream.Eat(TokenType.OpenParenthesis);
            if (stream.Match(TokenType.ClosedParenthesis))
            {
                args = new();
            }
            else
            {
                args = GetListOfExpressions(scope);
            }
            stream.Eat(TokenType.ClosedParenthesis);
            return new FunctionCall(left, id, args);
        }
        private IExpression LambdaExpression(Scope scope)
        {
            Scope child = new(scope);
            List<string> parameters = new();
            if (stream.MatchPrefix(TokenType.OpenParenthesis,
                TokenType.ClosedParenthesis))
            {
                stream.Eat(TokenType.OpenParenthesis, TokenType.ClosedParenthesis);
            }
            else
            {
                stream.Eat(TokenType.OpenParenthesis);
                parameters.Add(stream.Eat(TokenType.Identifier).Value);
                while (stream.Match(TokenType.Comma))
                {
                    stream.Eat(TokenType.Comma);
                    parameters.Add(stream.Eat(TokenType.Identifier).Value);
                }
                stream.Eat(TokenType.ClosedParenthesis);
            }
            stream.Eat(TokenType.FunctionAssigment);
            if (stream.Match(TokenType.OpenCurlyBracket))
            {
                InstructionBlock instructionBlock = InstructionBlock(scope);
                return new ActionExpression(parameters.ToArray(), instructionBlock);
            }
            else
            {
                IExpression exp = Exp(child);
                return new DelegateExpression(parameters.ToArray(), exp, child);
            }
        }
        private IExpression Group(Scope scope)
        {
            stream.Eat(TokenType.OpenParenthesis);
            IExpression res = Exp(scope);
            stream.Eat(TokenType.ClosedParenthesis);
            return res;
        }
        private IExpression List(Scope scope)
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
        private List<IExpression> GetListOfExpressions(Scope scope)
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
        public object ParseToken(Token t)
        {
            if (t.Type == TokenType.String)
            {
                return t.Value;
            }
            else if (t.Type == TokenType.Number)
            {
                return double.Parse(t.Value,CultureInfo.InvariantCulture);
            }
            else
            {
                return bool.Parse(t.Value);
            }
        }
        private IExpression AnonimusTypeExpression(Scope scope)
        {
            Dictionary<Token, IExpression> properties = new();
            if (stream.MatchPrefix(TokenType.OpenCurlyBracket,
                TokenType.ClosedCurlyBracket))
            {
                stream.Eat(TokenType.OpenCurlyBracket);
                stream.Eat(TokenType.ClosedCurlyBracket);
                return new AnonimusTypeExpression(properties);
            }
            else
            {
                stream.Eat(TokenType.OpenCurlyBracket);
                Property(properties, scope);
                while (stream.Match(TokenType.Comma))
                {
                    stream.Eat(TokenType.Comma);
                    Property(properties, scope);
                }
                stream.Eat(TokenType.ClosedCurlyBracket);
                return new AnonimusTypeExpression(properties);
            }
        }
        private void Property(Dictionary<Token, IExpression> properties, Scope scope)
        {
            var id = stream.Eat(TokenType.Identifier);
            stream.Eat(TokenType.PropertyAssigment);
            IExpression value = Exp(scope);
            properties.Add(id, value);
        }
        #endregion
    }
}
