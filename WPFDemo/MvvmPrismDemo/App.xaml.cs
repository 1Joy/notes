using MvvmPrismDemo.BLL;
using MvvmPrismDemo.Module1;
using MvvmPrismDemo.ViewModelTest;
using MvvmPrismDemo.Views;
using MvvmPrismDemo.ViewTest;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MvvmPrismDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            //new Bootstrapper().Run();
        }

        protected override Window CreateShell()
        {
            //创建MainWindow的实例，进行窗口显示
            //通过IOC容器负责创建对象，可以对所需的对象进行统一管理，并且所需要的其他对象可自动进行创建
            return Container.Resolve<MainTest>();
        }

        /// <summary>
        /// 初始化主窗口的时候执行
        /// </summary>
        /// <param name="shell"></param>
        protected override void InitializeShell(Window shell)
        {
            //初始化主窗体前可能需要登录
            var loginWin = Container.Resolve<LoginWindow>();
            if(loginWin == null||loginWin.ShowDialog()!=true)
            {
                //如果没有强行退出，依旧会进入主窗口
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// ioc容器的对象注册
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册IUserBLL的具体实现为UserBLL
            containerRegistry.Register<IUserBLL, UserBLL>();

            //注册dialog父窗口实现
            containerRegistry.RegisterDialogWindow<DialogWindowBase>();

            //注册弹窗
            containerRegistry.RegisterDialog<DialogContent>(nameof(DialogContent));


            //注册区域
            containerRegistry.RegisterForNavigation<RegionContentView>(nameof(RegionContentView));
        }

        /// <summary>
        /// 配置module
        /// </summary>
        /// <param name="moduleCatalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //第一种加载module方式
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<SubModule>();

            //第二种方式：按需加载
            Type type = typeof(SubModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = type.Name,
                ModuleType = type.AssemblyQualifiedName,
                InitializationMode = InitializationMode.OnDemand ,//按需加载module

            });

            //第三种方式：扫描文件
        }

        /// <summary>
        /// 配置ViewModelLocator 的匹配规则
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(ViewToViewModelResolve);

            //临时注册一个view到ViewModel的关系

            //第一种方式：类型/类型
            ViewModelLocationProvider.Register(typeof(MainWindow).ToString(), typeof(MainTestViewModel));

            //第二种方式：类型/工厂
            ViewModelLocationProvider.Register(typeof(MainWindow).ToString(), () => Container.Resolve<MainTestViewModel>());

            //第三种方式：
            ViewModelLocationProvider.Register<MainWindow>(() => Container.Resolve<MainTestViewModel>());

            //第四种方式
            ViewModelLocationProvider.Register<MainWindow, MainTestViewModel>();
        }

        /// <summary>
        /// 具体类型转换匹配实现
        /// </summary>
        /// <param name="type">需要进行匹配的View的类型</param>
        /// <returns>需要匹配View的ViewModel的类型</returns>
        private Type ViewToViewModelResolve(Type type)
        {
            //在此处修改viewmodel的匹配规则
            string viewName = type.FullName;
            string viewModelName = viewName;
            if (viewName.Contains(".ViewTest."))
            {
                viewModelName = viewModelName.Replace(".ViewTest.", ".ViewModelTest.");
            }
            if (viewModelName.EndsWith("View"))
            {
                viewModelName += "Model";
            }
            else 
            {
                viewModelName += "ViewModel";
            }
            return Type.GetType(viewModelName);
        }


        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);

            regionAdapterMappings.RegisterMapping<Canvas>(Container.Resolve<CanvasRegionAdapter>());
        }
    }
}
