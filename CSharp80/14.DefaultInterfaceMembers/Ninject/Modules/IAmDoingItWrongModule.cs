using _14.DefaultInterfaceMembers.Interfaces;
using _14.DefaultInterfaceMembers.Library;
using Ninject.Modules;

namespace _14.DefaultInterfaceMembers.Ninject.Modules
{
    internal class IAmDoingItWrongModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAmDoingItWrong>().To<Program.IAmDoingItWrongImpl>();
            Bind<IWhatever>().To<IWhateverImpl>();
        }
    }
}