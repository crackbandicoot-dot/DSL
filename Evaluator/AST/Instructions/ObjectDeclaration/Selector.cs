using DSL.Evaluator.AST.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration
{
    internal class Selector
    {
        public string Source { get; internal set; }
        public bool Single { get; internal set; }
        public IExpression Predicate { get; internal set; }
    }
}
