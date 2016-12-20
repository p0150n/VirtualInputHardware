namespace VirtualInputHardware.Web.Hubs
{
    using System;
    using System.Threading.Tasks;
    using WindowsInput;
    using Microsoft.AspNetCore.SignalR;

    public class MouseHub : Hub<IMouseHub>
    {
        private static IMouseSimulator _mouseSimulator;

        public MouseHub(IMouseSimulator mouseSimulator)
        {
            if (mouseSimulator == null)
            {
                throw new NullReferenceException("mouseSimulator");
            }

            _mouseSimulator = mouseSimulator;
        }

        public MouseHub() : this(_mouseSimulator ?? new WindowsInput.InputSimulator().Mouse)
        {
        }

        public IKeyboardSimulator Keyboard => this.MouseSimulator.Keyboard;

        private IMouseSimulator MouseSimulator => _mouseSimulator;

        public void MoveMouseBy(int pixelDeltaX, int pixelDeltaY)
        {
            this.MouseSimulator.MoveMouseBy(pixelDeltaX, pixelDeltaY);
        }

        public async void MoveMouseByAsync(int pixelDeltaX, int pixelDeltaY)
        {
            await Task.Run(() =>
            {
                this.MouseSimulator.MoveMouseBy(pixelDeltaX, pixelDeltaY);
            });
        }

        public void MoveMouseTo(double absoluteX, double absoluteY)
        {
            this.MouseSimulator.MoveMouseTo(absoluteX, absoluteY);
        }

        public void MoveMouseToPositionOnVirtualDesktop(double absoluteX, double absoluteY)
        {
            this.MouseSimulator.MoveMouseToPositionOnVirtualDesktop(absoluteX, absoluteY);
        }

        public void LeftButtonDown()
        {
            this.MouseSimulator.LeftButtonDown();
        }

        public void LeftButtonUp()
        {
            this.MouseSimulator.LeftButtonUp();
        }

        public void LeftButtonClick()
        {
            this.MouseSimulator.LeftButtonClick();
        }

        public void LeftButtonDoubleClick()
        {
            this.MouseSimulator.LeftButtonDoubleClick();
        }

        public void RightButtonDown()
        {
            this.MouseSimulator.RightButtonDown();
        }

        public void RightButtonUp()
        {
            this.MouseSimulator.RightButtonUp();
        }

        public void RightButtonClick()
        {
            this.MouseSimulator.RightButtonClick();
        }

        public void RightButtonDoubleClick()
        {
            this.MouseSimulator.RightButtonDoubleClick();
        }

        public void XButtonDown(int buttonId)
        {
            this.MouseSimulator.XButtonDown(buttonId);
        }

        public void XButtonUp(int buttonId)
        {
            this.MouseSimulator.XButtonUp(buttonId);
        }

        public void XButtonClick(int buttonId)
        {
            this.MouseSimulator.XButtonClick(buttonId);
        }

        public void XButtonDoubleClick(int buttonId)
        {
            this.MouseSimulator.XButtonDoubleClick(buttonId);
        }

        public void VerticalScroll(int scrollAmountInClicks)
        {
            this.MouseSimulator.VerticalScroll(scrollAmountInClicks);
        }

        public void HorizontalScroll(int scrollAmountInClicks)
        {
            this.MouseSimulator.HorizontalScroll(scrollAmountInClicks);
        }

        public void Sleep(int millsecondsTimeout)
        {
            this.MouseSimulator.Sleep(millsecondsTimeout);
        }

        public void Sleep(TimeSpan timeout)
        {
            this.MouseSimulator.Sleep(timeout);
        }
    }
}
