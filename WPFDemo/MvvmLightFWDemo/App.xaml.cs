using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmLightFWDemo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //初始化DispatcherHelper的实例，如果不执行这个操作，后续的所有DispatcherHelper操作都不会生效
            DispatcherHelper.Initialize();
        }
    }
}
