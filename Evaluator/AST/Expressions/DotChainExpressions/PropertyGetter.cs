// Ignore Spelling: DSL

using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.LenguajeTypes;
using System.Reflection;

namespace DSL.Evaluator.AST.Expressions.DotChainExpressions
{
    internal class PropertyGetter : IExpression
    {
        private static readonly Dictionary<Type, PropertyInfo> propertyCache = new();
        private readonly IExpression left;
        private readonly string propertyName;
        private readonly List<IExpression>? args;

        public PropertyGetter(IExpression left, string propertyName)
        {
            this.left = left;
            this.propertyName = propertyName;

        }
        public PropertyGetter(IExpression left, string propertyName, List<IExpression> args)
        {
            this.left = left;
            this.propertyName = propertyName;
            this.args = args;
        }
        public IDSLType Evaluate()
        {
            IDSLType l = left.Evaluate();
            Type expType = l.GetType();
            if (!propertyCache.TryGetValue(expType, out PropertyInfo? propertyInfo))
            {
                // Cache the property info
                propertyInfo = expType.GetProperty(propertyName);
                if (propertyInfo == null)
                {
                    throw new Exception($"Type {expType.Name} does not have a property named '{propertyName}'.");
                }
                propertyCache[expType] = propertyInfo;
            }
            if (args is null)
            {
                object? value = propertyInfo.GetValue(l);
                if (value is IDSLType dslTypeValue)
                {
                    return dslTypeValue;
                }
                else
                {
                    throw new InvalidCastException($"Property '{propertyName}' on type {expType.Name} is not of type IDSLType.");
                }
            }
            else
            {
                var array = args.Select(x => x.Evaluate()).ToArray(); //Array with a number 0
                object? value = propertyInfo.GetValue(l, array); // exception because un match number of params
                if (value is IDSLType dslTypeValue)
                {
                    return dslTypeValue;
                }
                else
                {
                    throw new InvalidCastException($"Property '{propertyName}' on type {expType.Name} is not of type IDSLType.");
                }
            }
        }
    }
}

