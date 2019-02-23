#region Define

#region C#7.3 Common interface (who cares)
// #define _01a_CSharp73_OLD_COMMON_INTERFACE
// #define _01b_CSharp73_NEW_COMMON_INTERFACE
// #define _01c_CSharp73_FIXED_COMMON_INTERFACE
#endregion C#7.3 Common interface (who cares)

#region C#7.3 Abstract classes (that's not my fault)
// #define _02a_CSharp73_OLD_ABSTRACT_CLASSES
// #define _02b_CSharp73_NEW_ABSTRACT_CLASSES
// #define _02c_CSharp73_NEWER_ABSTRACT_CLASSES
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
#define _04c_CSharp80_NEWER_DEFAULT_MEMBERS
#endregion C#8.0 Default members

#endregion Define

using _14.DefaultInterfaceMembers.Adapters;
using _14.DefaultInterfaceMembers.Interfaces;
using _14.DefaultInterfaceMembers.Library;
using Ninject.Modules;

namespace _14.DefaultInterfaceMembers.Ninject.Modules
{

    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01a_CSharp73_OLD_COMMON_INTERFACE
     *
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01b_CSharp73_NEW_COMMON_INTERFACE
     * 
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01c_CSharp73_NEW_COMMON_INTERFACE
     * 
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     */

    #region C#7.3 Common interface (who cares)

#if _01a_CSharp73_OLD_COMMON_INTERFACE

    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }

#endif

#if _01b_CSharp73_NEW_COMMON_INTERFACE || _01c_CSharp73_FIXED_COMMON_INTERFACE

    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }

#endif

    #endregion C#7.3 Common interface (who cares)

    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02a_CSharp73_OLD_ABSTRACT_CLASSES
     *
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02b_CSharp73_NEW_ABSTRACT_CLASSES
     * 
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02c_CSharp73_NEWER_ABSTRACT_CLASSES
     * 
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     */

    #region C#7.3 Abstract classes (that's not my fault)

#if _02a_CSharp73_OLD_ABSTRACT_CLASSES

    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }

#endif

#if _02b_CSharp73_NEW_ABSTRACT_CLASSES || _02c_CSharp73_NEWER_ABSTRACT_CLASSES

    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }

#endif

    #endregion C#7.3 Abstract classes (that's not my fault)

    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03a_CSharp73_OLD_INTERFACE_NAMESPACES
     *
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> IAmDoingItWrongV1Adapter
     * - Interfaces.v1.IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03b_CSharp73_NEW_INTERFACE_NAMESPACES
     * 
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> IAmDoingItWrongV1Adapter
     * - Interfaces.v1.IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03c_CSharp73_FIXED_INTERFACE_NAMESPACES
     * 
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> IAmDoingItWrongV1Adapter
     * - Interfaces.v1.IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03d_CSharp73_NEWER_INTERFACE_NAMESPACES
     * 
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> IAmDoingItWrongV2Adapter
     * - Interfaces.v2.IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     */

    #region C#7.3 Interface namespaces

#if _03a_CSharp73_OLD_INTERFACE_NAMESPACES

    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<IAmDoingItWrongAdapter>();
            Bind<Interfaces.v1.IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }

#endif

#if _03b_CSharp73_NEW_INTERFACE_NAMESPACES

    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<IAmDoingItWrongAdapter>();
            Bind<Interfaces.v1.IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }

#endif

#if _03c_CSharp73_FIXED_INTERFACE_NAMESPACES

    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<IAmDoingItWrongAdapter>();
            Bind<Interfaces.v1.IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }

#endif

#if _03d_CSharp73_NEWER_INTERFACE_NAMESPACES

    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<IAmDoingItWrongAdapter>();
            Bind<Interfaces.v2.IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }

#endif

    #endregion C#7.3 Interface namespaces

    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 8.0 -DefineSection _04a_CSharp80_OLD_DEFAULT_MEMBERS
     *
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 8.0 -DefineSection _04b_CSharp80_NEW_DEFAULT_MEMBERS
     * 
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 8.0 -DefineSection _04c_CSharp80_NEWER_DEFAULT_MEMBERS
     * 
     * Ninject will map the following interfaces to given types:
     * - IAmDoingItWrong -> Program.IAmDoingItWrongImpl
     * - IWhatever -> IWhateverImpl
     */

    #region C#8.0 Default members

#if _04a_CSharp80_OLD_DEFAULT_MEMBERS

    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }

#endif

#if _04b_CSharp80_NEW_DEFAULT_MEMBERS

    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }

#endif

#if _04c_CSharp80_NEWER_DEFAULT_MEMBERS

    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }

#endif

    #endregion C#8.0 Default members
}