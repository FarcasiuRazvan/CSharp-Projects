using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    interface IStmt
    {

        String ToString();
        PrgState execute(PrgState state);
    }
}
