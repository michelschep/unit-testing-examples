using System;
using FluentAssertions;
using Xunit;

namespace FastWell.UnitTestExamples
{
    public class PakketjesOptellerBuilder
    {
        public Calculator Build()
        {
            return new Calculator();
        }
    }
}
