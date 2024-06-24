// Ignore Spelling: DSL

using DSL.Evaluator.LenguajeTypes;
using System.Reflection;

namespace DSL.Evaluator.Expressions.DotChainExpressions
{
    internal class PropertyAccess : IExpression
    {
        private static readonly Dictionary<Type, PropertyInfo> propertyCache = new();
        private readonly IExpression left;
        private readonly string propertyName;
        public PropertyAccess(IExpression left, string propertyName)
        {
            this.left = left;
            this.propertyName = propertyName;
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
    }
}

