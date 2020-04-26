using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToyLanguageInterpretor
{
    class MyFileTable<Key,Value> : MyIFileTable<Key,Value>
    {
        private Dictionary<int, KeyValuePair<String, TextReader>> fileTable;
        private int unique = 0;

        public MyFileTable()
        {
            this.fileTable = new Dictionary<int, KeyValuePair<String, TextReader>>();
        }
        public void add(int id, KeyValuePair<String, TextReader> val)
        {
            this.fileTable.Add(id, val);
        }
        public void update(int id, KeyValuePair<String, TextReader> val)
        {
            this.fileTable.Add(id, val);
        }
        public bool isDefined(int id)
        {
            return this.fileTable.ContainsKey(id);
        }
        public KeyValuePair<String, TextReader> lookup(int id)
        {
            return this.fileTable.GetValueOrDefault(id);
        }
        public int lookFor(KeyValuePair<String, TextReader> val)
        {
            foreach(int e in new List<int>(this.fileTable.Keys))
            {
                if (Object.Equals(this.fileTable.GetValueOrDefault(e), val)) return e;
             
            }
            return -1;
        }
        public void delete(int id)
        {
            this.fileTable.Remove(id);
        }
        public int lookForFileName(String fileName)
        {

            foreach (KeyValuePair<String, TextReader> e in new List<KeyValuePair<String, TextReader>>(this.fileTable.Values))
            {
                if (fileName==e.Key) return this.lookFor(e);
            }
            return -1;
        }
        public override String ToString()
        {
            StringBuilder str = new StringBuilder();
            this.fileTable.Keys.ToString();
            new List<int>(this.fileTable.Keys).ForEach(e => str.Append(e.ToString() + "," + this.fileTable[e].Key.ToString()+ "," + this.fileTable[e].Value.ToString() + "\n ;"));
            return str.ToString();
        }
        public int uniqueId()
        {
            return this.unique++;
        }
    }
}
