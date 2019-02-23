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

namespace _14.DefaultInterfaceMembers.Interfaces
{
    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01a_CSharp73_OLD_COMMON_INTERFACE
     *
     * Interface IAmDoingItWrong with one member:
     * - DoingSomething: () -> string
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01b_CSharp73_NEW_COMMON_INTERFACE
     * 
     * Interface IAmDoingItWrong with one old member and one additional:
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _01c_CSharp73_NEW_COMMON_INTERFACE
     * 
     * Interface IAmDoingItWrong with one old member and one additional:
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string
     */

    #region C#7.3 Common interface (who cares)

#if _01a_CSharp73_OLD_COMMON_INTERFACE

    public interface IAmDoingItWrong
    {
        string DoingSomething();
    }

#endif

#if _01b_CSharp73_NEW_COMMON_INTERFACE || _01c_CSharp73_FIXED_COMMON_INTERFACE

    public interface IAmDoingItWrong
    {
        string DoingSomething();

        string DoingSomething(string message);
    }

#endif

    #endregion C#7.3 Common interface (who cares)

    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02a_CSharp73_OLD_ABSTRACT_CLASSES
     *
     * Interface IAmDoingItWrong with one member:
     * - DoingSomething: () -> string
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02b_CSharp73_NEW_ABSTRACT_CLASSES
     * 
     * Interface IAmDoingItWrong with one old member and one additional:
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _02c_CSharp73_NEWER_ABSTRACT_CLASSES
     * 
     * Interface IAmDoingItWrong with one old member and one additional:
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string
     */

    #region C#7.3 Abstract classes (that's not my fault)

#if _02a_CSharp73_OLD_ABSTRACT_CLASSES

    public interface IAmDoingItWrong
    {
        string DoingSomething();
    }

#endif

#if _02b_CSharp73_NEW_ABSTRACT_CLASSES || _02c_CSharp73_NEWER_ABSTRACT_CLASSES

    public interface IAmDoingItWrong
    {
        string DoingSomething();

        string DoingSomething(string message);
    }

#endif

    #endregion C#7.3 Abstract classes (that's not my fault)

    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03a_CSharp73_OLD_INTERFACE_NAMESPACES
     *
     * Defines an interface IAmDoingItWrong (in a versioned v1 namespace) with one member:
     * - DoingSomething: () -> string
     * Also defines an interface IAmDoingItWrong (in a common namespace):
     * - DoingSomething: () -> string
     * that reflects the newest version of that interface.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03b_CSharp73_NEW_INTERFACE_NAMESPACES
     * 
     * Defines an interface IAmDoingItWrong (in a versioned v1 namespace) with one member:
     * - DoingSomething: () -> string
     * Also defines an interface IAmDoingItWrong (in a versioned v2 namespace) with one member:
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string
     * Also defines an interface IAmDoingItWrong (in a common namespace):
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string
     * that reflects the newest version of that interface.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03c_CSharp73_FIXED_INTERFACE_NAMESPACES
     * 
     * Defines an interface IAmDoingItWrong (in a versioned v1 namespace) with one member:
     * - DoingSomething: () -> string
     * Also defines an interface IAmDoingItWrong (in a versioned v2 namespace) with one member:
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string
     * Also defines an interface IAmDoingItWrong (in a common namespace):
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string
     * that reflects the newest version of that interface.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03d_CSharp73_NEWER_INTERFACE_NAMESPACES
     * 
     * Defines an interface IAmDoingItWrong (in a versioned v1 namespace) with one member:
     * - DoingSomething: () -> string
     * Also defines an interface IAmDoingItWrong (in a versioned v2 namespace) with one member:
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string
     * Also defines an interface IAmDoingItWrong (in a common namespace):
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string
     * that reflects the newest version of that interface.
     */

    #region C#7.3 Interface namespaces

#if _03a_CSharp73_OLD_INTERFACE_NAMESPACES

    namespace v1
    {
        public interface IAmDoingItWrong
        {
            string DoingSomething();
        }
    }

    public interface IAmDoingItWrong : v1.IAmDoingItWrong { }

#endif

#if _03b_CSharp73_NEW_INTERFACE_NAMESPACES || _03c_CSharp73_FIXED_INTERFACE_NAMESPACES || _03d_CSharp73_NEWER_INTERFACE_NAMESPACES

    namespace v1
    {
        public interface IAmDoingItWrong
        {
            string DoingSomething();
        }
    }

    namespace v2
    {
        public interface IAmDoingItWrong : v1.IAmDoingItWrong
        {
            string DoingSomething(string message);
        }
    }

    public interface IAmDoingItWrong : v2.IAmDoingItWrong { }

#endif

    #endregion C#7.3 Interface namespaces

    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 8.0 -DefineSection _04a_CSharp80_OLD_DEFAULT_MEMBERS
     *
     * Defines an interface IAmDoingItWrong with one member:
     * - DoingSomething: () -> string
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 8.0 -DefineSection _04b_CSharp80_NEW_DEFAULT_MEMBERS
     * 
     * Interface IAmDoingItWrong with one old member and one additional:
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string (default implementation)
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 8.0 -DefineSection _04c_CSharp80_NEWER_DEFAULT_MEMBERS
     * 
     * Interface IAmDoingItWrong with one old member and one additional:
     * - DoingSomething: () -> string
     * - DoingSomething: (string) -> string
     */

    #region C#8.0 Default members

#if _04a_CSharp80_OLD_DEFAULT_MEMBERS

    public interface IAmDoingItWrong
    {
        string DoingSomething();
    }

#endif

#if _04b_CSharp80_NEW_DEFAULT_MEMBERS

    public interface IAmDoingItWrong
    {
        string DoingSomething();

        string DoingSomething(string message) => $"Output from a DoingSomething method (with param: '{message}') " +
                $"of the IAmDoingItWrongImpl class (new interface).";
    }

#endif

#if _04c_CSharp80_NEWER_DEFAULT_MEMBERS

    public interface IAmDoingItWrong
    {
        string DoingSomething();

        string DoingSomething(string message);
    }

#endif

    #endregion C#8.0 Default members
}