using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector
{
    internal class PredicateDeclaration : IInstruction
    {
        private LenguajeTypes.Selector result;
        private object value;

        public PredicateDeclaration(LenguajeTypes.Selector result, object value)
        {
            this.result = result;
            this.value = value;
        }

        public void Execute()
        {

            result.Predicate = (LenguajeTypes.Delegate)((Dictionary<string, object>)value)["Predicate"];
        }
    }
}