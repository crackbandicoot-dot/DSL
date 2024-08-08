
using DSL.Evaluator.LenguajeTypes;


namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration
{
    public class Context
    {
        public Dictionary<string, Card> cards;
        private Dictionary<string, Effect> effects;

        public Context()
        {
            cards = new Dictionary<string, Card>();
            effects = new Dictionary<string, Effect>();
        }

        internal bool ContainsEffect(string v)
        {
            return effects.ContainsKey(v);
        }

        internal void Declare(Card card)
        {
            cards.Add(card.Name, card);
        }
        internal void Declare(Effect effect)
        {
            effects.Add(effect.Name, effect);
        }
        internal Card GetCard(string cardName)
        {
            return cards[cardName];
        }
        internal Effect GetEffect(string effectName)
        {
            return effects[effectName];
        }
    }
}