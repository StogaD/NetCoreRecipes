﻿using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SerilogDemo.Logger
{
    public class AppInfoLogEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {

            //  var property = new LogEventProperty("Version", new ScalarValue(2));
            //  logEvent.AddPropertyIfAbsent(property);

            var prop = propertyFactory.CreateProperty("AppInfo", Assembly.GetExecutingAssembly().GetName().Name);

            logEvent.AddPropertyIfAbsent(prop);
        }
    }
}
