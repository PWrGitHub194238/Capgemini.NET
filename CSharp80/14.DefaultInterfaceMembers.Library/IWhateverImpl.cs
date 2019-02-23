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

namespace _14.DefaultInterfaceMembers.Library
{
    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01a_CSharp73_OLD_COMMON_INTERFACE
     *
     * Library class implementing IWhatever interface with one member:
     * - DoStuff: () -> string
     * which is using the IAmDoingItWrong interface with one member:
     * - DoingSomething: void -> ()
     * Implementation of IWhatever is using the old member of the interface.
     * 
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01b_CSharp73_NEW_COMMON_INTERFACE
     * 
     * Library class implementing IWhatever interface with one member:
     * - DoStuff: () -> string
     * which is using the IAmDoingItWrong interface with one old member and one additional:
     * - DoingSomething: void -> ()
     * - DoingSomething: void -> (string message)
     * Implementation of IWhatever is using the new member of the interface.
     * 
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01c_CSharp73_NEW_COMMON_INTERFACE
     * 
     * Library class implementing IWhatever interface with one member:
     * - DoStuff: () -> string
     * which is using the IAmDoingItWrong interface with one old member and one additional:
     * - DoingSomething: void -> ()
     * - DoingSomething: void -> (string message).
     * Implementation of IWhatever is using the new member of the interface.
     */

    #region C#7.3 Common interface (who cares)

#if _01a_CSharp73_OLD_COMMON_INTERFACE

    public class IWhateverImpl : IWhatever
    {
        readonly IAmDoingItWrong stuff;

        public IWhateverImpl(IAmDoingItWrong stuff) => this.stuff = stuff;

        public string DoStuff() => stuff.DoingSomething();
    }

#endif

#if _01b_CSharp73_NEW_COMMON_INTERFACE || _01c_CSharp73_FIXED_COMMON_INTERFACE

    public class IWhateverImpl : IWhatever
    {
        readonly IAmDoingItWrong stuff;

        public IWhateverImpl(IAmDoingItWrong stuff) => this.stuff = stuff;

        public string DoStuff() => stuff.DoingSomething("stuff");
    }

#endif

    #endregion C#7.3 Common interface (who cares)

    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02a_CSharp73_OLD_ABSTRACT_CLASSES
     *
     * Library class implementing IWhatever interface with one member:
     * - DoStuff: () -> string
     * which is using the IAmDoingItWrong interface with one member:
     * - DoingSomething: void -> ()
     * Implementation of IWhatever is using the old member of the interface.
     * Additionally, library provides an abstract implementation of IAmDoingItWrong interface.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02b_CSharp73_NEW_ABSTRACT_CLASSES
     * 
     * Library class implementing IWhatever interface with one member:
     * - DoStuff: () -> string
     * which is using the IAmDoingItWrong interface with one old member and one additional:
     * - DoingSomething: void -> ()
     * - DoingSomething: void -> (string message).
     * Implementation of IWhatever is using the new member of the interface.
     * Additionally, library provides an abstract implementation of IAmDoingItWrong interface.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02c_CSharp73_NEWER_ABSTRACT_CLASSES
     * 
     * Library class implementing IWhatever interface with one member:
     * - DoStuff: () -> string
     * which is using the IAmDoingItWrong interface with one old member and one additional:
     * - DoingSomething: void -> ()
     * - DoingSomething: void -> (string message).
     * Implementation of IWhatever is using the new member of the interface.
     * Additionally, library provides an abstract implementation of IAmDoingItWrong interface.
     */

    #region C#7.3 Abstract classes (that's not my fault)

#if _02a_CSharp73_OLD_ABSTRACT_CLASSES

    public class IWhateverImpl : IWhatever
    {
        readonly IAmDoingItWrong stuff;

        public IWhateverImpl(IAmDoingItWrong stuff) => this.stuff = stuff;

        public string DoStuff() => stuff.DoingSomething();
    }

    public abstract class IAmDoingItWrongImpl : IAmDoingItWrong
    {
        public abstract string DoingSomething();
    }

#endif

#if _02b_CSharp73_NEW_ABSTRACT_CLASSES || _02c_CSharp73_NEWER_ABSTRACT_CLASSES

    public class IWhateverImpl : IWhatever
    {
        readonly IAmDoingItWrong stuff;

        public IWhateverImpl(IAmDoingItWrong stuff) => this.stuff = stuff;

        public string DoStuff() => stuff.DoingSomething("stuff");
    }

    public abstract class IAmDoingItWrongImpl : IAmDoingItWrong
    {
        public abstract string DoingSomething();

        public virtual string DoingSomething(string message) => DoingSomething();
    }

#endif

    #endregion C#7.3 Abstract classes (that's not my fault)

    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03a_CSharp73_OLD_INTERFACE_NAMESPACES
     *
     * Library class implementing IWhatever interface with one member:
     * - DoStuff: () -> string
     * which is using the IAmDoingItWrong interface with one member:
     * - DoingSomething: void -> ()
     * Implementation of IWhatever is using the old member of the interface.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03b_CSharp73_NEW_INTERFACE_NAMESPACES
     * 
     * Library class implementing IWhatever interface with one member:
     * - DoStuff: () -> string
     * which is using the IAmDoingItWrong interface with one member:
     * - DoingSomething: void -> ()
     * Implementation of IWhatever is using the old member of the interface.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03c_CSharp73_FIXED_INTERFACE_NAMESPACES
     * 
     * Library class implementing IWhatever interface with one member:
     * - DoStuff: () -> string
     * which is using the IAmDoingItWrong interface with one member:
     * - DoingSomething: void -> ()
     * Implementation of IWhatever is using the old member of the interface.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03d_CSharp73_NEWER_INTERFACE_NAMESPACES
     * 
     * Library class implementing IWhatever interface with one member:
     * - DoStuff: () -> string
     * which is using the IAmDoingItWrong interface with one member:
     * - DoingSomething: void -> ()
     * Implementation of IWhatever is using the old member of the interface.
     */

    #region C#7.3 Interface namespaces

#if _03a_CSharp73_OLD_INTERFACE_NAMESPACES

    public class IWhateverImpl : IWhatever
    {
        readonly IAmDoingItWrong stuff;

        public IWhateverImpl(IAmDoingItWrong stuff) => this.stuff = stuff;

        public string DoStuff() => stuff.DoingSomething();
    }

#endif

#if _03b_CSharp73_NEW_INTERFACE_NAMESPACES || _03c_CSharp73_FIXED_INTERFACE_NAMESPACES || _03d_CSharp73_NEWER_INTERFACE_NAMESPACES

    public class IWhateverImpl : IWhatever
    {
        readonly IAmDoingItWrong stuff;

        public IWhateverImpl(IAmDoingItWrong stuff) => this.stuff = stuff;

        public string DoStuff() => stuff.DoingSomething("stuff");
    }

#endif

    #endregion C#7.3 Interface namespaces
}