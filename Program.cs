using DSL.Evaluator.Instructions.ObjectDeclaration;
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using DSL.Parser;

internal class Program
{
    private static void Main(string[] args)
    {
        
        
    //    string path = @"C:\source\repos\CP\DSL\Test.txt";
    //    StreamReader stream = new StreamReader(path);
    //    string input = stream.ReadToEnd();
    //    Parser parser = new Parser(new LexerStream(input));
    //    foreach (var instruction in parser.ParseAST())
    //    {
    //        instruction.Execute();
    //    }
    //    var a = parser.Context.Acced((DSL.Evaluator.LenguajeTypes.String)("\"Print13\""));
    //    if (a is Effect e)
    //    {
    //        e.Action.Invoke((DSL.Evaluator.LenguajeTypes.String)("Lol"));
    //        e.Action.Invoke((DSL.Evaluator.LenguajeTypes.String)("XD"));
    //    }
    //    /*while (parser.CurrentInstruction is not EndInstruction)
    //     {
    //        parser.CurrentInstruction.Execute();
    //        parser.NextInstruction();
    //     }  */
    }
    public class Context : IContext
    {
        public Number TriggerPlayer { get => 0; set => throw new NotImplementedException(); }
        public List Board { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List DeckOfPlayer(Number player)
        {
            throw new NotImplementedException();
        }

        public List FieldOfPlayer(Number player)
        {
            throw new NotImplementedException();
        }

        public List GraveYardOfPlayer(Number player)
        {
            throw new NotImplementedException();
        }

        public List HandOfPlayer(Number player)
        {
            throw new NotImplementedException();
        }
    }

}