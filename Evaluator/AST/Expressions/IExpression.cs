using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Expressions
{
    internal interface IExpression : IASTNode
    {
        public IDSLType Evaluate();
    }

}
