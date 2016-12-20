namespace VirtualInputHardware.UWP.ViewModels
{
    using System;
    using Windows.Devices.Sensors;
    using Windows.UI.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Input;
    using Microsoft.AspNet.SignalR.Client;
    using Services.Contracts;

    public class MousePageViewModel : ViewModelBase
    {
        private readonly ISignalRConnectionService signalRConnectionService;
        private readonly Accelerometer accelerometer;
        private double mousePadSpeed = 2;
        private bool? isAirMouseEnabled = false;
        private bool preventNextLeftMouseMuttonClick = false;
        private bool preventNextRightMouseMuttonClick = false;

        public MousePageViewModel(ISignalRConnectionService signalRConnectionService,
            Accelerometer accelerometer) : base()
        {
            this.signalRConnectionService = signalRConnectionService;
            this.accelerometer = accelerometer;

            //TODO Extract method
            if (this.accelerometer != null)
            {
                uint minReportInterval = this.accelerometer.MinimumReportInterval;
                uint reportInterval = minReportInterval > 16 ? minReportInterval : 16;
                this.accelerometer.ReportInterval = reportInterval;
                this.accelerometer.ReadingChanged += AccelerometerOnReadingChanged;
            }
        }

        public double MousePadSpeed
        {
            get { return this.mousePadSpeed; }
            set { this.SetProperty(ref this.mousePadSpeed, value); }
        }

        public bool? IsAirMouseEnabled
        {
            get { return this.isAirMouseEnabled; }
            set { this.SetProperty(ref this.isAirMouseEnabled, value); }
        }

        public void LeftMouseButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!this.IsConnectionOk())
            {
                return;
            }

            if (this.preventNextLeftMouseMuttonClick)
            {
                this.preventNextLeftMouseMuttonClick = false;
                return;
            }

            this.LeftMouseClick();
        }

        public void LeftMouseButton_OnHolding(object sender, HoldingRoutedEventArgs e)
        {
            if (!this.IsConnectionOk())
            {
                return;
            }

            if (e.HoldingState == HoldingState.Started)
            {
                this.signalRConnectionService.MouseHubProxy.Invoke("LeftButtonDown");
            }
            else
            {
                this.signalRConnectionService.MouseHubProxy.Invoke("LeftButtonUp");
                this.preventNextLeftMouseMuttonClick = true;
            }
        }

        public void RightMouseButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!this.IsConnectionOk())
            {
                return;
            }

            if (this.preventNextRightMouseMuttonClick)
            {
                this.preventNextRightMouseMuttonClick = false;
                return;
            }

            this.RightMouseClick();
        }

        public void RightMouseButton_OnHolding(object sender, HoldingRoutedEventArgs e)
        {
            if (!this.IsConnectionOk())
            {
                return;
            }

            if (e.HoldingState == HoldingState.Started)
            {
                this.signalRConnectionService.MouseHubProxy.Invoke("RightButtonDown");
            }
            else
            {
                this.signalRConnectionService.MouseHubProxy.Invoke("RightButtonUp");
                this.preventNextRightMouseMuttonClick = true;
            }
        }

        public void MousePadRectangle_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            LeftMouseClick();
        }

        public void MousePadRectangle_OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (!this.IsConnectionOk())
            {
                return;
            }

            double speed = this.MousePadSpeed;
            int transisionX = (int)Math.Round(e.Delta.Translation.X * speed, 0);
            int transisionY = (int)Math.Round(e.Delta.Translation.Y * speed, 0);

            this.signalRConnectionService.MouseHubProxy.Invoke("MoveMouseByAsync", transisionX, transisionY);
        }
        
        private void AccelerometerOnReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            if (!this.IsConnectionOk() || this.IsAirMouseEnabled == false)
            {
                return;
            }

            double speed = this.MousePadSpeed;
            double airSpeed = 10;
            var accelerometerGap = 0.03;

            var xAcceleration = args.Reading.AccelerationX;
            int transisionX = 0;
            if (xAcceleration < -accelerometerGap || xAcceleration > accelerometerGap )
            {
                transisionX = (int)Math.Round(xAcceleration * speed * airSpeed, 0);
            }
            
            var yAcceleration = args.Reading.AccelerationY;
            int transisionY = 0;
            if (yAcceleration < -accelerometerGap || yAcceleration > accelerometerGap)
            {
                transisionY = (int)Math.Round(yAcceleration * speed * airSpeed, 0);
            }

            this.signalRConnectionService.MouseHubProxy.Invoke("MoveMouseByAsync", transisionX, transisionY);
        }

        private void LeftMouseClick()
        {
            if (!this.IsConnectionOk())
            {
                return;
            }

            this.signalRConnectionService.MouseHubProxy.Invoke("LeftButtonClick");
        }

        private void RightMouseClick()
        {
            if (!this.IsConnectionOk())
            {
                return;
            }

            this.signalRConnectionService.MouseHubProxy.Invoke("RightButtonClick");
        }

        private bool IsConnectionOk()
        {
            if (this.signalRConnectionService.Connection?.State != ConnectionState.Connected ||
                this.signalRConnectionService.Connection == null)
            {
                return false;
            }

            return true;
        }
    }
}

