using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMDemo.Base
{
    public class CommandBase : ICommand
    {
        /// <summary>
        /// 通知，触发一次CanExecute
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            //将具体的CanExecute逻辑添加到CommandManager里面
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Action<object> DoExecute { get; set; }

        /// <summary>
        /// 具体执行是否可用判断的委托
        /// </summary>
        public Func<object,bool> DoCanExecute { get; set; }

        /// <summary>
        /// 绑定对象是否可用
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true：可用</returns>
        public bool CanExecute(object parameter)
        {
            return DoCanExecute?.Invoke(parameter) == true;
        }

        /// <summary>
        /// 执行过程
        /// </summary>
        /// <param name="parameter">commandParameter参数</param>
        public void Execute(object parameter)
        {
            //具体的实现逻辑
            DoExecute?.Invoke(parameter);
        }


        /// <summary>
        /// 在命令对象内触发CanExecuteChanged事件
        /// 用于在外部触发命令可用通知
        /// </summary>
        public void DoCanExecuteChanged()
        {
            //CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
