using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroWebServer;

namespace UnitTests
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void IsNullOrEmpty_ShouldReturnTrue_WhenItemIsNull()
        {
            Assert.IsTrue(StringHelpers.IsNullOrEmpty(null));
        }

        [TestMethod]
        public void IsNullOrEmpty_ShouldReturnTrue_WhenItemIsEmpty()
        {
            Assert.IsTrue(StringHelpers.IsNullOrEmpty(""));
        }

        [TestMethod]
        public void IsNullOrEmpty_ShouldReturnFalse_WhenItemIsNotEmpty()
        {
            Assert.IsFalse(StringHelpers.IsNullOrEmpty(" "));
        }

        [TestMethod]
        public void Replace_ShouldReturnNull_WhenItemIsNull()
        {
            Assert.IsNull(StringHelpers.Replace(null, "/", "\\"));
        }

        [TestMethod]
        public void Replace_ShouldReturnEmpty_WhenItemIsEmpty()
        {
            Assert.AreEqual("", StringHelpers.Replace("", "/", "\\"));
        }

        [TestMethod]
        public void Replace_ShouldReturnItem_WhenItemDoesNotContainValue()
        {
            Assert.AreEqual("Hello World", StringHelpers.Replace("Hello World", "/", "\\"));
        }

        [TestMethod]
        public void Replace_ShouldReturnItemWithSingleValueReplaced_WhenItemContainsValueAtBeginning()
        {
            Assert.AreEqual("\\Hello World", StringHelpers.Replace("/Hello World", "/", "\\"));
        }

        [TestMethod]
        public void Replace_ShouldReturnItemWithSingleValueReplaced_WhenItemContainsValueAtEnd()
        {
            Assert.AreEqual("Hello World\\", StringHelpers.Replace("Hello World/", "/", "\\"));
        }

        [TestMethod]
        public void Replace_ShouldReturnItemWithSingleValueReplaced_WhenItemContainsValueInMiddle()
        {
            Assert.AreEqual("Hello \\World", StringHelpers.Replace("Hello /World", "/", "\\"));
        }

        [TestMethod]
        public void Replace_ShouldReturnItemWithMultipleValuesReplaced_WhenItemContainsValueMultipleTimes()
        {
            Assert.AreEqual("\\He\\llo \\Wor\\ld\\", StringHelpers.Replace("/He/llo /Wor/ld/", "/", "\\"));
        }
    }
}
