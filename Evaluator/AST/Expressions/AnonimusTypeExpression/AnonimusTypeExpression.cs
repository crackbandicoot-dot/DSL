using DSL.Extensor_Methods;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Expressions.AnonimusTypeExpression
{
    internal class AnonimusTypeExpression : IExpression
    {
        private readonly Dictionary<string, IExpression> properties;

        public AnonimusTypeExpression(Dictionary<string, IExpression> properties)
        {
            this.properties = properties;
        }
        public object Evaluate()
        {
            Dictionary<string, object> evaluedProperties = new();
            properties.ForEach(kvp => evaluedProperties.Add(kvp.Key, kvp.Value.Evaluate()));
            return evaluedProperties;
        }
    }
}