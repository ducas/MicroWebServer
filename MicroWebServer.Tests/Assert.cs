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

        internal static void IsNull(string actual)
        {
            AreEqual(null, actual);
        }

        public static void Fail(string message)
        {
            throw new AssertException(message);
        }
    }

    public class AssertException : Exception
    {
        public AssertException(string message) : base(message)
        {
        }
    }
}
