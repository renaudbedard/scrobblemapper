using System;
using System.Threading;
using System.Threading.Tasks;

namespace ScrobbleMapper
{
    /// <summary>
    /// Adds progress tracking to TPL tasks
    /// </summary>
    class ReportingTask : ReportingTaskBase, IReportingTask
    {
        public Task Task { get; set; }
    }

    /// <summary>
    /// Adds progress tracking to TPL futures
    /// </summary>
    class ReportingTask<T> : ReportingTaskBase, IReportingTask<T>
    {
        public Future<T> Task { get; set; }

        Task IReportingTask.Task
        {
            get { return Task; }
        }
    }

    /// <summary>
    /// The immutable-ish interface to a reporting future
    /// </summary>
    interface IReportingTask<T> : IReportingTask
    {
        new Future<T> Task { get; }
    }

    /// <summary>
    /// The immutable-ish interface to a reporting task
    /// </summary>
    interface IReportingTask
    {
        Task Task { get; }

        event Action ProgressChanged;
        event Action DescriptionChanged;

        float Progress { get; }
        string Description { get; }
    }

    /// <summary>
    /// Common code for reporting tasks and futures
    /// </summary>
    abstract class ReportingTaskBase
    {
        public event Action ProgressChanged = ActionUtil.NullAction;
        public event Action DescriptionChanged = ActionUtil.NullAction;

        public void ReportItemCompleted()
        {
            Interlocked.Increment(ref itemsCompleted);
            ProgressChanged();
        }

        int itemsCompleted;
        public int ItemsCompleted
        {
            get { return itemsCompleted; }
        }

        int totalItems;
        public int TotalItems
        {
            get { return totalItems; }
            set
            {
                totalItems = value;
                ProgressChanged();
            }
        }

        string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                DescriptionChanged();
            }
        }

        public float Progress
        {
            get
            {
                int denominator = TotalItems == 0 ? 1 : TotalItems;
                return (float)ItemsCompleted / denominator;
            }
        }
    }
}