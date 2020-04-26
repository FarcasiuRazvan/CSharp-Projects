using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToyLanguageInterpretor
{
    class PrgState
    {
        private MyIStack<IStmt> exeStack;
        private MyIDictionary<String, int> symTable;
        private MyIList<int> output;
        private IStmt originalProgram;
        private MyIFileTable<int,KeyValuePair<String, TextReader>> FileTable;

        public PrgState(MyIStack<IStmt> stk, MyIDictionary<String, int> symtbl, MyIList<int> ot,MyIFileTable<int, KeyValuePair<String, TextReader>> fileTable, IStmt prg)
        {
            this.exeStack = stk;
            this.symTable = symtbl;
            this.output= ot;
            this.FileTable = fileTable;
            this.exeStack.push(prg);
        }

        public MyIStack<IStmt> getStk()
        {
            return this.exeStack;
        }

        public MyIDictionary<String, int> getSymTable()
        {
            return this.symTable;
        }

        public MyIFileTable<int,KeyValuePair<String, TextReader>> getFileTable()
        {
            return this.FileTable;
        }

        public MyIList<int> getOutput() { return this.output; }
        public override string ToString()
        {
            return "Execution Stack: " + this.exeStack.ToString() + " \n " + "Symbol Table: " + this.symTable.ToString() + " \n " + "Output: " + this.output.ToString()+ " \n " + " FileTable: "+this.FileTable.ToString();
        }
    }
}
