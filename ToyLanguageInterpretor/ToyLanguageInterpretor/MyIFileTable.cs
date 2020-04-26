using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToyLanguageInterpretor
{
    interface MyIFileTable<Key,Value>
    {
        void add(int id, KeyValuePair<String, TextReader> val);
        void update(int id, KeyValuePair<String, TextReader> val);
        bool isDefined(int id);
        KeyValuePair<String, TextReader> lookup(int id);
        String ToString();
        int uniqueId();
        int lookFor(KeyValuePair<String, TextReader> val);
        int lookForFileName(String val);
        void delete(int id);
    }
}
