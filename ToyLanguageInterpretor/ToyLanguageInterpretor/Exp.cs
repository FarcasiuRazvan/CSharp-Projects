using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    abstract class Exp
    {
        public abstract int eval(MyIDictionary<String, int> tbl);
        public abstract String ToString();
    }
}
