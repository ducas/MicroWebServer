using System;
using Microsoft.SPOT;
using System.IO;
using MFUnit;

namespace MicroWebServer.Tests.UnitTests
{
    public class JsonSerializerTests
    {
        static string Serialize(object o)
        {
            using (var ms = new MemoryStream())
            using (var w = new StreamWriter(ms))
            using (var r = new StreamReader(ms))
            {
                new JsonSerializer().Serialize(o, w);
                w.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                return r.ReadToEnd();
            }
        }

        public void Serialize_ShouldWriteNull_WhenObjectIsNull()
        {
            Assert.AreEqual("null", Serialize(null));
        }

        public void Serialize_ShouldWriteValue_WhenObjectIsString()
        {
            Assert.AreEqual("\"Hello World\"", Serialize("Hello World"));
        }

        public void Serialize_ShouldWriteEscapeValue_WhenObjectIsString()
        {
            Assert.AreEqual("\"Hello \\\" World\"", Serialize("Hello \" World"));
        }

        public void Serialize_ShouldWriteValue_WhenObjectIsInt()
        {
            Assert.AreEqual("\"1\"", Serialize(1));
        }

        public void Serialize_ShouldWriteValue_WhenObjectIsDouble()
        {
            Assert.AreEqual("\"1\"", Serialize(1D));
        }

        public void Serialize_ShouldWriteValue_WhenObjectIsBool()
        {
            Assert.AreEqual("\"True\"", Serialize(true));
        }

        public void Serialize_ShouldWriteEmptyArray_WhenObjectIsEmptyEnumerable()
        {
            Assert.AreEqual("[]", Serialize(new int[] { }));
        }

        public void Serialize_ShouldWriteArrayOfValues_WhenObjectIsValueTypeArray()
        {
            Assert.AreEqual("[\"1\", \"2\", \"3\"]", Serialize(new int[] { 1, 2, 3 }));
        }

        class ComplexType
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsDone { get; set; }
        }

        public void Serialize_ShouldWriteObject_WhenObjectIsComplexType()
        {
            Assert.AreEqual("{ \"id\": \"1\", \"name\": \"blah\", \"isDone\": \"False\" }", Serialize(new ComplexType { Id = 1, Name = "blah", IsDone = false }));
        }

        public void Serialize_ShouldWriteArrayOfObjects_WhenObjectIsComplexTypeArray()
        {
            Assert.AreEqual(
                "[{ \"id\": \"1\", \"name\": \"blah\", \"isDone\": \"False\" }, { \"id\": \"2\", \"name\": \"blah 2\", \"isDone\": \"True\" }]",
                Serialize(new[] { new ComplexType { Id = 1, Name = "blah", IsDone = false }, new ComplexType { Id = 2, Name = "blah 2", IsDone = true } })
                );
        }

        class NestedComplexType
        {
            public int Id { get; set; }
            public NestedComplexType Child { get; set; }
        }

        public void Serialize_ShouldWriteNestedComplexTypes_WhenObjectHasNestedComplexTypes()
        {
            Assert.AreEqual(
                "{ \"id\": \"1\", \"child\": { \"id\": \"2\", \"child\": null } }",
                Serialize(new NestedComplexType { Id = 1, Child = new NestedComplexType { Id = 2 } })
                );
        }
    }
}
