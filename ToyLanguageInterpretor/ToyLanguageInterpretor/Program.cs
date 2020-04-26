using System;
using System.Collections.Generic;
using System.IO;

namespace ToyLanguageInterpretor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nEXAMPLE 1\n");
            //..........
            // v=2;Print(v)
            //..........
            //Statement
            IStmt statement1 = new CompStmt(new AssignStmt("v", new ConstExp(2)), new PrintStmt(new VarExp("v")));


            //..........
            MyIStack<IStmt> stk1 = new MyStack<IStmt>();
            MyIDictionary<String, int> dict1 = new MyDictionary<String,int>();
            MyIList<int> list1 = new MyList<int>();
            MyIFileTable<int,KeyValuePair<String, TextReader>> fileTable1 = new MyFileTable<int,KeyValuePair<String, TextReader>>();
            
            PrgState prgState1 = new PrgState(stk1, dict1, list1, fileTable1, statement1);
            IPrgRepo repos1 = new PrgRepo("c:\\Users\\Wolf\\Desktop\\report1.txt");
            repos1.add(prgState1);
            Controller ctrl1 = new Controller(repos1);
            ctrl1.allStep();

            Console.WriteLine("\nEXAMPLE 2\n");
            //..........
            //a=2+3*5;b=a+1;Print(b)
            //..........
            //Statement
            AssignStmt a2 = new AssignStmt("a", new ArithExp(new ConstExp(2), new ArithExp(new ConstExp(3), new ConstExp(5), 3), 1));
            AssignStmt b2 = new AssignStmt("b", new ArithExp(new VarExp("a"), new ConstExp(1), 1));
            PrintStmt v2 = new PrintStmt(new VarExp("b"));
            IStmt statement2 = new CompStmt(new CompStmt(a2, b2), v2);


            //..........
            MyIStack<IStmt> stk2 = new MyStack<IStmt>();
            MyIDictionary<String, int> dict2 = new MyDictionary<String, int>();
            MyIList<int> list2 = new MyList<int>();
            MyIFileTable<int, KeyValuePair<String, TextReader>> fileTable2 = new MyFileTable<int, KeyValuePair<String, TextReader>>();

            PrgState prgState2 = new PrgState(stk2, dict2, list2, fileTable2, statement2);
            IPrgRepo repos2 = new PrgRepo("c:\\Users\\Wolf\\Desktop\\report2.txt");
            repos2.add(prgState2);
            Controller ctrl2 = new Controller(repos2);
            ctrl2.allStep();

            Console.WriteLine("\nEXAMPLE 3\n");
            //..........
            // a=2-2;(If a Then v=2 Else v=3);Print(v)
            //..........
            // Statement
            AssignStmt a3 = new AssignStmt("a", new ArithExp(new ConstExp(2), new ConstExp(2), 2));
            IfStmt b3 = new IfStmt(new VarExp("a"), new AssignStmt("v", new ConstExp(2)), new AssignStmt("v", new ConstExp(3)));
            PrintStmt v3 = new PrintStmt(new VarExp("v"));
            IStmt statement3 = new CompStmt(new CompStmt(a3, b3), v3);


            //..........
            MyIStack<IStmt> stk3 = new MyStack<IStmt>();
            MyIDictionary<String, int> dict3 = new MyDictionary<String, int>();
            MyIList<int> list3 = new MyList<int>();
            MyIFileTable<int, KeyValuePair<String, TextReader>> fileTable3 = new MyFileTable<int, KeyValuePair<String, TextReader>>();

            PrgState prgState3 = new PrgState(stk3, dict3, list3, fileTable3, statement3);
            IPrgRepo repos3 = new PrgRepo("c:\\Users\\Wolf\\Desktop\\report3.txt");
            repos3.add(prgState3);
            Controller ctrl3 = new Controller(repos3);
            ctrl3.allStep();
            //..........
            /*
            Console.WriteLine("\nEXAMPLE 4\n");
            //..........
            // openRFile(var_f,"test.in");
            // readFile(var_f,var_c);print(var_c);
            // (if var_c then readFile(var_f,var_c);print(var_c) else print(0));
            // closeRFile(var_f);
            //..........
            // Statement
            Exp var_f = new VarExp("var_f");
            Exp var_c = new VarExp("var_c");
            openRFile open4 = new openRFile("var_f", "c:\\Users\\Wolf\\Desktop\\test.in.txt");
            readFile read4 = new readFile(var_f, "var_c");
            IStmt readPrint = new CompStmt(read4, new PrintStmt(var_c));
            IStmt if4 = new IfStmt(var_c, new CompStmt(read4, new PrintStmt(var_c)), new PrintStmt(new ConstExp(0)));
            closeRFile close4 = new closeRFile(var_f);
            IStmt statement4 = new CompStmt(new CompStmt(open4, readPrint), new CompStmt(if4, close4));

            //..........
            MyIStack<IStmt> stk4 = new MyStack<IStmt>();
            MyIDictionary<String, int> dict4 = new MyDictionary<String, int>();
            MyIList<int> list4 = new MyList<int>();
            MyIFileTable<int, KeyValuePair<String, TextReader>> fileTable4 = new MyFileTable<int, KeyValuePair<String, TextReader>>();

            PrgState prgState4 = new PrgState(stk4, dict4, list4, fileTable4, statement4);
            IPrgRepo repos4 = new PrgRepo("c:\\Users\\Wolf\\Desktop\\report4.txt");
            repos4.add(prgState4);
            Controller ctrl4 = new Controller(repos4);
            ctrl4.allStep();
            //..........

            Console.WriteLine("\nEXAMPLE 5\n");
            //..........
            // openRFile(var_f,"test.in");
            // readFile(var_f+2,var_c);print(var_c);
            // (if var_c then readFile(var_f,var_c);print(var_c)
            // else print(0));
            // closeRFile(var_f)
            //..........
            // Statement
            Exp var_f1 = new VarExp("var_f1");
            Exp var_c1 = new VarExp("var_c1");
            openRFile open5 = new openRFile("var_f1", "c:\\Users\\Wolf\\Desktop\\test.in.txt");
            readFile read5 = new readFile(new ArithExp(var_f1, new ConstExp(2), 1), "var_c1");
            IStmt readPrint5 = new CompStmt(new readFile(var_f1, "var_c1"), new PrintStmt(var_c1));
            IStmt if5 = new IfStmt(var_c1, new CompStmt(read5, new PrintStmt(var_c1)), new PrintStmt(new ConstExp(0)));
            closeRFile close5 = new closeRFile(var_f1);
            IStmt statement5 = new CompStmt(new CompStmt(open5, readPrint5), new CompStmt(if4, close5));

            //..........
            MyIStack<IStmt> stk5 = new MyStack<IStmt>();
            MyIDictionary<String, int> dict5 = new MyDictionary<String, int>();
            MyIList<int> list5 = new MyList<int>();
            MyIFileTable<int, KeyValuePair<String, TextReader>> fileTable5 = new MyFileTable<int, KeyValuePair<String, TextReader>>();

            PrgState prgState5 = new PrgState(stk5, dict5, list5, fileTable5, statement5);
            IPrgRepo repos5 = new PrgRepo("c:\\Users\\Wolf\\Desktop\\report5.txt");
            repos5.add(prgState5);
            Controller ctrl5 = new Controller(repos5);
            ctrl5.allStep();
            Console.ReadLine();*/
        }
    }
}






