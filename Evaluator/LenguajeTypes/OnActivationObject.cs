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
        private List<Card> GetTargets(IContext gameContext)
        {
            var targetsSource = GetSource(gameContext);
            var filtred = new List<Card>();
            foreach (var target in targetsSource)
            {
                if ((bool)Selector.Predicate.Invoke(target))
                {
                    filtred.Add(target);
                }
            }
            if (Selector.Single)
            {
                return new List<Card>() { filtred[0] };
            }
            return filtred;
        }
        private List<Card> GetSource(IContext gameContext)
        {
            int player = gameContext.TriggerPlayer;
            int otherPlayer = (player + 1) % 2;
            Dictionary<string, List<Card>> sourceMap
            = new()
            {
                {"hand",gameContext.HandOfPlayer(player)},
                {"otherHand",gameContext.HandOfPlayer(otherPlayer)},
                {"deck",gameContext.DeckOfPLayer(player)},
                {"otherDeck",gameContext.DeckOfPLayer(otherPlayer)},
                {"field",gameContext.FieldOfPlayer(player)},
                {"otherField",gameContext.FieldOfPlayer(otherPlayer)},
                {"board",gameContext.Board}
            };
            return sourceMap[Selector.Source];
        }
    }
}
