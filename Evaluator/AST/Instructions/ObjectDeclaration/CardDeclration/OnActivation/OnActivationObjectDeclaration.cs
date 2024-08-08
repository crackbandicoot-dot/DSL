using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Effect;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.PostAction;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector;
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation
{
    internal class OnActivationObjectDeclaration : IInstruction
    {
        private readonly Dictionary<string, object> onActivationObj;
        private readonly List<OnActivationObject> result;
        private readonly Context context;

        public OnActivationObjectDeclaration(Dictionary<string, object> onActivationObj, List<OnActivationObject> result,
                                               Context context)
        {
            this.onActivationObj = onActivationObj;
            this.result = result;
            this.context = context;
        }
        public void Execute()
        {
            if (onActivationObj.Count > 3)
            {
                throw new Exception("Invalid activation object");
            }
            OnActivationObject obj = new();
            EffectInstanciation eIns = new(obj, onActivationObj, context);
            SelectorDeclaration sDec = new(obj, null, onActivationObj);
            eIns.Execute();
            sDec.Execute();
            PostActionDeclaration pActDec = new(result, obj.Selector, onActivationObj, context);
            pActDec.Execute();
        }
    }
}