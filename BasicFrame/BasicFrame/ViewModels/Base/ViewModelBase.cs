using BasicFrame.Services.Navigation;
using System;
using System.Threading.Tasks;

namespace BasicFrame.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected readonly INavigationService NavigationService;
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set 
            { 
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public static bool IsSupervisor { get; set; }

        public ViewModelBase()
        {
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("HH:mm:ss");
        }
    }
}
