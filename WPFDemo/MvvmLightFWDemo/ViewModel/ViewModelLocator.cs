/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MvvmLightFWDemo"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MvvmLightFWDemo.Dialog;
using MvvmLightFWDemo.Service;
using System;

namespace MvvmLightFWDemo.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            //依赖注入时，用来创建对象
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            //Ioc容器里注册了一个MainViewModel对象，此时并没有创建实例
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IService, Service.Service>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            
            //注册一个标识为key1的对象类型
            SimpleIoc.Default.Register<IService>(()=>new Service.Service(),"key1");
        }

        /// <summary>
        /// 从Ioc容器里面获取实例
        /// 只要一调用就会创建这个实例
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                //return ServiceLocator.Current.GetInstance<MainViewModel>();
                return ServiceLocator.Current.GetInstance<MainViewModel>(Guid.NewGuid().ToString());
            }
        }
        
        public static void Cleanup<T>() where T:ViewModelBase
        {
            // TODO Clear the ViewModels

            //统一进行资源释放
            ServiceLocator.Current.GetInstance<T>().Cleanup();
            SimpleIoc.Default.Unregister<MainViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }
    }
}