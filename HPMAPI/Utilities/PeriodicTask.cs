using System;
using System.Threading;
using System.Threading.Tasks;

namespace HPMAPI.Utilities
{
    // Taken from https://stackoverflow.com/a/23814733/4508432
    // Adapted slightly to support the dueTime concept from the built-in timer class.
    public class PeriodicTask
    {
        public static async Task Run(Action action, TimeSpan dueTime, TimeSpan period, CancellationToken cancellationToken)
        {
            await Task.Delay(dueTime, cancellationToken);
            if (!cancellationToken.IsCancellationRequested)
                action();
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(period, cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                    action();
            }
        }

        public static Task Run(Action action, TimeSpan dueTime, TimeSpan period)
        {
            return Run(action, period, dueTime, CancellationToken.None);
        }
    }
}
