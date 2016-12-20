namespace VirtualInputHardware.UWP.ViewModels
{
    using System.Windows.Input;
    using VirtualInputHardware.UWP.Mvvm;

    public abstract class ViewModelBase : BindableBase
    {
        protected DelegateCommand commonCommand;
        private bool isCommonCommandExecutable = false;

        public ViewModelBase()
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            this.commonCommand = new DelegateCommand(this.Common_Executed, this.Common_CanExecute);
        }

        public ICommand CommonCommand => this.commonCommand;

        public bool IsInDesignMode => Windows.ApplicationModel.DesignMode.DesignModeEnabled;

        public bool IsCommonCommandExecutable
        {
            get
            {
                return this.isCommonCommandExecutable;
            }

            set
            {
                this.SetProperty(ref this.isCommonCommandExecutable, value);
                this.commonCommand.RaiseCanExecuteChanged();
            }
        }

        protected virtual bool Common_CanExecute()
        {
            return !this.IsCommonCommandExecutable;
        }

        protected virtual void Common_Executed()
        {
            this.IsCommonCommandExecutable = true;
        }
    }
}
