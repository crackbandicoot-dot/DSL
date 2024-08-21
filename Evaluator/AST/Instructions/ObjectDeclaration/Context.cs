
using DSL.Evaluator.LenguajeTypes;
using System.Collections.Generic;


namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration
{
    public class Context
    {
        public Dictionary<string, CardInfo> cards;
        private Dictionary<string, Effect> effects;

        public Context()
        {
            cards = new Dictionary<string, CardInfo>();
            effects = new Dictionary<string, Effect>();
        }

        internal bool ContainsEffect(string v)
        {
            return effects.ContainsKey(v);
        }

        internal void Declare(CardInfo card)
        {
            cards.Add(card.Name, card);
        }
        internal void Declare(Effect effect)
        {
            effects.Add(effect.Name, effect);
        }
        internal CardInfo GetCard(string cardName)
        {
            return cards[cardName];
        }
        internal Effect GetEffect(string effectName)
        {
            return effects[effectName];
        }
    }
}