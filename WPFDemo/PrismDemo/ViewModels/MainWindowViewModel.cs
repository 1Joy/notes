using Prism.Mvvm;
using Prism.Regions;
using PrismDemo.Views;

namespace PrismDemo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private readonly IRegionManager regionManager;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            this.regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewContent));
            this.regionManager.RegisterViewWithRegion("HeaderRegion", typeof(ViewHeader));
        }
    }
}
