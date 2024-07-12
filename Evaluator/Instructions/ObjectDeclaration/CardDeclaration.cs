using DSL.Evaluator.Expressions;
using DSL.Evaluator.Expressions.NumberExpressions;
using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Instructions.ObjectDeclaration
{
    internal class CardDeclaration : IInstruction
    {
        private readonly Context context;
        private readonly IExpression cardBody;

        public CardDeclaration(Context context,IExpression cardBody) 
        {
            this.context = context;
            this.cardBody = cardBody;
        }
        public void Execute()
        {
            var evaluatedExpression = cardBody.Evaluate();
            if (evaluatedExpression is AnonimusObject obj)
            {
                context.Declare(new Card(obj));
            }
            else
            {
                throw new Exception("Invalid Card Declaration");
            }
        }
    }
}

