using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.EffectDeclaration
{
    internal class NameDeclaration : IInstruction
    {
        private readonly Dictionary<string, object> properties;
        private readonly Effect effect;

        public NameDeclaration(Dictionary<string, object> properties, Effect effect)
        {
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
                throw new Exception("Effect has not name");
            }
        }
    }
}
