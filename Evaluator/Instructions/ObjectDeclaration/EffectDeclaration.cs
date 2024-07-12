using DSL.Evaluator.Expressions;
using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Instructions.ObjectDeclaration
{
    internal class EffectDeclaration : IInstruction
    {
        private readonly Context context;
        private readonly IExpression effectBody;

        public EffectDeclaration(Context context,IExpression effectBody)
        {
            this.context = context;
            this.effectBody= effectBody;
        }

        public void Execute()
        {
            var evaluatedExpression = effectBody.Evaluate();
            if (evaluatedExpression is AnonimusObject obj)
            {
                context.Declare(new Effect(obj));
            }
            else
            {
                throw new Exception("Invalid Effect Declaration");
            }
            
        }
    }
}
