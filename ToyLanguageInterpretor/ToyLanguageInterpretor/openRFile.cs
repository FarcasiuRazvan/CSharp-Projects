using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToyLanguageInterpretor
{
    class openRFile : IStmt
    {
        private String var_file_id;
        private String filename;
        public openRFile(String id, String fname)
        {
            this.var_file_id = id;
            this.filename = fname;
        }
        public String ToString()
        {
            return "openRFile(" + this.var_file_id.ToString() + "," + this.filename + ")";
        }
        public PrgState execute(PrgState state)
        {
            MyIStack<IStmt> stk = state.getStk();

            if (state.getFileTable().lookForFileName(this.filename) != -1) throw new MyException("Wrong Filename");

            TextReader buff = File.OpenText(this.filename);
            int id = state.getFileTable().uniqueId();
            KeyValuePair<String, TextReader> value = new KeyValuePair<String, TextReader>(this.filename, buff);
            state.getFileTable().add(id, value);

            state.getSymTable().add(this.var_file_id, id);
            return null;
        }
    }
}
