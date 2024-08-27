using DSL.Evaluator.AST.Instructions.Statements.SimpleStatements;
using DSL.Evaluator.LenguajeTypes;
using DSL.Interfaces;
using DSL.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="cardFactory">card factory that will be used to instantiate the cards</param>
        ///<param name="printFunction">function the compiler will use in order to execute the print statement</param>
        /// <returns></returns>
        public static IEnumerable<ICard> Compile(string programString,ICardFactory cardFactory,Action<string> printFunction)
        {
            Parser.ProgramParser parser = new(new LexerStream(programString));
            PrintStatement.printerFunction = printFunction;
            var program = parser.GwentProgram();
            program.Execute();
            var c = program.Context;
            return c.cards.Values.
             Select(card => cardFactory.CreateCard(card.Name, card.Faction, card.Type, card.Range, card.Power, new DynamicEffect(card.OnActivation)));
        }
    }
}