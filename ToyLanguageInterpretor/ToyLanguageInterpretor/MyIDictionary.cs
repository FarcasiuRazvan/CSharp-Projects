using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    interface MyIDictionary<Key, Value>
    {

        void add(Key id, Value val);
        void update(Key id, Value val);
        bool isDefined(Key id);
        Value lookup(Key id);
        string ToString();
        //void updateValue(Key id, Value val);
        //HashMap<Key, Value> getContent();
        //void setContent(HashMap<Key, Value> dict);
    }
}
