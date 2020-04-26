using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class Controller
    {
        private IPrgRepo repo;

        public Controller(IPrgRepo newrepo)
        {
            this.repo = newrepo;
        }

        public void allStep()
        {
            PrgState prgState = repo.getCrtPrg();

            while (!prgState.getStk().isEmpty())
                executeOneStatement(prgState);

            Console.WriteLine("-----ProgramStateStart------");
            Console.WriteLine(prgState.ToString());
            Console.WriteLine("-----ProgramStateEnd------");
        }
    private void executeOneStatement(PrgState prg)
    {
        MyIStack<IStmt> execStack = prg.getStk();
        if (!execStack.isEmpty())
        {
            IStmt statement = execStack.pop();
            statement.execute(prg);
            this.repo.LogPrgStateExec(prg);

            Console.WriteLine("Execution stack: ");
            Console.WriteLine(execStack.ToString());//.toStr());
            Console.WriteLine("Symbol Table: ");
            Console.WriteLine(prg.getSymTable().ToString());
            Console.WriteLine("Output: ");
            Console.WriteLine(prg.getOutput().ToString());
            Console.WriteLine("\n ==> \n");
        }
    }
}
}
