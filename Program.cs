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
        //int[] a = new[] { 1, 2 };
        
        Number n2 = 2;
        IDSLType n3 = n2;
        Console.WriteLine((Number)n3/n2);

       /* // Console.WriteLine('"'=='"');
        string path = @"C:\source\repos\CP\DSL\Test.txt";
         StreamReader stream = new StreamReader(path);
         string input = stream.ReadToEnd();
         Lexer lex = new Lexer(input);
         lex.NextToken();
         Parser parser = new Parser(lex);
         parser.NextInstruction();
         while (parser.CurrentInstruction is not EndInstruction)
         {
            parser.CurrentInstruction.Execute();
            parser.NextInstruction();
         }    
       */
    }
}