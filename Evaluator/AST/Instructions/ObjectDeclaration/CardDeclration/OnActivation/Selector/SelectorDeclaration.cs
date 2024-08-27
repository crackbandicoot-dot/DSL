using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector
{
    internal class SelectorDeclaration : IInstruction
    {
        private readonly Token onActivationToken;
        private OnActivationObject result;
        private readonly LenguajeTypes.Selector? parent;
        private AnonimusObject onActivationObj;

        public SelectorDeclaration(Token onActivationToken,OnActivationObject result, LenguajeTypes.Selector? parent, AnonimusObject onActivationObj)
        {
            this.onActivationToken = onActivationToken;
            this.result = result;
            this.parent = parent;
            this.onActivationObj = onActivationObj;
        }

        public void Execute()
        {
            if (onActivationObj.TryGetValue("Selector", out object? value))
            {
                var selectorToken = onActivationObj.GetAssociatedToken("Selector");
                //Logica para llenar Selector
                result.Selector = new();
                var selector = result.Selector;
                SingleDeclaration singleDeclaration = new(selectorToken,selector, (AnonimusObject)value);
                SourceDeclaration sourceDeclaration = new(selectorToken,parent, selector, (AnonimusObject)value);
                PredicateDeclaration predicateDeclaration = new(selectorToken,selector, (AnonimusObject)value);
                singleDeclaration.Execute();
                sourceDeclaration.Execute();
                predicateDeclaration.Execute();
            }
            else
            {
                if (parent is not null)
                {
                    result.Selector = parent;
                }
            }
        }
    }
}