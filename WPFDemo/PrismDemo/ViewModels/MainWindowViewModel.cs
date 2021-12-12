using Prism.Mvvm;
using Prism.Regions;
using PrismDemo.Views;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        private string _myBindingText;

        public string MyBindingText
        {
            get { return _myBindingText; }
            set
            {
                _myBindingText = value;
                RaisePropertyChanged();
            }
        }

        int index = 0;
        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            this.regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewContent));
            this.regionManager.RegisterViewWithRegion("HeaderRegion", typeof(ViewHeader));

            Action action = () =>
            {
                while (true)
                {
                    MyBindingText = "hello joy" + index++;
                    Thread.Sleep(3000);
                }
            };
            Task.Run(action);
            
        }
    }
}
