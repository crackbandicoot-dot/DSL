using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector
{
    internal class SelectorDeclaration : IInstruction
    {
        private OnActivationObject result;
        private readonly LenguajeTypes.Selector? parent;
        private Dictionary<string, object> onActivationObj;

        public SelectorDeclaration(OnActivationObject result, LenguajeTypes.Selector? parent, Dictionary<string, object> onActivationObj)
        {
            this.result = result;
            this.parent = parent;
            this.onActivationObj = onActivationObj;
        }

        public void Execute()
        {
            if (onActivationObj.TryGetValue("Selector", out object? value))
            {
                //Logica para llenar Selector
                result.Selector = new();
                var selector = result.Selector;
                SingleDeclaration singleDeclaration = new(selector, (Dictionary<string, object>)value);
                SourceDeclaration sourceDeclaration = new(parent, selector, (Dictionary<string, object>)value);
                PredicateDeclaration predicateDeclaration = new(selector, value);
                singleDeclaration.Execute();
                sourceDeclaration.Execute();
                predicateDeclaration.Execute();
            }
            else
            {
                if (parent is null)
                {
                    throw new Exception("not selector to use");
                }
                else
                {
                    result.Selector = parent;
                }
            }
        }
    }
}