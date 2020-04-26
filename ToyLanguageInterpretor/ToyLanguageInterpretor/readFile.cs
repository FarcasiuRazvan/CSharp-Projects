using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToyLanguageInterpretor
{
    class readFile : IStmt
    {
        private Exp exp_file_id;
        private String var_name;
        public readFile(Exp id, String name)
        {
            this.exp_file_id = id;
            this.var_name = name;
        }
        public override String ToString()
        {
            return "readFile(" + this.exp_file_id.ToString() + "," + this.var_name + ")";
        }
        public PrgState execute(PrgState state)
        {
        int value, theValue;
        value=this.exp_file_id.eval(state.getSymTable());
        TextReader buff;
        buff=state.getFileTable().lookup(value).Value;
        if(buff==null) throw new MyException("No buff found !!!");

        String str = buff.ReadLine();
        if(str!=null) theValue=int.Parse(str);
        else theValue=0;

        if(!state.getSymTable().isDefined(var_name)) state.getSymTable().add(var_name, theValue);
        else state.getSymTable().update(var_name, theValue);
        return null;
        }
    }
}
