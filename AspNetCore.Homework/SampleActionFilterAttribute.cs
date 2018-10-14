using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Homework
{
    public class SampleActionFilterAttribute : TypeFilterAttribute
    {
        private bool on;
        public SampleActionFilterAttribute(bool on=false):base(typeof(SampleActionFilterImpl))
        {
            Arguments = new object[] {on};

        }

        private class SampleActionFilterImpl : IActionFilter
        {
            private readonly ILogger logger;
            private bool on = false;
            public SampleActionFilterImpl( bool on, ILoggerFactory loggerFactory)
            {
                this.on = on;
                logger = loggerFactory.CreateLogger<SampleActionFilterAttribute>();
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (on)
                    logger.LogInformation("Action starting...");
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                if (on)
                    logger.LogInformation("Action completed.");
            }
        }
    }
}
