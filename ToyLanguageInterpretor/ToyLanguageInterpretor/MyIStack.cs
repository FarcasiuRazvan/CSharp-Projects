using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    interface MyIStack<T>
    {
        T pop();
        void push(T v);
        bool isEmpty();
        string ToString();
        T top();
    }
}
