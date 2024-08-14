using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Interfaces
{
    /// <summary>
    /// Represents the abstraction of a
    /// card that a game will need to implement in order to
    /// use the compiler.
    /// </summary>
    public interface ICard
    {
        string Name { get; }
        string Faction { get; }
        string Type { get; }
        IList<string> Range { get; }
        double Power { get; set; }
        IEffect Effect { get;}
    }
}
