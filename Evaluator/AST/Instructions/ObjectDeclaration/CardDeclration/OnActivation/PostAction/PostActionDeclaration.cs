using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Effect;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector;
using DSL.Evaluator.LenguajeTypes;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.PostAction
{
    internal class PostActionDeclaration : IInstruction
    {
        private readonly List<OnActivationObject> result;
        private readonly LenguajeTypes.Selector parent;
        private readonly Dictionary<string, object> onActivationObj;
        private readonly Context context;

        public PostActionDeclaration(List<OnActivationObject> result, LenguajeTypes.Selector parent,
            Dictionary<string, object> onActivationObj, Context context)
        {
            this.result = result;
            this.parent = parent;
            this.onActivationObj = onActivationObj;
            this.context = context;
        }

        public void Execute()
        {
            OnActivationObject postAction = new();
            SelectorDeclaration selectorDeclaration = new(postAction, parent, onActivationObj);
            EffectInstanciation ei = new(postAction, onActivationObj, context);
            selectorDeclaration.Execute();
            ei.Execute();
            result.Add(postAction);
            if (onActivationObj.TryGetValue("PostAction", out object? value))
            {
                var nestedProperties = (Dictionary<string, object>)value;
                var nestedPostAction = new PostActionDeclaration(result, parent, nestedProperties, context);
                nestedPostAction.Execute();
            }
        }
    }
}