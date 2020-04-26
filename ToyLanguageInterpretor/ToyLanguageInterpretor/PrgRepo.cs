using System;
using System.Collections;
using System.IO;
using System.Text;

namespace ToyLanguageInterpretor
{
    class PrgRepo : IPrgRepo
    {
        private ArrayList programStates;
        private string logFilePath;
        public PrgRepo(string logFile)
        {
            this.programStates = new ArrayList();
            this.logFilePath = logFile;
        }

        public PrgState getCrtPrg()
        {
            return (PrgState)this.programStates[0];
        }

        public void add(PrgState pr)
        {
            this.programStates.Add(pr);
        }
        public void LogPrgStateExec(PrgState p)
        {

            File.WriteAllText(this.logFilePath, p.ToString());
            Console.WriteLine(p.ToString());

        }

        public ArrayList getPrgList() { return this.programStates; }
        public void setPrgList(ArrayList states) { this.programStates = states; }

    }
}
