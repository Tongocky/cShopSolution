using System;
using System.Collections.Generic;
using System.Text;

namespace cShopSolution.Utilities.Exceptions
{
    public class CShopException : Exception
    {
        public CShopException()
        {
        }

        public CShopException(string message)
            : base(message)
        {
        }

        public CShopException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
