using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmPrismDemo.ViewModelTest
{
    public class DialogContentViewModel : IDialogAware
    {
        public string Title => "弹窗标题";

        /// <summary>
        /// 关闭弹窗操作
        /// </summary>
        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// 是否允许关闭弹窗
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CanCloseDialog()
        {
            return true;
        }

        /// <summary>
        /// 当窗口关闭时调用
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDialogClosed()
        {
        }

        /// <summary>
        /// 当窗口打开时调用执行
        /// </summary>
        /// <param name="parameters"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDialogOpened(IDialogParameters parameters)
        {
            //获取打开窗口时传递的参数
            string userName = parameters.GetValue<string>("userName");
        }
    }
}
