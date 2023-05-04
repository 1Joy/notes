using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmPrismDemo.ViewModelTest
{
    [RegionMemberLifetime(KeepAlive =true)]
    public class RegionContentViewModel : IRegionMemberLifetime,IConfirmNavigationRequest,INavigationAware
    {
        /// <summary>
        /// true：保持view实例不被切换
        /// false：页面切换都是新的view实例
        /// </summary>
        public bool KeepAlive => true;

        /// <summary>
        /// 处理导航请求，是否允许离开当前界面
        /// 当从A视图跳转到B视图时，在A视图中进行跳转确认
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <param name="continuationCallback"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;
            if(MessageBox.Show("是否要进行跳转","提示",MessageBoxButton.YesNo)==MessageBoxResult.No) {
                result = false;
            }
            //如果不执行callback，就不会执行跳转
            continuationCallback(result);
        }

        /// <summary>
        /// 是否是导航目标
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //true：直接返回导航中的历史视图
            //false：返回一个新的视图
            return true;
        }

        /// <summary>
        /// 离开当前视图
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 导航到当前视图时回调，
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //获取导航传递的参数
             var value = navigationContext.Parameters["Name"];

            //获取导航日志
            journal = navigationContext.NavigationService.Journal;

            if (journal.CanGoBack)
            {
                //后退
                journal.GoBack();
            }
            if (journal.CanGoForward)
            {
                //前进
                journal.GoForward();
            }
        }

        IRegionNavigationJournal journal;
    }
}
