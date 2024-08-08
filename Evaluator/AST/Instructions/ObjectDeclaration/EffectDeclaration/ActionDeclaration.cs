using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.EffectDeclaration
{
    internal class ActionDeclaration : IInstruction
    {
        private readonly Dictionary<string, object> properties;
        private readonly Effect effect;

        public ActionDeclaration(Dictionary<string, object> properties, Effect effect)
        {
            this.properties = properties;
            this.effect = effect;
        }

        public void Execute()
        {
            if (properties.TryGetValue("Action", out object? value))
            {
                var action = (LenguajeTypes.Action)value;
                effect.Action = action;
            }
            else
            {
                throw new Exception("Effect does not contain an action def");
            }
        }
    }
}
