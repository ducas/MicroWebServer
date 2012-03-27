using System;
using System.Text;
using MicroWebServer;

namespace MicroWebServer.Tests.UnitTests
{
    public class StringHelperTests
    {
        public void IsNullOrEmpty_ShouldReturnTrue_WhenItemIsNull()
        {
            Assert.IsTrue(StringHelpers.IsNullOrEmpty(null));
        }

        public void IsNullOrEmpty_ShouldReturnTrue_WhenItemIsEmpty()
        {
            Assert.IsTrue(StringHelpers.IsNullOrEmpty(""));
        }

        public void IsNullOrEmpty_ShouldReturnFalse_WhenItemIsNotEmpty()
        {
            Assert.IsFalse(StringHelpers.IsNullOrEmpty(" "));
        }

        public void Replace_ShouldReturnNull_WhenItemIsNull()
        {
            Assert.IsNull(StringHelpers.Replace(null, "/", "\\"));
        }

        public void Replace_ShouldReturnEmpty_WhenItemIsEmpty()
        {
            Assert.AreEqual("", StringHelpers.Replace("", "/", "\\"));
        }

        public void Replace_ShouldReturnItem_WhenItemDoesNotContainValue()
        {
            Assert.AreEqual("Hello World", StringHelpers.Replace("Hello World", "/", "\\"));
        }

        public void Replace_ShouldReturnItemWithSingleValueReplaced_WhenItemContainsValueAtBeginning()
        {
            Assert.AreEqual("\\Hello World", StringHelpers.Replace("/Hello World", "/", "\\"));
        }

        public void Replace_ShouldReturnItemWithSingleValueReplaced_WhenItemContainsValueAtEnd()
        {
            Assert.AreEqual("Hello World\\", StringHelpers.Replace("Hello World/", "/", "\\"));
        }

        public void Replace_ShouldReturnItemWithSingleValueReplaced_WhenItemContainsValueInMiddle()
        {
            Assert.AreEqual("Hello \\World", StringHelpers.Replace("Hello /World", "/", "\\"));
        }

        public void Replace_ShouldReturnItemWithMultipleValuesReplaced_WhenItemContainsValueMultipleTimes()
        {
            Assert.AreEqual("\\He\\llo \\Wor\\ld\\", StringHelpers.Replace("/He/llo /Wor/ld/", "/", "\\"));
        }

        public void Replace_ShouldReturnItem_WhenItemContainsLongValue()
        {
            Assert.AreEqual("Hello \\\" World", StringHelpers.Replace("Hello \" World", "\"", "\\\""));
        }
    }
}
