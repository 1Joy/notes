using MVVMDemo.Base;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMDemo.Models
{
    /*
     * INotifyPropertyChanged：通知发布某属性的变化
     */

    //public class MainWindoeModel:INotifyPropertyChanged
    [AddINotifyPropertyChangedInterface]
    public class MainWindoeModel
    {
        /// <summary>
        /// AlsoBotifyForAttribute：实现通知的时候，同时通知指定属性
        /// DoNotNotify：指定不需要自动实现代码改变的通知
        /// DependsOn：指定哪些属性变化的时候，通知当前属性变化
        /// DoNotCheckEquality：强制不做旧值对比（默认情况会自动添加对比代码
        /// ）
        /// </summary>
        //[DoNotNotify]
        public double Value1 { get; set; } = 1;
        public double Value2 { get; set; } = 2;
        private double value3;

        public double Value3
        {
            get { return value3; }
            set { 
                value3 = value;

                //发布事件：告诉所有绑定了这个属性的界面对象属性需要更新值了
                //PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("Value3"));

                //通过CommandManager强制触发CanExecuteChanged事件；也可以在一定周期内默认自动触发所有的canExecute事件有一定的性能损耗
                CommandManager.InvalidateRequerySuggested();

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand BtnCommand { get; set; }

        public MainWindoeModel()
        {
            BtnCommand = new CommandBase()
            {
                DoExecute = new Action<object>((p) =>
                {
                    Console.WriteLine("BtnCommand");
                }),
                
            };

            //通过CommandManager添加是否可用的判断逻辑
            BtnCommand.CanExecuteChanged += BtnCommand_CanExecuteChanged;
        }

        private void BtnCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
