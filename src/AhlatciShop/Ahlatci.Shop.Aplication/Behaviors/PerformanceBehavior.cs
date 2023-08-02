using ArxOne.MrAdvice.Advice;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahlatci.Shop.Aplication.Behaviors
{
    public class PerformanceBehavior : Attribute, IMethodAdvice
    {
        public void Advise(MethodAdviceContext context)
        {  

            //kronometre
            Stopwatch stopwatch = new Stopwatch();
           
            //kronometre başladı
            stopwatch.Start();
            context.Proceed();
            //kronometre durduruldu
            stopwatch.Stop();

            var totalDuration = stopwatch.Elapsed.TotalSeconds;

            Log.Information($"{context.TargetName} metodu {totalDuration} saniyede tamamlandı");

        }
    }
}
