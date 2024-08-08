using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Expressions.TypeRestrictionExpression
{
    internal class TypeRestrictionExpression : IExpression
    {
        private readonly string typeRestriction;

        public TypeRestrictionExpression(string typeRestriction)
        {
            this.typeRestriction = typeRestriction;
        }
        public object Evaluate()
        {
            return new TypeRestriction(typeRestriction);
        }
    }
}
