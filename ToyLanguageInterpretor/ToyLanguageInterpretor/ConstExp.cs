using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class ConstExp : Exp
    {
        private int number;

        public ConstExp(int val) { this.number = val; }

        public override int eval(MyIDictionary<String, int> tbl)
        {
            return this.number;
        }

        public override String ToString()
        {
            return this.number.ToString() + "";
        }
    }
}
