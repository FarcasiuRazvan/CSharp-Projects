using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class MyDictionary<Key, Value> : MyIDictionary<Key, Value>  {
    private Dictionary<Key, Value> dictionary;

    public MyDictionary()
    {
        this.dictionary = new Dictionary<Key, Value>();
    }
    public void add(Key id, Value val)
    {
        this.dictionary.Add(id, val);
    }
    public void update(Key id, Value val)
    {
        this.dictionary.Remove(id);
        this.dictionary.Add(id, val);
    }
    public bool isDefined(Key id)
    {
        return this.dictionary.ContainsKey(id);
    }
    public Value lookup(Key id)
    {
        return this.dictionary.GetValueOrDefault(id);
    }
    public override string ToString()
    {
        StringBuilder str = new StringBuilder();
        this.dictionary.Keys.ToString();
        new List<Key>(this.dictionary.Keys).ForEach(e =>str.Append(e.ToString()+"="+ this.dictionary[e].ToString()+"\n "));
        return str.ToString();
    }
}
}
