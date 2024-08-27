using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.EffectDeclaration
{
    internal class NameDeclaration : IInstruction
    {
        private readonly Token effectToken;
        private readonly AnonimusObject properties;
        private readonly Effect effect;

        public NameDeclaration(Token effectToken,AnonimusObject properties, Effect effect)
        {
            this.effectToken = effectToken;
            this.properties = properties;
            this.effect = effect;
        }

        public void Execute()
        {
            if (properties.TryGetValue("Name", out object? value))
            {
                effect.Name = (string)value;
            }
            else
            {
                throw new Exception($"Effect has not name in {effectToken.Pos}");
            }
        }
    }
}
