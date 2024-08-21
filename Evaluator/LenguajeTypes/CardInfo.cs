// Ignore Spelling: lexer DSL
using DSL.Interfaces;
using System.Collections.Generic;

namespace DSL.Evaluator.LenguajeTypes
{
    public class CardInfo
    {
        public string Name { get; set; }
        public string Faction { get; set; }
        public string Type { get; set; }
        public List<string> Range { get; set; }
        public double Power { get; set; }
        public List<OnActivationObject> OnActivation { get; set; }
        public void ActivateEffect(IContext gameContext)
        {
            OnActivation.ForEach(onActivation => onActivation.Activate(gameContext));
        }
    }
}