using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Effect
{
     class EffectInstantiation : IInstruction
    {
        private readonly Token onActivationToken;
        private readonly OnActivationObject onActivationObject;
        private readonly AnonimusObject properties;
        private readonly Context context;

        public EffectInstantiation(Token onActivationToken,OnActivationObject onActivationObject,
            AnonimusObject properties, Context context)
        {
            this.onActivationToken = onActivationToken;
            this.onActivationObject = onActivationObject;
            this.properties = properties;
            this.context = context;
        }

        public void Execute()
        {

            if (properties.TryGetValue("Effect", out object? eff))
            {
                var effectsProperties = (AnonimusObject)eff;
                var effectToken = properties.GetAssociatedToken("Effect");
                //Verificar el nombre del effecto
                if (effectsProperties.TryGetValue("Name", out object? name))
                {
                    var nameToken = effectsProperties.GetAssociatedToken("Name");
                    if (context.ContainsEffect((string)name))
                    {
                        onActivationObject.Effect = context.GetEffect((string)name);
                        //Verificar parametros
                        foreach (var param in onActivationObject.Effect.Params)
                        {
                            param.Value.Check(effectsProperties[param.Key]);
                            onActivationObject.Params.Add(param.Key, effectsProperties[param.Key]);
                        }
                        if (onActivationObject.Effect.Params.Count != effectsProperties.Count - 1)
                        {
                            throw new Exception($"Unmatched parameter in {effectToken.Pos}");
                        }
                    }
                    else
                    {
                        throw new Exception($"Context does not contains a effect with name {(string)name}");
                    }
                }

                else
                {
                    throw new Exception($"Effect instanciation requires a name property in {effectToken.Pos}");
                }
            }
            else
            {
                throw new Exception($"OnActivation object requires a Effect property  in {onActivationToken.Pos}");
            }
        }
    }
}
