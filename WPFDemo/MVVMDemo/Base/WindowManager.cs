using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMDemo.Base
{
    public class WindowManager
    {
        /// <summary>
        /// 注册的窗口存放
        /// 通过反射实现
        /// </summary>
        static Dictionary<string, Type> _windows = new Dictionary<string, Type>();

        public static void Register(Type type)
        {
            if (!_windows.ContainsKey(type.Name))
            {
                _windows.Add(type.Name, type);
            }
        }

        /// <summary>
        /// 显示窗口界面
        /// </summary>
        /// <param name="winKey">传窗口的名称而不是窗口的类型，为了解耦合</param>
        public static void ShowDialog(string winKey,object dataContext)
        {
            if (_windows.ContainsKey(winKey))
            {
                Type type = _windows[winKey];
                var win = (Window)Activator.CreateInstance(type);
                win.DataContext = dataContext;
                win.ShowDialog();
            }
        }
    }
}
