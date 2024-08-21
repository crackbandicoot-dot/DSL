using DSL.Evaluator.AST.Expressions.DotChainExpressions;
using DSL.Evaluator.AST.Instructions;
using System;

namespace DSL.Evaluator.AST.Expressions.NumberExpressions
{
    internal class DecrementOperator : IExpression, IInstruction
    {
        private readonly IExpression exp;
        public DecrementOperator(IExpression exp)
        {
            this.exp = exp;
        }
        public object Evaluate()
        {
            if (exp is Variable variable)
            {
                var value = (double)variable.Evaluate() - 1;
                variable.scope.Declare(variable.identifierToken.Value, value);
                return value;
            }
            else if (exp is PropertyGetter propertyGetter)
            {

                var e = new PropertySetter(propertyGetter.left, propertyGetter.propertyName, new PlusOperation(propertyGetter, new SimpleExpression(1)), propertyGetter.args);
                e.Execute();
                return propertyGetter.Evaluate();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void Execute()
        {
            Evaluate();
        }


    }
}
