using AUSKF;
using Microsoft.Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace AUSKF
{
    using System;
    using NLog;
    using Owin;

    public partial class Startup
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            try
            {
                this.ConfigureAuth(app);
            }
            catch (OverflowException ofe)
            {
                log.Error(ofe.Message);
            }
        }
    }
}