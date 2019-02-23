#region Define

#region C#7.3 Common interface (who cares)
// #define _01a_CSharp73_OLD_COMMON_INTERFACE
// #define _01b_CSharp73_NEW_COMMON_INTERFACE
// #define _01c_CSharp73_FIXED_COMMON_INTERFACE
#endregion C#7.3 Common interface (who cares)

#region C#7.3 Abstract classes (that's not my fault)
// #define _02a_CSharp73_OLD_ABSTRACT_CLASSES
// #define _02b_CSharp73_NEW_ABSTRACT_CLASSES
#define _02c_CSharp73_NEWER_ABSTRACT_CLASSES
#endregion C#7.3 Common interface (that's not my fault)

#region C#7.3 Interface namespaces
// #define _03a_CSharp73_OLD_INTERFACE_NAMESPACES
// #define _03b_CSharp73_NEW_INTERFACE_NAMESPACES
// #define _03c_CSharp73_FIXED_INTERFACE_NAMESPACES
// #define _03d_CSharp73_NEWER_INTERFACE_NAMESPACES
#endregion C#7.3 Interface namespaces

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

    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02a_CSharp73_OLD_ABSTRACT_CLASSES
     *
     * Prints 'Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).'.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02b_CSharp73_NEW_ABSTRACT_CLASSES
     * 
     * Prints 'Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).'.
     * Example neither fails to compile nor prints the expected result.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02b_CSharp73_NEW_ABSTRACT_CLASSES
     * 
     * Prints 'Output from a DoingSomething method (with param: 'stuff') of the IAmDoingItWrongImpl class (new interface).'.
     */

    #region Abstract classes (that's not my fault)

#if _02a_CSharp73_OLD_ABSTRACT_CLASSES

    internal class Program
    {

    #region C#7.3 Common interface impl (who cares)

        private static readonly IKernel ninject = new StandardKernel(new INinjectModule[] {
            new IAmDoingItWrongModule()
        });

        private static readonly IWhatever impl = ninject.Get<IWhatever>();

        public class IAmDoingItWrongImpl : Library.IAmDoingItWrongImpl
        {
            public override string DoingSomething() => "Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).";
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

#if _02b_CSharp73_NEW_ABSTRACT_CLASSES

    internal class Program
    {

    #region C#7.3 Abstract classes impl (that's not my fault)

        private static readonly IKernel ninject = new StandardKernel(new INinjectModule[] {
            new IAmDoingItWrongModule()
        });

        private static readonly IWhatever impl = ninject.Get<IWhatever>();

        public class IAmDoingItWrongImpl : Library.IAmDoingItWrongImpl
        {
            public override string DoingSomething() => "Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).";
        }

    #endregion C#7.3 Abstract classes impl (that's not my fault)

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

#if _02c_CSharp73_NEWER_ABSTRACT_CLASSES

    internal class Program
    {

    #region C#7.3 Abstract classes impl (that's not my fault)

        private static readonly IKernel ninject = new StandardKernel(new INinjectModule[] {
            new IAmDoingItWrongModule()
        });

        private static readonly IWhatever impl = ninject.Get<IWhatever>();

        public class IAmDoingItWrongImpl : Library.IAmDoingItWrongImpl
        {
            public override string DoingSomething() => "Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).";

            public override string DoingSomething(string message) => $"Output from a DoingSomething method (with param: '{message}') " +
                $"of the IAmDoingItWrongImpl class (new interface).";
        }

    #endregion C#7.3 Abstract classes impl (that's not my fault)

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

    #endregion Abstract classes (that's not my fault)

    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03a_CSharp73_OLD_INTERFACE_NAMESPACES
     *
     * Prints 'Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).'.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03b_CSharp73_NEW_INTERFACE_NAMESPACES
     * 
     * Project will not even compile due to the:
     * - CS0535 C# does not implement interface member
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03c_CSharp73_FIXED_INTERFACE_NAMESPACES
     * 
     * Prints 'Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).'.
     * Example neither fails to compile nor prints the expected result.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03d_CSharp73_NEWER_INTERFACE_NAMESPACES
     * 
     * Prints 'Output from a DoingSomething method (with param: 'stuff') of the IAmDoingItWrongImpl class (new interface).'.
     */

    #region C#7.3 Interface namespaces

#if _03a_CSharp73_OLD_INTERFACE_NAMESPACES

    internal class Program
    {

    #region C#7.3 Interface namespaces impl

        private static readonly IKernel ninject = new StandardKernel(new INinjectModule[] {
            new IAmDoingItWrongModule()
        });

        private static readonly IWhatever impl = ninject.Get<IWhatever>();

        public class IAmDoingItWrongImpl : Interfaces.v1.IAmDoingItWrong
        {
            public string DoingSomething() => "Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).";
        }

    #endregion C#7.3 Interface namespaces impl

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

#if _03b_CSharp73_NEW_INTERFACE_NAMESPACES

    internal class Program
    {

    #region C#7.3 Interface namespaces impl

        private static readonly IKernel ninject = new StandardKernel(new INinjectModule[] {
            new IAmDoingItWrongModule()
        });

        private static readonly IWhatever impl = ninject.Get<IWhatever>();

        public class IAmDoingItWrongImpl : Interfaces.v1.IAmDoingItWrong
        {
            public string DoingSomething() => "Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).";
        }

    #endregion C#7.3 Interface namespaces impl

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

#if _03c_CSharp73_FIXED_INTERFACE_NAMESPACES

    internal class Program
    {

        #region C#7.3 Interface namespaces impl

        private static readonly IKernel ninject = new StandardKernel(new INinjectModule[] {
            new IAmDoingItWrongModule()
        });

        private static readonly IWhatever impl = ninject.Get<IWhatever>();

        public class IAmDoingItWrongImpl : Interfaces.v1.IAmDoingItWrong
        {
            public string DoingSomething() => "Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).";
        }

        #endregion C#7.3 Interface namespaces impl

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

#if _03d_CSharp73_NEWER_INTERFACE_NAMESPACES

    internal class Program
    {

    #region C#7.3 Interface namespaces impl

        private static readonly IKernel ninject = new StandardKernel(new INinjectModule[] {
            new IAmDoingItWrongModule()
        });

        private static readonly IWhatever impl = ninject.Get<IWhatever>();

        public class IAmDoingItWrongImpl : Interfaces.v2.IAmDoingItWrong
        {
            public string DoingSomething() => "Output from a DoingSomething method of the IAmDoingItWrongImpl class (old interface).";

            public string DoingSomething(string message) => $"Output from a DoingSomething method (with param: '{message}') " +
                $"of the IAmDoingItWrongImpl class (new interface).";
        }

    #endregion C#7.3 Interface namespaces impl

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

    #endregion C#7.3 Interface namespaces
}