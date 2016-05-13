using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ScrobbleMapper.Forms;

namespace ScrobbleMapper
{
    static class TaskUtil
    {
        /// <summary>
        /// Performs an asynchronous task in foreground (blocking the host form's user input) and reports its progress.
        /// </summary>
        /// <typeparam name="T">The task's result type</typeparam>
        /// <param name="host">The host form</param>
        /// <param name="taskContext">The task to perform</param>
        /// <param name="onSuccess">What to do if the operation succeceeds; the parameter is the task's result</param>
        /// <param name="onError">What to do if the operation fails; the parameter is the cause of the failure</param>
        public static void PerformForegroundTask<T>(Form host, IReportingTask<T> taskContext, Action<T> onSuccess, Action<Exception> onError)
        {
            // Disable the host form (like ShowDialog would do)
            host.Enabled = false;

            var reporter = new ProgressReporter(taskContext);
            reporter.Show(host);

            // When the task completes,
            taskContext.Task.ContinueWith(delegate
            {
                // Determine if there was an error
                bool success = false;
                bool canceled = taskContext.Task.IsCanceled;
                Exception error = null;

                if (!taskContext.Task.IsCanceled)
                {
                    error = taskContext.Task.Exception;

                    // It really sucks to have an aggregate exception with a single inner...
                    while (error != null && error is AggregateException && (error as AggregateException).InnerExceptions.Count == 1)
                        error = error.InnerException;

                    // Target invocation exceptions are also meaningless wrappers
                    while (error != null && error is TargetInvocationException)
                        error = error.InnerException;

                    success = error == null;
                }

                // Bring back the host context (in its own thread)
                bool enteredOnce = false;
                Action bringBackHostContext = () =>
                {
                    if (!enteredOnce)
                    {
                        enteredOnce = true;

                        reporter.Close();
                        reporter = null;

                        host.Enabled = true;
                        host.Focus();

                        // Execute the provided actions
                        if (success)
                            onSuccess(taskContext.Task.Value);
                        else if (!canceled)
                            onError(error);
                    }
                };

                IAsyncResult ar = host.BeginInvoke(bringBackHostContext);
                while (!ar.AsyncWaitHandle.WaitOne(500, false) && !enteredOnce)
                {
                    ar = host.BeginInvoke(bringBackHostContext);
                }
            });
        }
    }
}
