using MvvmPrismDemo.BLL;
using MvvmPrismDemo.ViewTest;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MvvmPrismDemo.ViewModelTest
{
    public class MainTestViewModel:BindableBase
    {
        [Dependency]
        public IUserBLL UserBLL { get; set; }

        private IEventAggregator _eventAggregator;

        [Dependency]
        public IDialogService _dialogService { get; set; }

        [Dependency]
        public IRegionManager _regionManager { get; set; }

        [Dependency]
        public IModuleManager _moduleManager { get; set; }

        public MainTestViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            //订阅事件
            _eventAggregator.GetEvent<MessageEvent>().Subscribe((data) =>
            {
                ///todo 订阅事件的响应的处理逻辑
            });

            _eventAggregator.GetEvent<MessageEvent>().Subscribe((data) =>
            {
                ///todo 订阅事件的响应的处理逻辑
            },ThreadOption.PublisherThread,false,data=>data.StartsWith("hello"));

            //将一个view显示到region的几种方式

            //第一种方式：AddToRegion()方式可以向region中添加新的view，但是不一定添加进去的view就能立即被显示，需要激活region里面要显示的页面，同时会将界面的内容进行缓存
            _regionManager.AddToRegion("MainContent", "RegionContentView");
            var region = _regionManager.Regions["MainContent"];
            var view = region.Views.Where(v=>v.GetType().Name== "RegionContentView").FirstOrDefault();
            region.Activate(view);

            //第二种方式：
            _moduleManager.LoadModule("SubModule"); //如果module的加载方式设置为按需加载，就需要显示的加载指定的模块，否则module不会生效
            _regionManager.RequestNavigate("MainContent", "RegionContentView");

            //第三种方式：
            //指定区域加载指定的内容
            _regionManager.RegisterViewWithRegion("MainContent", typeof(RegionContentView));
        }

        private void PublishEvent()
        {
            _eventAggregator.GetEvent<MessageEvent>().Publish("hello,发送消息事件");
        }

        private void ShowDialog()
        {
            _dialogService.ShowDialog("DialogContent");
            _dialogService.ShowDialog("DialogContent", null, null, "win1");

            DialogParameters param = new DialogParameters();
            param.Add("userName", "joy");
            param.Add("age",26);
            _dialogService.ShowDialog("DialogContent", param, null, "win1");
        }
    }

    /// <summary>
    /// 自定义消息事件
    /// </summary>
    public class MessageEvent : PubSubEvent<string>
    {

    }
}
