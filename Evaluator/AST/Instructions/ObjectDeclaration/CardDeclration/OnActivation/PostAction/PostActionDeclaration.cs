using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Effect;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector;
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.PostAction
{
    internal class PostActionDeclaration : IInstruction
    {
        private readonly Token onActivationToken;
        private readonly List<OnActivationObject> result;
        private readonly LenguajeTypes.Selector parent;
        private readonly AnonimusObject onActivationObj;
        private readonly Context context;

        public PostActionDeclaration(Token onActivationToken,List<OnActivationObject> result, LenguajeTypes.Selector parent,
           AnonimusObject onActivationObj, Context context)
        {
            this.onActivationToken = onActivationToken;
            this.result = result;
            this.parent = parent;
            this.onActivationObj = onActivationObj;
            this.context = context;
        }

        public void Execute()
        {
            OnActivationObject postAction = new();
            SelectorDeclaration selectorDeclaration = new(onActivationToken,postAction, parent, onActivationObj);
            EffectInstantiation ei = new(onActivationToken,postAction, onActivationObj, context);
            selectorDeclaration.Execute();
            ei.Execute();
            result.Add(postAction);
            if (onActivationObj.TryGetValue("PostAction", out object? value))
            {
                var nestedProperties = (AnonimusObject)value;
                var nestedPostAction = new PostActionDeclaration(onActivationToken,result, parent, nestedProperties, context);
                nestedPostAction.Execute();
            }
        }
    }
}