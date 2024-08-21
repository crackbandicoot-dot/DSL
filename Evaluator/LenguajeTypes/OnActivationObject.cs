using DSL.Interfaces;
using System;
using System.Collections.Generic;

namespace DSL.Evaluator.LenguajeTypes
{
    public class OnActivationObject
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        internal Effect Effect { get; set; }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public Selector Selector { get; set; }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        internal void Activate(IContext gameContext)
        {
            //Select the targets
            Effect.Action.Invoke(GetTargets(gameContext), gameContext);
        }
        private IList<ICard> GetTargets(IContext gameContext)
        {
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
                return new List<ICard>() { filtred[0] };
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
