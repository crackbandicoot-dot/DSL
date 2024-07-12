// Ignore Spelling: lexer DSL

namespace DSL.Evaluator.LenguajeTypes
{
    public interface IContext : IDSLType
    {

        public Number TriggerPlayer { get; set; }
        public List Board { get; set; }
        public List HandOfPlayer(Number player);
        public List FieldOfPlayer(Number player);
        public List GraveYardOfPlayer(Number player);
        public List DeckOfPlayer(Number player);
        bool IEquatable<IDSLType>.Equals(IDSLType? other)
        {
            throw new NotImplementedException();
        }

        string IDSLType.ToString()
        {
            throw new NotImplementedException();
        }
    }
}