// Ignore Spelling: DSL

using DSL.Evaluator.Instructions;
using DSL.Evaluator.LenguajeTypes;
using System.Reflection;

namespace DSL.Evaluator.Expressions.DotChainExpressions
{
    internal class FunctionCall : IExpression, IInstruction
    {
        private readonly IExpression leftExpression;
        private readonly string functionName;
        private readonly List<IExpression> args;
        public FunctionCall(IExpression leftExpression, string functionName, List<IExpression> args)
        {
            this.leftExpression = leftExpression;
            this.functionName = functionName;
            this.args = args;
        }
        public IDSLType Evaluate()
        {
            IDSLType left = leftExpression.Evaluate();
            Type type = left.GetType();
            MethodInfo? methodInfo = type.GetMethod(functionName);
            if (methodInfo == null)
            {
                throw new ArgumentException($"Type {type} doesn't have a method {functionName}");
            }
            else
            {
                object? result = methodInfo.Invoke(left, args.Select(x => x.Evaluate()).ToArray());
                if (result == null)
                {
                    return new None();
                }
                return (IDSLType)result;
            }

        }
        public void Execute()
        {
            Evaluate();
        }
    }
}
