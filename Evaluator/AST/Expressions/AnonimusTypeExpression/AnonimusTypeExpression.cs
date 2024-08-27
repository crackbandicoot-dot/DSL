using DSL.Evaluator.AST.Instructions.ObjectDeclaration;
using DSL.Extensor_Methods;
using DSL.Lexer;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Expressions.AnonimusTypeExpression
{
    internal class AnonimusTypeExpression : IExpression
    {
        private readonly Dictionary<Token, IExpression> properties;

        public AnonimusTypeExpression(Dictionary<Token, IExpression> properties)
        {
            this.properties = properties;
        }
        public object Evaluate()
        {
            Dictionary<Token, object> evaluedProperties = new();
            properties.ForEach(kvp => evaluedProperties.Add(kvp.Key, kvp.Value.Evaluate()));
            return new AnonimusObject(evaluedProperties);
        }
    }
}