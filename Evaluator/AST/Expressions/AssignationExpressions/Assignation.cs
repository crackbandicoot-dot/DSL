using DSL.Evaluator.AST.Expressions.DotChainExpressions;
using DSL.Evaluator.AST.Instructions;
using System;

namespace DSL.Evaluator.AST.Expressions.AssignationExpressions
{
    internal class Assignation : IExpression, IInstruction
    {
        private readonly IExpression leftExp;
        private readonly IExpression rightExp;
        public Assignation(IExpression leftExp, IExpression rightExp)
        {
            this.leftExp = leftExp;
            this.rightExp = rightExp;
        }

        public object Evaluate()
        {
            if (leftExp is Variable variable)
            {
                variable.scope.Declare(variable.identifierToken.Value, rightExp.Evaluate());
                return variable.Evaluate();
            }
            else if (leftExp is PropertyGetter propertyGetter)
            {
                PropertySetter ps = new(propertyGetter.left, propertyGetter.propertyName, rightExp, propertyGetter.args);
                ps.Execute();
                return rightExp.Evaluate();
            }
            else
            {
                throw new Exception();
            }
        }

        public void Execute()
        {
            Evaluate();
        }
    }
}
