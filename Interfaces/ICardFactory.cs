
using System.Collections.Generic;
namespace DSL.Interfaces
{
    /// <summary>
    /// Allow the user
    /// of the DSL create the cards in its own terms.
    /// </summary>
    public interface ICardFactory
    {
        ICard CreateCard(string name,string faction,string type,
            IList<string> range,double power,IEffect effect);
    }
}