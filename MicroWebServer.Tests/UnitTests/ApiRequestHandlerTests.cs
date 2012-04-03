using System;
using Microsoft.SPOT;
using System.Collections;
using System.Reflection;
using MicroWebServer.Tests.Mocks;
using MFUnit;

namespace MicroWebServer.Tests.UnitTests
{
    public class ApiRequestHandlerTests
    {
        public void Ctor_ShouldSetPrefix()
        {
            var handler = new ApiRequestHandler("/test/");
            Assert.AreEqual("/test/", handler.Prefix);
        }

        public void Ctor_ShouldPrependSlashToPrefix()
        {
            var handler = new ApiRequestHandler("test/");
            Assert.AreEqual("/test/", handler.Prefix);
        }

        public void Ctor_ShouldAppendSlashToPrefix()
        {
            var handler = new ApiRequestHandler("/test");
            Assert.AreEqual("/test/", handler.Prefix);
        }

        public void Ctor_ShouldAppendAndPrependSlashToPrefix()
        {
            var handler = new ApiRequestHandler("test");
            Assert.AreEqual("/test/", handler.Prefix);
        }

        public void Register_ShouldThrowException_WhenTypeIsNotApiController()
        {
            try
            {
                var handler = new ApiRequestHandler("test");
                handler.Register("test", typeof(int));
            }
            catch (ApplicationException)
            {
                return;
            }
            Assert.Fail("ApplicationExcpetion was not thrown");
        }
        
        public void Register_ShouldThrowException_WhenTypeDoesNotHaveParameterlessConstructor()
        {
            try
            {
                var handler = new ApiRequestHandler("test");
                handler.Register("test", typeof(BadController));
            }
            catch (ApplicationException)
            {
                return;
            }
            Assert.Fail("ApplicationExcpetion was not thrown");
        }

       

        public void Register_ShouldRegisterType_WhenTypeIsValid()
        {
            var handler = new ApiRequestHandler("test");
            var type = typeof(ValidController);
            handler.Register("Test", type);
            Assert.AreEqual(type, handler.GetControllerType("Test"));
        }

        public void RegisterAll_ShouldRegisterTypesWithDefaultName_WhenAssemblyHasValidControllers()
        {
            var handler = new ApiRequestHandler("test");
            var type = typeof(ValidController);
            handler.RegisterAll(Assembly.GetAssembly(type));
            Assert.AreEqual(type.FullName, handler.GetControllerType("Valid").FullName);
        }
    }
}
