using BasicFrame.ViewModels.Base;
using System.Threading.Tasks;

namespace BasicFrame.Services.Navigation
{
    public interface INavigationService
    {
		ViewModelBase PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToModalAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToModalAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
    }
}