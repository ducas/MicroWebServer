using System;

namespace MicroWebServer
{
    public interface ISerializable
    {
        string Serialize();
        void Deserialize(string value);
    }
}
