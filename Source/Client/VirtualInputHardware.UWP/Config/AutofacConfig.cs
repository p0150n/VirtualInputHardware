namespace VirtualInputHardware.UWP.Config
{
    using Windows.Devices.Sensors;
    using Windows.UI.Core;
    using Autofac;
    using Services;
    using Services.Contracts;
    using ViewModels;

    public static class AutofacConfig
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.Register<Windows.UI.Core.CoreDispatcher>(x => CoreWindow.GetForCurrentThread().Dispatcher).AsSelf().InstancePerDependency();
            builder.Register<Accelerometer>(x => Accelerometer.GetDefault(AccelerometerReadingType.Standard)).InstancePerDependency();
            builder.RegisterType<MousePageViewModel>().AsSelf().InstancePerDependency();
            builder.RegisterType<ConnectionPageViewModel>().AsSelf().InstancePerDependency();

            builder.RegisterType<SignalRConnectionService>().As<ISignalRConnectionService>().SingleInstance();
            
            return builder.Build();
        }
    }
}
