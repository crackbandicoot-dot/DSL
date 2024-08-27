using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;
namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.EffectDeclaration
{
    internal class EffectDeclaration : IInstruction
    {
        private readonly Token effectToken;
        private readonly Context context;
        private readonly IExpression effectBody;
        private static readonly HashSet<string> validProperties =
            new() { "Name", "Params", "Action" };
        public EffectDeclaration(Token effectToken,Context context, IExpression effectBody)
        {
            this.effectToken = effectToken;
            this.context = context;
            this.effectBody = effectBody;
        }

        public void Execute()
        {
            Effect effect = new();
            var d = (AnonimusObject)effectBody.Evaluate();
            //Chequear que no tenga una propiedad de mas
            foreach (var property in d.Keys)
            {
                if (!validProperties.Contains(property))
                {
                    throw new Exception($"Propertry{property} is not a valid effect" +
                        $"property");
                }
            }
            NameDeclaration nameDeclaration = new(effectToken,d, effect);
            ActionDeclaration actionDeclaration = new(effectToken,d, effect);
            ParamsDeclaration paramsDeclaration = new(effectToken,d, effect);
            nameDeclaration.Execute();
            actionDeclaration.Execute();
            paramsDeclaration.Execute();
            context.Declare(effect);
        }
    }
}
