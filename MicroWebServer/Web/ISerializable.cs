using System;

namespace MicroWebServer.Web
{
    public interface ISerializable
    {
        string Serialize();
        void Deserialize(string value);
    }
}
