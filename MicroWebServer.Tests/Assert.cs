using System;
using Microsoft.SPOT;

namespace MicroWebServer.Tests
{
    public static class Assert
    {
        public static void AreEqual(object expected, object actual)
        {
            if (expected == null && expected == actual) return;
            if (expected == null || !expected.Equals(actual)) throw new AssertException("Expected: \"" + expected + "\", Actual: \"" + actual + "\"");
        }

        internal static void IsTrue(bool actual)
        {
            AreEqual(true, actual);
        }

        internal static void IsFalse(bool actual)
        {
            AreEqual(false, actual);
        }

        public static void Fail(string message)
        {
            throw new AssertException(message);
        }

        internal static void IsNull(object actual)
        {
            if (actual != null) throw new AssertException("Expected: \"null\", Actual: \"not null\"");
        }

        internal static void IsNotNull(object actual)
        {
            if (actual == null) throw new AssertException("Expected: \"not null\", Actual: \"null\"");
        }
    }

    public class AssertException : Exception
    {
        public AssertException(string message) : base(message)
        {
        }
    }
}
