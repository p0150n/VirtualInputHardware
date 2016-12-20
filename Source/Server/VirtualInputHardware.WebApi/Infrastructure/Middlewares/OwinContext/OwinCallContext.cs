namespace VirtualInputHardware.WebApi.Infrastructure.Middlewares.OwinContext
{
    using System.Runtime.Remoting.Messaging;
    using Microsoft.Owin;

    /// <summary>
    /// Helper class for setting and accessing the current <see cref="IOwinContext"/>
    /// </summary>
    public class OwinCallContext
    {
        private const string OwinContextKey = "owin.IOwinContext";

        public static IOwinContext Current => (IOwinContext)CallContext.LogicalGetData(OwinContextKey);

        public static void Set(IOwinContext context) => CallContext.LogicalSetData(OwinContextKey, context);

        public static void Remove(IOwinContext context) => CallContext.FreeNamedDataSlot(OwinContextKey);
    }
}
