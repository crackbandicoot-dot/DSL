using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Expressions.BooleanExpressions;
using DSL.Evaluator.Expressions;
using DSL.Lexer;


namespace DSL.Evaluator.Instructions
{

    internal static class Factory<T>// Assuming T is a non-nullable value type
    {
        public static UnaryExpression<T> InstaciateUnaryOperator(TokenType unaryOperatorName, Expression<T> expression)
        {
            // Example for bool type, you can add more cases for other types
            if (typeof(T) == typeof(bool))
            {
                switch (unaryOperatorName)
                {
                    case TokenType.Not:
                        return new Not((Expression<bool>)(object)expression) as UnaryExpression<T>;
                    default:
                        throw new NotImplementedException($"Unary operator {unaryOperatorName} is not implemented for type {typeof(T)}.");
                }
            }
            else
            {
                throw new NotImplementedException($"Type {typeof(T)} is not supported.");
            }
        }

        public static BinaryExpression<T> InstaciateBinaryOperator(TokenType binaryOperatorName, Expression<T> left, Expression<T> right)
        {
            if (typeof(T) == typeof(bool))
            {
                switch (binaryOperatorName)
                {
                    case TokenType.And:
                        return new AndOperation((Expression<bool>)(object)left, (Expression<bool>)(object)right) as BinaryExpression<T>;
                    case TokenType.Or:
                        return new OrOperation((Expression<bool>)(object)left, (Expression<bool>)(object)right) as BinaryExpression<T>;
                    default:
                        throw new NotImplementedException($"Unary operator {binaryOperatorName} is not implemented for type {typeof(T)}.");
                }
            }
            else
            {
                throw new NotImplementedException($"Type {typeof(T)} is not supported.");
            }
            // Implement your binary operator instantiation logic here
            throw new NotImplementedException();
        }
    }
}

