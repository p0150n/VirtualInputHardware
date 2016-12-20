namespace VirtualInputHardware.UWP.Mvvm
{
    using Autofac;
    using Config;
    using ViewModels;

    public static class ViewModelFactory
    {
        private static IContainer Container => App.Current?.Container ?? AutofacConfig.BuildContainer();

        public static MousePageViewModel MousePage => ViewModelFactory.Container.Resolve<MousePageViewModel>();

        public static ConnectionPageViewModel ConnectionPage => ViewModelFactory.Container.Resolve<ConnectionPageViewModel>();
    }
}
