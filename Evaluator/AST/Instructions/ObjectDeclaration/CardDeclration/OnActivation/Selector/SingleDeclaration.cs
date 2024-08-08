namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector
{
    internal class SingleDeclaration : IInstruction
    {
        private LenguajeTypes.Selector result;
        private Dictionary<string, object> properties;

        public SingleDeclaration(LenguajeTypes.Selector result, Dictionary<string, object> properties)
        {
            this.result = result;
            this.properties = properties;
        }

        public void Execute()
        {
            if (properties.TryGetValue("Single", out object? value))
            {
                result.Single = (bool)(value);
            }
        }
    }
}