using DSL.Evaluator.Instructions.Statements;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using DSL.Parser;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;
internal class Program
{
    private static void Main(string[] args)
    {
         string path = @"C:\source\repos\CP\DSL\Test.txt";
         StreamReader stream = new StreamReader(path);
         string input = stream.ReadToEnd();
         Parser parser = new Parser(new LexerStream(input));
         parser.NextInstruction();
         parser.CurrentInstruction.Execute();
         Console.WriteLine("a");
        /*while (parser.CurrentInstruction is not EndInstruction)
         {
            parser.CurrentInstruction.Execute();
            parser.NextInstruction();
         }  */  
       
    }
}