using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.LenguajeTypes
{
    internal readonly struct None : IDSLType
    {
        public bool Equals(IDSLType? other) => other is None;
        public override string ToString() => "None";
    }
}
