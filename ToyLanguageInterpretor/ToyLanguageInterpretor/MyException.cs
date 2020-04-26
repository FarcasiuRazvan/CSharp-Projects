using System;
using System.Collections.Generic;
using System.Text;

namespace ToyLanguageInterpretor
{
    class MyException: ApplicationException
    {
        public MyException() { }
        public MyException(String message) : base(message) { }
        public MyException(String message, Exception exp) : base(message, exp) { }

        public String getMessage() { return this.Message; }
    }
}
