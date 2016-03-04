using System;
using System.Threading;

namespace TaskScheduler
{
    interface IJob
    {
        void Execute(object argument);
    }

    class PrintJob : IJob
    {
        public void Execute(object argument)
        {
            Console.WriteLine(argument);
        }
    }

    class TaskScheduler
    {
        static void Main(string[] args)
        {
            TaskScheduler.ScheduleDelayedJob(new PrintJob(), "hello", new TimeSpan(0, 0, 2));
            TaskScheduler.SchedulePeriodicJob(new PrintJob(), "hi", new TimeSpan(0, 0, 1));
            Thread.Sleep(5000);
        }

        static void Execute(Object state, bool b)
        {
            ((IJob)((object[])state)[0]).Execute(((object[])state)[1]);
        }

        static void ScheduleDelayedJob(IJob job, object argument, TimeSpan delay)
        {
            ThreadPool.RegisterWaitForSingleObject(new EventWaitHandle(false, EventResetMode.AutoReset),
                Execute, new[] { job, argument }, delay, true);
        }

        static void SchedulePeriodicJob(IJob job, object argument, TimeSpan period)
        {
            ThreadPool.RegisterWaitForSingleObject(new EventWaitHandle(false, EventResetMode.AutoReset),
                Execute, new[] { job, argument }, period, false);
        }
    }
}
