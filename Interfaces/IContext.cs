using System.Collections.Generic;

namespace DSL.Interfaces
{
    /// <summary>
    /// Allows to know and change the state of the game.
    /// </summary>
    public interface IContext
    {
        int TriggerPlayer {  get; }
        IList<ICard> Board { get; }
        IList<ICard> FieldOfPlayer(int player);
        IList<ICard> GraveYardOfPlayer(int player); 
        IList<ICard> HandOfPlayer(int player);
        IList<ICard> DeckOfPlayer(int player);
    }
}
