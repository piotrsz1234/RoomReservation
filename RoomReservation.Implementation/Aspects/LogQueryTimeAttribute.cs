using System.Diagnostics;
using Microsoft.Extensions.Logging;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace RoomReservation.Implementation.Aspects
{
    [PSerializable]
    public class LogQueryTimeAttribute : OnMethodBoundaryAspect
    {
        private static readonly Stopwatch Stopwatch = Stopwatch.StartNew();
        public static ILogger Logger { get; set; } = null!;

        public override void OnEntry(MethodExecutionArgs args)
        {
            Stopwatch.Restart();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            if(args.Method.IsConstructor)
                return;
            
            var time = Stopwatch.ElapsedMilliseconds;
            Logger?.LogInformation("Method {MethodName} executed in {time} ms", args.Method.Name, time);
        }
    }
}