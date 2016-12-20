namespace VirtualInputHardware.WebApi.Infrastructure.Middlewares.OwinContext
{
    using System.Threading.Tasks;
    using Microsoft.Owin;

    /// <summary>
    /// Sets the current <see cref="IOwinContext"/> for later access via <see cref="OwinCallContext.Current"/>.
    /// Inspiration: https://github.com/neuecc/OwinRequestScopeContext
    /// </summary>
    public class OwinContextMiddleware : OwinMiddleware
    {
        public OwinContextMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                OwinCallContext.Set(context);
                await this.Next.Invoke(context);
            }
            finally
            {
                OwinCallContext.Remove(context);
            }
        }
    }
}