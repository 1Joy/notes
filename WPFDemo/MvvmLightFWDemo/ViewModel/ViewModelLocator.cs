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
            //����ע��ʱ��������������
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

            //Ioc������ע����һ��MainViewModel���󣬴�ʱ��û�д���ʵ��
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IService, Service.Service>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            
            //ע��һ����ʶΪkey1�Ķ�������
            SimpleIoc.Default.Register<IService>(()=>new Service.Service(),"key1");
        }

        /// <summary>
        /// ��Ioc���������ȡʵ��
        /// ֻҪһ���þͻᴴ�����ʵ��
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

            //ͳһ������Դ�ͷ�
            ServiceLocator.Current.GetInstance<T>().Cleanup();
            SimpleIoc.Default.Unregister<MainViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }
    }
}