using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class ArithExp : Exp
    {
        private Exp e1;
        private Exp e2;
        private int op; //1 stands for +, 2 for -, etc...

        public ArithExp(Exp one, Exp two, int operation)
        {
            this.e1 = one;
            this.e2 = two;
            this.op = operation;
        }
        //override
        public override int eval(MyIDictionary<String, int> tbl)
        {
            try
            {

                if (this.op == 1) return (this.e1.eval(tbl) + this.e2.eval(tbl));
                else
                if (this.op == 2) return (this.e1.eval(tbl) - this.e2.eval(tbl));
                else
                if (this.op == 3) return (this.e1.eval(tbl) * this.e2.eval(tbl));
                else
                if (this.op == 4)
                {
                    if (this.e2.eval(tbl) == 0) throw new MyException("ERROR: DIVISION BY 0");
                    else return (this.e1.eval(tbl) / this.e2.eval(tbl));
                }
            }
            catch (MyException mess)
            {
                Console.WriteLine(mess.getMessage());
            }
            throw new MyException("Error: unknown operator!!");
        }

        public override String ToString()
        {
            if (op == 1) return e1 + "+" + e2 + " ";
            else if (op == 2) return e1 + "-" + e2 + " ";
            else if (op == 3) return e1 + "*" + e2 + " ";
            else if (op == 4) return e1 + "/" + e2 + " ";
            return "";
        }
    }
}
