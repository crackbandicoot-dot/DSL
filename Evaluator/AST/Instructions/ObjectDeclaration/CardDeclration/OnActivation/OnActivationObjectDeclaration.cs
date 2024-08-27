using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Effect;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.PostAction;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector;
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation
{
    internal class OnActivationObjectDeclaration : IInstruction
    {
        private readonly Token onActivationToken;
        private readonly AnonimusObject onActivationObj;
        private readonly List<OnActivationObject> result;
        private readonly Context context;

        public OnActivationObjectDeclaration(Token onActivationToken,AnonimusObject onActivationObj, List<OnActivationObject> result,
                                               Context context)
        {
            this.onActivationToken = onActivationToken;
            this.onActivationObj = onActivationObj;
            this.result = result;
            this.context = context;
        }
        public void Execute()
        {
            if (onActivationObj.Count > 3)
            {
                throw new Exception($"Invalid element in the on ativation array {onActivationToken.Pos}");
            }
            OnActivationObject obj = new();
            EffectInstantiation eIns = new(onActivationToken,obj, onActivationObj, context);
            SelectorDeclaration sDec = new(onActivationToken,obj, null, onActivationObj);
            eIns.Execute();
            sDec.Execute();
            PostActionDeclaration pActDec = new(onActivationToken,result, obj.Selector, onActivationObj, context);
            pActDec.Execute();
        }
    }
}