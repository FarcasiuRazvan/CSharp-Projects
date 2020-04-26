using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class CompStmt : IStmt
    {
        private IStmt first;
        private IStmt second;
        public CompStmt(IStmt one, IStmt two)
        {
            this.first = one;
            this.second = two;
        }
        public PrgState execute(PrgState state)
        {
            MyIStack<IStmt> stk = state.getStk();
            stk.push(this.second);
            stk.push(this.first);

            return null;
            //return state;
        }
        public override String ToString()
        {
            return "" + this.first.ToString() + "" + this.second.ToString() + "";
        }
    }
}
