using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    interface IPrgRepo
    {
        PrgState getCrtPrg();
        void add(PrgState pr);
        void LogPrgStateExec(PrgState p);
    }
}
