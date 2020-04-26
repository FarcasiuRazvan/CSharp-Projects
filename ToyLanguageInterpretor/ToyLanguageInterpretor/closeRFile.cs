using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToyLanguageInterpretor
{
    class closeRFile : IStmt
    {
        private Exp exp_file_id;
        public closeRFile(Exp id)
        {
            this.exp_file_id = id;
        }
        public override String ToString()
        {
            return "closeRFile(" + this.exp_file_id.ToString() + ")";
        }
        public PrgState execute(PrgState state)
        {
            int value;
            value = this.exp_file_id.eval(state.getSymTable());
            TextReader buff;
            buff = state.getFileTable().lookup(value).Value;
            if (buff == null) throw new MyException("No buff found !!!");
            buff.Close();
            state.getFileTable().delete(value);
            return null;
        }
    }
}
