using DSL.Evaluator.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Instructions.ObjectDeclaration
{
    internal class Selector
    {
        public string Source { get; internal set; }
        public bool Single {  get; internal set; }
        public IExpression Predicate { get; internal set; }
    }
}
