using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.EffectDeclaration
{
    internal class ActionDeclaration : IInstruction
    {
        private readonly Token effectToken;
        private readonly AnonimusObject effectProperties;
        private readonly Effect effect;

        public ActionDeclaration(Token effectToken,AnonimusObject properties, Effect effect)
        {
            this.effectToken = effectToken;
            this.effectProperties = properties;
            this.effect = effect;
        }

        public void Execute()
        {
            if (effectProperties.TryGetValue("Action", out object? value))
            {
                var action = (LenguajeTypes.Action)value;
                effect.Action = action;
            }
            else
            {
                throw new Exception($"Effect does not contain an action def in {effectToken.Pos}");
            }
        }
    }
}
