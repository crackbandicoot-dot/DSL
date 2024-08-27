using DSL.Interfaces;
using System;
using System.Collections.Generic;

namespace DSL.Evaluator.LenguajeTypes
{
    public class OnActivationObject
    {
        public OnActivationObject() {
            Params = new();
        }
        internal Dictionary<string,object> Params { get; set; }
        internal Effect Effect { get; set; }
        public Selector Selector { get; set; }

        internal void Activate(IContext gameContext)
        {
            foreach (var param in Params)
            {
                Effect.Action.instructionBlock.ScopeVariables.Declare(param.Key, param.Value);
            }
            Effect.Action.Invoke(GetTargets(gameContext), gameContext);
        }
        private IList<ICard> GetTargets(IContext gameContext)
        {
            if(Selector is null) return new List<ICard>();
            var targetsSource = GetSource(gameContext);
            var filtred = new List<ICard>();
            foreach (var target in targetsSource)
            {
                if ((bool)Selector.Predicate.Invoke(target))
                {
                    filtred.Add(target);
                }
            }
            if (Selector.Single)
            {
                return filtred.Count > 0 ? new List<ICard>() { filtred[0]} : new();
            }
            return filtred;
        }
        private IList<ICard> GetSource(IContext gameContext)
        {
            int player = gameContext.TriggerPlayer;
            int otherPlayer = (player + 1) % 2;
            return Selector.Source switch
            {
                "hand" => gameContext.HandOfPlayer(player),
                "otherHand" => gameContext.HandOfPlayer(otherPlayer),
                "deck" => gameContext.DeckOfPlayer(player),
                "otherDeck" => gameContext.DeckOfPlayer(otherPlayer),
                "field" => gameContext.FieldOfPlayer(player),
                "otherField" => gameContext.FieldOfPlayer(otherPlayer),
                "board" => gameContext.Board,
                _ => throw new Exception()
            };
        }
    }
}
