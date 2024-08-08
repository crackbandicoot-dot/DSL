using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.LenguajeTypes;
namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.EffectDeclaration
{
    internal class EffectDeclaration : IInstruction
    {
        private readonly Context context;
        private readonly IExpression effectBody;
        private static readonly HashSet<string> validProperties =
            new() { "Name", "Params", "Action" };
        public EffectDeclaration(Context context, IExpression effectBody)
        {
            this.context = context;
            this.effectBody = effectBody;
        }

        public void Execute()
        {
            Effect effect = new();
            var d = (Dictionary<string, object>)effectBody.Evaluate();
            //Chequear que no tenga una propiedad de mas
            foreach (var property in d.Keys)
            {
                if (!validProperties.Contains(property))
                {
                    throw new Exception($"Propertry{property} is not a valid effect" +
                        $"property");
                }
            }
            NameDeclaration nameDeclaration = new(d, effect);
            ActionDeclaration actionDeclaration = new(d, effect);
            ParamsDeclaration paramsDeclaration = new(d, effect);
            nameDeclaration.Execute();
            actionDeclaration.Execute();
            paramsDeclaration.Execute();
            context.Declare(effect);
        }
    }
}
