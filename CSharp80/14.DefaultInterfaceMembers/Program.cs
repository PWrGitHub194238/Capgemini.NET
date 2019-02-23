#region Define

#region C#7.3 Common interface (who cares)
// #define _01a_CSharp73_OLD_COMMON_INTERFACE
// #define _01b_CSharp73_NEW_COMMON_INTERFACE
#define _01c_CSharp73_FIXED_COMMON_INTERFACE
#endregion C#7.3 Common interface (who cares)

#region C#7.3 Abstract classes (that's not my fault)
// #define _02a_CSharp73_OLD_ABSTRACT_CLASSES
// #define _02b_CSharp73_NEW_ABSTRACT_CLASSES
// #define _02c_CSharp73_FIXED_ABSTRACT_CLASSES
#endregion C#7.3 Common interface (that's not my fault)

#region C#7.3 Namespace change
// #define _03a_CSharp73_OLD_NAMESPACE_CHANGE
// #define _03b_CSharp73_NEW_NAMESPACE_CHANGE
// #define _03c_CSharp73_NEWER_NAMESPACE_CHANGE
#endregion C#7.3 Namespace change

#region C#8.0 Default members
// #define _04a_CSharp80_OLD_DEFAULT_MEMBERS
// #define _04b_CSharp80_NEW_DEFAULT_MEMBERS
#endregion C#8.0 Default members

#endregion Define

using _14.DefaultInterfaceMembers.Interfaces;
using _14.DefaultInterfaceMembers.Ninject.Modules;
using Ninject;
using Ninject.Modules;
using System;

namespace _14.DefaultInterfaceMembers
{
    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01a_CSharp73_OLD_COMMON_INTERFACE
     *
     * Prints 'Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).'.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01b_CSharp73_NEW_COMMON_INTERFACE
     * 
     * Project will not even compile due to the:
     * - CS0535 C# does not implement interface member.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01c_CSharp73_FIXED_COMMON_INTERFACE
     * 
     * Prints 'Output from a DoingSomething method (with param: 'stuff') of the IAmDoingItWrongImpl class (new interface).'
     */

    #region C#7.3 Common interface (who cares)

#if _01a_CSharp73_OLD_COMMON_INTERFACE

    internal class Program
    {

    #region C#7.3 Common interface impl (who cares)

        private static readonly IKernel ninject = new StandardKernel(new INinjectModule[] {
            new IAmDoingItWrongModule()
        });

        private static readonly IWhatever impl = ninject.Get<IWhatever>();

        public class IAmDoingItWrongImpl : IAmDoingItWrong
        {
            public string DoingSomething() => "Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).";
        }

    #endregion C#7.3 Common interface impl (who cares)

        private static void Main()
        {
            Console.WriteLine(impl.DoStuff());
        }

        /* Expected output:
         *
         * Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).
         *
         */
    }

#endif

#if _01b_CSharp73_NEW_COMMON_INTERFACE

    internal class Program
    {

        #region C#7.3 Common interface impl (who cares)

        private static readonly IKernel ninject = new StandardKernel(new INinjectModule[] {
            new IAmDoingItWrongModule()
        });

        private static readonly IWhatever impl = ninject.Get<IWhatever>();

        public class IAmDoingItWrongImpl : IAmDoingItWrong
        {
            public string DoingSomething() => "Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).";
        }

        #endregion C#7.3 Common interface impl (who cares)

        private static void Main()
        {
            Console.WriteLine(impl.DoStuff());
        }

        /* Expected output:
         *
         *
         *
         */
    }

#endif

#if _01c_CSharp73_FIXED_COMMON_INTERFACE

    internal class Program
    {

    #region C#7.3 Common interface impl (who cares)

        private static readonly IKernel ninject = new StandardKernel(new INinjectModule[] {
            new IAmDoingItWrongModule()
        });

        private static readonly IWhatever impl = ninject.Get<IWhatever>();

        public class IAmDoingItWrongImpl : IAmDoingItWrong
        {
            public string DoingSomething() => "Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).";

            public string DoingSomething(string message) => $"Output from a DoingSomething method (with param: '{message}') " +
                $"of the IAmDoingItWrongImpl class (new interface).";
        }

    #endregion C#7.3 Common interface impl (who cares)

        private static void Main()
        {
            Console.WriteLine(impl.DoStuff());
        }

        /* Expected output:
         *
         * Output from a DoingSomething method (with param: 'stuff') of the IAmDoingItWrongImpl class (new interface).
         *
         */
    }

#endif

    #endregion C#7.3 Common interface (who cares)

    /* Execute-Example -ProjectName 00.Example -LangVersion 7.1 -DefineSection _02_CSharp71_EXAMPLE
     *
     * Prints 'Hello Word from C# 7.1!'.
     */

    #region C#7.1 Example

#if _02_CSharp71_EXAMPLE

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello Word from C# 7.1!");
        }

        /* Expected output:
         *
         * Hello Word from C# 7.1!
         *
         */
    }

#endif

    #endregion C#7.0 Example
}