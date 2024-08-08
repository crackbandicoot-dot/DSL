namespace DSL.Evaluator.LenguajeTypes
{
    public interface IContext
    {
        int TriggerPlayer { get; }
        List<Card> Board { get; }
        List<Card> HandOfPlayer(int player);
        List<Card> GraveYardOfPlayer(int player);
        List<Card> DeckOfPLayer(int player);
        List<Card> FieldOfPlayer(int player);
    }
}
