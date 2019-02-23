#region Define

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

namespace _14.DefaultInterfaceMembers.Adapters
{
    /* Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03a_CSharp73_OLD_INTERFACE_NAMESPACES
     *
     * Implements the adapter for v1.IAmDoingItWrong interface.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03b_CSharp73_NEW_INTERFACE_NAMESPACES
     * 
     * Implements the adapter for OLD v1.IAmDoingItWrong interface.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03c_CSharp73_FIXED_INTERFACE_NAMESPACES
     * 
     * Implements the adapter for OLD v1.IAmDoingItWrong interface.
     * 
     * Execute-Example -ProjectName 14.DefaultInterfaceMembers -LangVersion 7.3 -DefineSection _03d_CSharp73_NEWER_INTERFACE_NAMESPACES
     * 
     * Implements the adapter for new v2.IAmDoingItWrong interface.
     */

    #region C#7.3 Interface namespaces

#if _03a_CSharp73_OLD_INTERFACE_NAMESPACES

    internal class IAmDoingItWrongAdapter : IAmDoingItWrong
    {
        private readonly Interfaces.v1.IAmDoingItWrong adaptee;

        public IAmDoingItWrongAdapter(Interfaces.v1.IAmDoingItWrong adaptee) => this.adaptee = adaptee;

        public virtual string DoingSomething() => adaptee.DoingSomething();
    }

#endif

#if _03b_CSharp73_NEW_INTERFACE_NAMESPACES

    internal class IAmDoingItWrongAdapter : IAmDoingItWrong
    {
        private readonly Interfaces.v1.IAmDoingItWrong adaptee;

        public IAmDoingItWrongAdapter(Interfaces.v1.IAmDoingItWrong adaptee) => this.adaptee = adaptee;

        public virtual string DoingSomething() => adaptee.DoingSomething();
    }

#endif

#if _03c_CSharp73_FIXED_INTERFACE_NAMESPACES

    internal class IAmDoingItWrongAdapter : IAmDoingItWrong
    {
        private readonly Interfaces.v1.IAmDoingItWrong adaptee;

        public IAmDoingItWrongAdapter(Interfaces.v1.IAmDoingItWrong adaptee) => this.adaptee = adaptee;

        public virtual string DoingSomething() => adaptee.DoingSomething();

        public virtual string DoingSomething(string message) => DoingSomething();
    }

#endif

#if _03d_CSharp73_NEWER_INTERFACE_NAMESPACES

    internal class IAmDoingItWrongAdapter : IAmDoingItWrong
    {
        private readonly Interfaces.v2.IAmDoingItWrong adaptee;

        public IAmDoingItWrongAdapter(Interfaces.v2.IAmDoingItWrong adaptee) => this.adaptee = adaptee;

        public virtual string DoingSomething() => adaptee.DoingSomething();

        public virtual string DoingSomething(string message) => adaptee.DoingSomething(message);
    }

#endif

    #endregion C#7.3 Interface namespaces
}