using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.WebApi.UnitTests
{
    internal class StringValuesComparer : IEqualityComparer<StringValues>
    {
        private IEqualityComparer<StringValues> valueComparer;
        public StringValuesComparer(IEqualityComparer<StringValues> valueComparer = null)
        {
            this.valueComparer = valueComparer ?? EqualityComparer<StringValues>.Default;
        }

        public bool Equals(StringValues x, StringValues y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(StringValues obj)
        {
            throw new NotImplementedException();
        }
    }
}
