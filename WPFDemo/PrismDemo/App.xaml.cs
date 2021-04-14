using Prism.Ioc;
using Prism.Regions;
using PrismDemo.RegionAdapters;
using PrismDemo.Views;
using System.Windows;
using System.Windows.Controls;

namespace PrismDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);

            //注册新自定义的区域适配器
            //第一个参数是区域适配器是作用于哪个控件的
            //第二个参数是新增的区域适配器类
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
        }
    }
}
