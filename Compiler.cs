using DSL.Evaluator.LenguajeTypes;
using DSL.Interfaces;
using DSL.Lexer;

namespace DSL
{
    
    /// <summary>
    /// Class to compile G++ programs.
    /// </summary>
    public static class Compiler
    {
        /// <summary>
        /// Compiles a string representation of a G++ program
        /// into a collection of cards by using a Card Factory 
        /// provided by the user.
        /// </summary>
        /// <param name="programString">string representation of the DSL program</param>
        /// <param name="cardFactory">card factory that will be used to instanciate the cards</param>
        /// <returns></returns>
        public static IEnumerable<ICard> Compile(string programString,ICardFactory cardFactory)
        {
            
            Parser.ProgramParser parser = new(new LexerStream(programString));
            var program = parser.GwentProgram();
            program.Execute();
            var c = program.Context;
            return c.cards.Values.
             Select(card => cardFactory.CreateCard(card.Name, card.Faction, card.Type, card.Range, card.Power, new DynamicEffect(card.OnActivation)));
        }
    }
}