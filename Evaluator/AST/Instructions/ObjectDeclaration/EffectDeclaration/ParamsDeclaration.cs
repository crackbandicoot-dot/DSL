using DSL.Evaluator.LenguajeTypes;
using DSL.Extensor_Methods;
using DSL.Lexer;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.EffectDeclaration
{
    internal class ParamsDeclaration : IInstruction
    {
        private readonly AnonimusObject effectProperties;
        private readonly Effect effect;

        public ParamsDeclaration(Token effectToken,AnonimusObject properties, Effect effect)
        {
            this.effectProperties = properties;
            this.effect = effect;
        }
        public void Execute()
        {
            if (effectProperties.TryGetValue("Params", out object? value))
            {
                var Params = (AnonimusObject)value;
                Params.ForEach(kvp => effect.Params.Add(kvp.Key, (TypeRestriction)kvp.Value));
            }
        }
    }
}
