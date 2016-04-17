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
            RegisteredWaitHandle handle1 = TaskScheduler.ScheduleDelayedJob(new PrintJob(), "hello", new TimeSpan(0, 0, 2));
            RegisteredWaitHandle handle2 = TaskScheduler.SchedulePeriodicJob(new PrintJob(), "hi", new TimeSpan(0, 0, 1));

            Thread.Sleep(5000);

            handle2.Unregister(null);
            Thread.Sleep(3000);
        }

        static void Execute(Object state, bool b)
        {
            ((IJob)((object[])state)[0]).Execute(((object[])state)[1]);
        }

        static RegisteredWaitHandle ScheduleDelayedJob(IJob job, object argument, TimeSpan delay)
        {
            return ThreadPool.RegisterWaitForSingleObject(new EventWaitHandle(false, EventResetMode.AutoReset),
                Execute, new[] { job, argument }, delay, true);
        }

        static RegisteredWaitHandle SchedulePeriodicJob(IJob job, object argument, TimeSpan period)
        {
            return ThreadPool.RegisterWaitForSingleObject(new EventWaitHandle(false, EventResetMode.AutoReset),
                Execute, new[] { job, argument }, period, false);
        }
    }
}
