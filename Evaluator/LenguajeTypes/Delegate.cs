﻿using DSL.Evaluator.AST.Expressions;

namespace DSL.Evaluator.LenguajeTypes
{
    public class Delegate
    {
        private readonly IExpression expression;
        private readonly Scope.Scope scope;

        internal Delegate(string[] identifiers, IExpression expression, Scope.Scope scope)
        {
            Identifiers = identifiers;
            this.expression = expression;
            this.scope = scope;
        }

        public object Invoke(params object[] parameters)
        {
            if (Identifiers.Length == parameters.Length)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    scope.Declare(Identifiers[i], parameters[i]);
                }
                return expression.Evaluate();
            }
            else
            {
                throw new Exception("Amount of parameters doesn't match");
            }
        }
        public string[] Identifiers { get; }
    }
}
