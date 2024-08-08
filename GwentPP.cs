using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;

namespace DSL
{
    public class GwentPP
    {
        public static void Main(string[] args)
        {


        }
        public static IEnumerable<Card> GetCards()
        {
            string path = @"C:\source\repos\CP\DSL\Test.txt";
            StreamReader stream = new(path);
            string input = stream.ReadToEnd();
            DSL.Parser.Parser parser = new(new LexerStream(input));
            var program = parser.GwentProgram();
            program.Execute();
            var c = program.Context;
            return c.cards.Values;
        }
    }
}