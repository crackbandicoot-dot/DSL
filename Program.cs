using DSL.Lexer;
using DSL.Parser;
internal class Program
{
    private static void Main(string[] args)
    {

        
        List<int> a = new List<int>() {1,2,3};
        string path = @"C:\source\repos\CP\DSL\Test.txt";
        StreamReader stream = new StreamReader(path);
        string input = stream.ReadToEnd();
        Parser parser = new Parser(new LexerStream(input));
        parser.NextInstruction();
        parser.CurrentInstruction!.Execute();

        /*while (parser.CurrentInstruction is not EndInstruction)
         {
            parser.CurrentInstruction.Execute();
            parser.NextInstruction();
         }  */
    }
}