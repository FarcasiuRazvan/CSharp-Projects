using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class AssignStmt : IStmt
    {
        private String id;
        private Exp exp;

        public AssignStmt(String ids, Exp ex)
        {
            this.id = ids;
            this.exp = ex;
        }
        public PrgState execute(PrgState state)
        {
            MyIStack<IStmt> stk = state.getStk();
            MyIDictionary<String, int> symTbl = state.getSymTable();
            try
            {
                int val = this.exp.eval(symTbl);
                if (symTbl.isDefined(this.id)) symTbl.update(this.id, val);
                else symTbl.add(this.id, val);
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
            return this.id + "=" + this.exp.ToString();
        }
    }
}
