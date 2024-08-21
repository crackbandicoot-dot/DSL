using DSL.Evaluator.LenguajeTypes;
using DSL.Extensor_Methods;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.EffectDeclaration
{
    internal class ParamsDeclaration : IInstruction
    {
        private readonly Dictionary<string, object> properties;
        private readonly Effect effect;

        public ParamsDeclaration(Dictionary<string, object> properties, Effect effect)
        {
            this.properties = properties;
            this.effect = effect;
        }
        public void Execute()
        {
            if (properties.TryGetValue("Params", out object? value))
            {
                var Params = (Dictionary<string, object>)value;
                Params.ForEach(kvp => effect.Params.Add(kvp.Key, (TypeRestriction)kvp.Value));
            }
        }
    }
}
