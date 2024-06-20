﻿using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions.Statements.SimpleStatements;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Instructions.Statements.LoopStatements
{
    internal class ForStatement : IInstruction
    {
        private readonly string variableIdentifier;
        private readonly IExpression list;
        private readonly InstructionBlock instructions;
        private readonly Scope<IDSLType> scope;

        public ForStatement(string variableIdentifier,IExpression list,InstructionBlock instructions,Scope<IDSLType> scope)
        {
            this.variableIdentifier = variableIdentifier;
            this.list = list;
            this.instructions = instructions;
            this.scope = scope;
        }
        public void Execute()
        {
            IDSLType exp = list.Evaluate();
            if (exp is List l)
            {
                Number length = l.Count;
                for (int i = 0; i < length; i++)
                {
                    instructions.ScopeVariables.Declare(variableIdentifier, l[i]);
                    instructions.Execute();
                }
            }
            else
            {
                throw new Exception($"You cannot iterate over a {exp.GetType()}");
            }
        }
    }
}