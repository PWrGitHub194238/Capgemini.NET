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
 * 
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

}