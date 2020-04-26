using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class VarExp : Exp
    {
        private String id;

        public VarExp(String ids) { this.id = ids; }

        public override int eval(MyIDictionary<String, int> nTable)
        {
            if (nTable.isDefined(this.id)) return nTable.lookup(this.id);

            else throw new MyException("ERROR: NOT FOUND!!!");
        }

        public override String ToString()
        {
            return "Eval(" + this.id + ")=LookUp(SymTable," + this.id + ");";
        }

}
}
