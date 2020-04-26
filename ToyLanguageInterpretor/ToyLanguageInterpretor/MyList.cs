using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class MyList<T> : MyIList<T> {
    private List<T> list;

    public MyList()
    {
        this.list = new List<T>();
    }
    public void Add(T var)
    {
        this.list.Add(var);
    }
    public T FromIndex(int index)
    {
        return (T)this.list[index];
    }
    public int GetSize()
    {
        return this.list.Count;
    }
    public override string ToString()
    {
        StringBuilder str = new StringBuilder();
        for (int i = 0; i < this.GetSize(); i++)
        {
            str.Append(this.list[i].ToString());
            str.Append(" ");
        }
        return str.ToString();
    }
}
}
