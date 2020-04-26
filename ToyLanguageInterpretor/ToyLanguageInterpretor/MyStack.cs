using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class MyStack<T> : MyIStack<T> {
    private Stack<T> stac;

    public MyStack()
    {
        this.stac = new Stack<T>();
    }
    public T pop()
    {
        return this.stac.Pop();
    }
    public void push(T v)
    {
        this.stac.Push(v);
    }

    public Stack<T> getStk()
    {
        return this.stac;
    }

    public bool isEmpty()
    {
        if (this.stac.Count == 0) return true;
        else return false;
    }

    public T top()
    {
        return this.stac.Peek();
    }

    public override string ToString()
    {
        StringBuilder str = new StringBuilder();
        foreach (T element in this.stac)
        {
            str.Append(element.ToString());
            str.Append(" ");
        }
        return str.ToString();
    }
}
}
