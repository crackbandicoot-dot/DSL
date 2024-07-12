using DSL.Evaluator.Expressions;
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions.NumberExpressions
{
    internal class AnonimusTypeExpression : IExpression
    {
        private readonly Dictionary<string, IExpression> properties;

        public AnonimusTypeExpression(Dictionary<string, IExpression> properties)
        {
            this.properties = properties;
        }

        public IDSLType Evaluate()
        {
            Dictionary<string, IDSLType> evaluedProperties = new();
            foreach (var kvp in properties)
            {
                evaluedProperties.Add(kvp.Key,kvp.Value.Evaluate());
            }
            return new AnonimusObject(evaluedProperties);
        }
    }
}