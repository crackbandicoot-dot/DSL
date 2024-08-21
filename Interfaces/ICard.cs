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
        /// <summary>
        /// Any string containing the name of the card.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The faction must be either "Goods","Bads" or "Neutral" in order to the compiler
        /// work correctly.
        /// </summary>
        string Faction { get; }
        /// <summary>
        /// The type must be  "Gold",
        ///    "Silver","Decoy","Leader","Weather","Clearing" or "Boost" in order 
        ///    to the compiler work correctly.
        /// </summary>
        string Type { get; }
        /// <summary>
        /// The range must be "Melee", "Ranged", "Siesge" to the compiler work
        /// correctly.
        /// </summary>
        IList<string> Range { get; }
        /// <summary>
        /// The power of the card, it should be a non negative number
        /// </summary>
        double Power { get; set; }
        IEffect Effect { get;}
    }
}
