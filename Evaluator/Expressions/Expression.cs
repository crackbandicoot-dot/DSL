using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Expressions
{
    internal abstract class Expression<TReturn>
    {
        public abstract TReturn Evaluate();
    }

}
