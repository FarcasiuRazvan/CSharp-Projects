using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class IfStmt : IStmt
    {
        private Exp exp;
        private IStmt thenS;
        private IStmt elseS;

        public IfStmt(Exp e, IStmt Then, IStmt Else)
        {
            this.exp = e;
            this.thenS = Then;
            this.elseS = Else;
        }

        public PrgState execute(PrgState state)
        {
            try
            {
                MyIStack<IStmt> execStack = state.getStk();
                int tmp = exp.eval(state.getSymTable());
                if (tmp != 0)
                    execStack.push(this.thenS);
                else
                    execStack.push(this.elseS);
            }
            catch (MyException mess)
            {
                Console.WriteLine(mess.getMessage());
            }
            return null;
            //return state;
        }

        public override String ToString()
        {
            return "IF(" + this.exp.ToString() + ")THEN(" + this.thenS.ToString() + ")ELSE(" + this.elseS.ToString() + ")";
        }
    }
}
