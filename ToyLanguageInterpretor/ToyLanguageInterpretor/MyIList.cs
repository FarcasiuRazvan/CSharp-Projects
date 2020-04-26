using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    interface MyIList<T>
    {
        void Add(T var);
        T FromIndex(int index);
        int GetSize();
        string ToString();
    }
}
