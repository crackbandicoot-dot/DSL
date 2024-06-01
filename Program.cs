using DSL;
using DSL.Instructions;
using DSL.Instructions.Expressions;
using DSL.Instructions.Expressions.BooleanExpressions;
using DSL.Instructions.Expressions.BooleanExpressions.Comparators;

internal class Program
{
    private static void Main(string[] args)
    {
        //Casos Bases
        SimpleExpression<bool> True = new SimpleExpression<bool>(true);
        SimpleExpression<bool> False=  new SimpleExpression<bool> (false);
        //Casos Recursivos
        AndOperation and1 = new AndOperation(True,False);
        OrOperation or1 = new OrOperation(and1,False);
        Not not1 = new Not(or1);
        Console.WriteLine(not1.Evaluate());
        // Esto es evaluar la expresion

        // !((true && false) || false)




        /* Console.WriteLine('"'=='"');
         string path = @"C:\source\repos\CP\DSL\Test.txt";
         StreamReader stream = new StreamReader(path);
         string input = stream.ReadToEnd();
         Lexer lexer = new Lexer(input);
         lexer.NextToken();
         Parser parser = new Parser(lexer);
         parser.NextInstruction();
         Console.WriteLine(((ValuedInstruction<bool>)parser.CurrentInstruction).Execute());
       */
    }
      

}