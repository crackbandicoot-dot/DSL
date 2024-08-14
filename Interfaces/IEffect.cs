using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Interfaces
{
    public interface IEffect
    {
        void Activate(IContext context);
    }
}
