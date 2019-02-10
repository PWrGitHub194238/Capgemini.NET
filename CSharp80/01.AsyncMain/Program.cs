#region Define

#region C#7.0 Non-Async
// #define _01_CSharp70_NON_ASYNC
#endregion

#region C#7.0 Task
// #define _02_CSharp70_TASK
// #define _02_CSharp70_TASK_Why_there_is_no_output
// #define _02_CSharp70_TASK_Why_there_is_no_delay
#endregion

#region C#7.0 Async
// #define _03_CSharp70_ASYNC
// #define _03_CSharp70_ASYNC_Why_there_is_no_output
#endregion

#region C#7.0 Async Await
// #define _04_CSharp70_ASYNC_AWAIT
#endregion

#region C#7.1 Async
// #define _05_CSharp71_ASYNC
// #define _05_CSharp71_ASYNC_Why_there_is_no_output
#endregion

#region C#7.1 Async Await #1
// #define _06_CSharp71_ASYNC_AWAIT1
#endregion

#region C#7.1 Async Await #2
#define _07_CSharp71_ASYNC_AWAIT2
#endregion

#endregion

using _01.AsyncMain.System.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace _01.AsyncMain
{
    /* Execute-Example -ProjectName 01.AsyncMain -LangVersion 7.0 -DefineSection _01_CSharp70_NON_ASYNC
     *
     * Should be executed on a UI thread, actions ActionImpl1 and ActionImpl2 will be called one after another synchronously.
     * Total execution time should be a sum of each action's delay: 1000 + 1000.
     * Each console output is guaranteed to be printed.
     */

    #region C#7.0 Non-Async

#if _01_CSharp70_NON_ASYNC

    internal class Program
    {
        private delegate void Action(string param, int delay = 1000);

        #region C#7.0 Non-Async Impl

        private static void ActionImpl1(string param, int delay)
        {
            Thread.Sleep(delay);
            Console.WriteLine($"ActionImpl1(param: {param}, delay: {delay})");
        }

        private static void ActionImpl2(string param, int delay)
        {
            Thread.Sleep(delay);
            Console.WriteLine($"ActionImpl2(param: {param}, delay: {delay})");
        }

        #endregion C#7.0 Non-Async Impl

        private static void Execute(string param, Action action)
        {
            action(param);
        }

        private static void Main()
        {
            Execute("A", ActionImpl1);
            Execute("B", ActionImpl2);
        }

        /* Expected output:
         *
         * ActionImpl1(param: A, delay: 1000)
         * ActionImpl2(param: B, delay: 1000)
         *
         */
    }

#endif

    #endregion C#7.0 Non-Async

    /* Execute-Example -ProjectName 01.AsyncMain -LangVersion 7.0 -DefineSection _02_CSharp70_TASK
     *
     * Should be executed on threads, actions ActionImpl1 and ActionImpl2 will be called asynchronously.
     * Total execution time should not be linked to the execution time of the UI thread.
     * No console output is guaranteed to be printed (CSharp70_TASK_Why_there_is_no_output)
     * If the delay of both actions is set to 0 (!CSharp70_TASK_Why_there_is_no_delay), both output might be seen.
     */

    #region C#7.0 Task

#if _02_CSharp70_TASK

    internal class Program
    {
        private delegate void Action(string param, int delay = 0);

    #region C#7.0 Task Impl

        private static void ActionImpl1(string param, int delay = 2000)
        {
    #region Why there is no delay?

#if _02_CSharp70_TASK_Why_there_is_no_delay
            delay = 2000;
#endif

    #endregion Why there is no delay?

            Thread.Sleep(delay);
            Console.WriteLine($"ActionImpl1(param: {param}, delay: {delay})");
        }

        private static void ActionImpl2(string param, int delay) =>
            Console.WriteLine($"ActionImpl2(param: {param}, delay: {delay})");

    #endregion C#7.0 Task Impl

        private static void Execute(string param, Action action) => new Task(() => action(param)).Start();

        private static void Main()
        {
            Execute("A", ActionImpl1);
            Execute("B", ActionImpl2);

    #region Why there is no output?

#if _02_CSharp70_TASK_Why_there_is_no_output
            Thread.Sleep(2000);
            Console.WriteLine("Hey, at least wait for the output!");
#endif

    #endregion Why there is no output?
        }

        /* Expected output:
         *
         * ActionImpl2(param: B, delay: 0)
         * Hey, at least wait for the output!
         * ActionImpl1(param: A, delay: 2000)
         *
         */
    }

#endif

    #endregion C#7.0 Task

    /* Execute-Example -ProjectName 01.AsyncMain -LangVersion 7.0 -DefineSection _03_CSharp70_ASYNC
     *
     * Should be executed on threads, actions ActionImpl1 and ActionImpl2 will be called asynchronously.
     * Total execution time should not be linked to the execution time of the UI thread.
     * No console output is guaranteed to be printed (_03_CSharp70_ASYNC_Why_there_is_no_output).
     */

    #region C#7.0 Async

#if _03_CSharp70_ASYNC

    internal class Program
    {
        private delegate void Action(string param, int delay);

    #region C#7.0 Async Impl

        private static readonly Action ActionImpl1 =
            (param, delay) => Console.WriteLine($"ActionImpl1(param: {param}, delay: {delay})");

        private static readonly Action ActionImpl2 =
            (param, delay) => Console.WriteLine($"ActionImpl2(param: {param}, delay: {delay})");

    #endregion C#7.0 Async Impl

        private static async Task Execute(string param, Action action) => await Task.Run(() =>
        {
            int delay = new Random().Next(0, 20) * 500;
            Thread.Sleep(delay);
            action(param, delay);
        });

        private static void Main()
        {
            Execute("A", ActionImpl1).JustForgetAboutIt();
            Execute("B", ActionImpl2).JustForgetAboutIt();

    #region Why there is no output?

#if _03_CSharp70_ASYNC_Why_there_is_no_output
            Thread.Sleep(20 * 500);
            Console.WriteLine("Hey, at least wait for the output!");
#endif

    #endregion Why there is no output?
        }

        /* Expected output:
         *
         * ActionImpl2(param: B, delay: 2000)
         * ActionImpl1(param: A, delay: 5500)
         * Hey, at least wait for the output!
         *
         */
    }

#endif

    #endregion C#7.0 Async

    /* Execute-Example -ProjectName 01.AsyncMain -LangVersion 7.0 -DefineSection _04_CSharp70_ASYNC_AWAIT
     *
     * Should be executed on threads, actions ActionImpl1 and ActionImpl2 will be called asynchronously
     * but due to 'GetResult()' executed on each task's awaiter, program will wait
     * for each Task to be completed before moving to the next line.
     * Total execution time should be a sum of each action's random delay.
     * Each console output is guaranteed to be printed.
     */

    #region C#7.0 Async Await

#if _04_CSharp70_ASYNC_AWAIT

    internal class Program
    {
        private delegate void Action(string param, int delay);

    #region C#7.0 Async Await Impl

        private static readonly Action ActionImpl1 =
            (param, delay) => Console.WriteLine($"ActionImpl1(param: {param}, delay: {delay})");

        private static readonly Action ActionImpl2 =
            (param, delay) => Console.WriteLine($"ActionImpl2(param: {param}, delay: {delay})");

    #endregion C#7.0 Async Await Impl

        private static async Task Execute(string param, Action action) => await Task.Run(() =>
        {
            int delay = new Random().Next(0, 20) * 500;
            Thread.Sleep(delay);
            action(param, delay);
        });

        private static void Main()
        {
            Execute("A", ActionImpl1).GetAwaiter().GetResult();
            Execute("B", ActionImpl2).GetAwaiter().GetResult();
        }

        /* Expected output:
         *
         * ActionImpl2(param: B, delay: 2000)
         * ActionImpl1(param: A, delay: 5500)
         *
         */
    }

#endif

    #endregion C#7.0 Async Await

    /* Execute-Example -ProjectName 01.AsyncMain -LangVersion 7.1 -DefineSection _05_CSharp71_ASYNC
     *
     * Should be executed on threads, actions ActionImpl1 and ActionImpl2 will be called asynchronously.
     * Total execution time should not be linked to the execution time of the UI thread.
     * No console output is guaranteed to be printed (_05_CSharp71_ASYNC_Why_there_is_no_output).
     */

    #region C#7.1 Async

#if _05_CSharp71_ASYNC

    internal class Program
    {
        private delegate void Action(string param, int delay);

    #region C#7.1 Async-Impl

        private static readonly Action ActionImpl1 =
            (param, delay) => Console.WriteLine($"ActionImpl1(param: {param}, delay: {delay})");

        private static readonly Action ActionImpl2 =
            (param, delay) => Console.WriteLine($"ActionImpl2(param: {param}, delay: {delay})");

    #endregion C#7.1 Async-Impl

        private static async Task Execute(string param, Action action) => await Task.Run(() =>
        {
            int delay = new Random().Next(0, 20) * 500;
            Thread.Sleep(delay);
            action(param, delay);
        });

        public static async Task Main()
        {
            Execute("A", ActionImpl1).JustForgetAboutIt();
            Execute("B", ActionImpl2).JustForgetAboutIt();

    #region Why there is no output?

#if _05_CSharp71_ASYNC_Why_there_is_no_output
            await Task.Delay(20 * 500);
            Console.WriteLine("Hey, at least wait for the output!");
#endif

    #endregion Why there is no output?
        }

        /* Expected output:
         *
         * ActionImpl1(param: A, delay: 2500)
         * ActionImpl2(param: B, delay: 4000)
         * Hey, at least wait for the output!
         *
         */
    }

#endif

    #endregion C#7.1 Async

    /* Execute-Example -ProjectName 01.AsyncMain -LangVersion 7.1 -DefineSection _06_CSharp71_ASYNC_AWAIT1
     *
     * Should be executed on threads, actions ActionImpl1 and ActionImpl2 will be called asynchronously
     * but due to 'await' keyword, program will wait for each Task to be completed before moving to the next line.
     * Total execution time should be a sum of each action's random delay.
     * Each console output is guaranteed to be printed.
     */

    #region C#7.1 Async Await #1

#if _06_CSharp71_ASYNC_AWAIT1

    internal class Program
    {
        private delegate void Action(string param, int delay);

    #region C#7.1 Async Await #1 Impl

        private static readonly Action ActionImpl1 =
            (param, delay) => Console.WriteLine($"ActionImpl1(param: {param}, delay: {delay})");

        private static readonly Action ActionImpl2 =
            (param, delay) => Console.WriteLine($"ActionImpl2(param: {param}, delay: {delay})");

    #endregion C#7.1 Async Await #1 Impl

        private static async Task Execute(string param, Action action) => await Task.Run(() =>
        {
            int delay = new Random().Next(0, 20) * 500;
            Thread.Sleep(delay);
            action(param, delay);
        });

        public static async Task Main()
        {
            await Execute("A", ActionImpl1);
            await Execute("B", ActionImpl2);
        }

        /* Expected output:
         *
         * ActionImpl1(param: A, delay: 9000)
         * ActionImpl2(param: B, delay: 7500)
         *
         */
    }

#endif

    #endregion C#7.1 Async Await #1

    /* Execute-Example -ProjectName 01.AsyncMain -LangVersion 7.1 -DefineSection _07_CSharp71_ASYNC_AWAIT2
     *
     * Should be executed on threads, actions ActionImpl1 and ActionImpl2 will be called asynchronously.
     * Total execution time should not be linked to the execution time of the UI thread.
     * Each console output is guaranteed to be printed due to usage of the 'await' keyword
     * right before exit. It lets tasks to be synchronized with the UI thread.
     * The order of actions execution is bound to the 'await' order.
     */

    #region C#7.1 Async Await #2

#if _07_CSharp71_ASYNC_AWAIT2

    internal class Program
    {
        private delegate void Action(string param, int delay);

    #region C#7.1 Async Await #2 Impl

        private static readonly Action ActionImpl1 =
            (param, delay) => Console.WriteLine($"ActionImpl1(param: {param}, delay: {delay})");

        private static readonly Action ActionImpl2 =
            (param, delay) => Console.WriteLine($"ActionImpl2(param: {param}, delay: {delay})");

    #endregion C#7.1 Async Await #2 Impl

        private static async Task Execute(string param, Action action) => await Task.Run(() =>
        {
            int delay = new Random().Next(0, 20) * 500;
            Thread.Sleep(delay);
            action(param, delay);
        });

        public static async Task Main()
        {
            Task t1 = Execute("A", ActionImpl1);
            Task t2 = Execute("B", ActionImpl2);

            await t1;
            await t2;
        }

        /* Expected output:
         *
         * ActionImpl2(param: B, delay: 2000)
         * ActionImpl1(param: A, delay: 3500)
         *
         */
    }

#endif

    #endregion C#7.1 Async Await #2
}