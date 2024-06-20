using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Instructions.Statements.SimpleStatements
{
    internal class FunctionCall : IExpression, IInstruction
    {
        private readonly IExpression leftExpression;
        private readonly string functionName;
        private readonly List<IExpression> args;
        private bool IsVoid => leftExpression != null;

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
                    return left;
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
