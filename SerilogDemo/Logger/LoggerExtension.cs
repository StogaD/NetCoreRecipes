using Serilog;
using Serilog.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerilogDemo.Logger
{
    public static class LoggerExtension
    {
        public static LoggerConfiguration WithAppInfo(this LoggerEnrichmentConfiguration enrich)
        {
            if (enrich == null)
            {
                throw new ArgumentNullException(nameof(enrich));
            }

            return enrich.With<AppInfoLogEnricher>();
        }
    }
}
