using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Effect
{
    public class EffectInstanciation : IInstruction
    {
        private readonly OnActivationObject onActivationObject;
        private readonly Dictionary<string, object> properties;
        private readonly Context context;

        public EffectInstanciation(OnActivationObject onActivationObject,
            Dictionary<string, object> properties, Context context)
        {
            this.onActivationObject = onActivationObject;
            this.properties = properties;
            this.context = context;
        }

        public void Execute()
        {

            if (properties.TryGetValue("Effect", out object? eff))
            {
                var effectsProperties = (Dictionary<string, object>)eff;
                //Verificar el nombre del effecto
                if (effectsProperties.TryGetValue("Name", out object? name))
                {
                    if (context.ContainsEffect((string)name))
                    {
                        onActivationObject.Effect = context.GetEffect((string)name);
                        //Verificar parametros
                        foreach (var param in onActivationObject.Effect.Params)
                        {
                            param.Value.Check(effectsProperties[param.Key]);
                            onActivationObject.Effect.Action.instructionBlock.ScopeVariables.Declare(param.Key, effectsProperties[param.Key]);
                        }
                        if (onActivationObject.Effect.Params.Count != effectsProperties.Count - 1)
                        {
                            throw new Exception("Unmatched parameter");
                        }
                    }
                    else
                    {
                        throw new Exception($"Context does not contains a effect with name {(string)name}");
                    }
                }

                else
                {
                    throw new Exception("Effect instanciation requires a name property");
                }
            }
            else
            {
                throw new Exception("OnActivation object requires a Effect property");
            }
        }
    }
}
