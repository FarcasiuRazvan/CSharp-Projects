using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class PrintStmt : IStmt
    {
        Exp exp;
        public PrintStmt(Exp ex)
        {
            this.exp = ex;
        }
        public override String ToString()
        {
            return "print(" + this.exp.ToString() + ")";
        }
        public PrgState execute(PrgState state)
        {
            int tmp = this.exp.eval(state.getSymTable());
            MyIList<int> o = state.getOutput();

            o.Add(tmp);

            return state;
        }
    }
}

