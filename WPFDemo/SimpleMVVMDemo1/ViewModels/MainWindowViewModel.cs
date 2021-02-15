using Microsoft.Win32;
using SimpleMVVMDemo1.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMVVMDemo1.ViewModels
{
    class MainWindowViewModel:NotificationObject
    {
        /// <summary>
        /// 声明与view相关的数据属性
        /// </summary>
        private double input1;

        public double Input1
        {
            get { return input1; }
            set
            {
                input1 = value;
                //通知view的binding的某属性改变
                RaisePropertyChange("Input1");
            }
        }


        private double input2;

        public double Input2
        {
            get { return input2; }
            set
            {
                input2 = value;
                RaisePropertyChange("Input2");
            }
        }

        private double result;

        public double Result
        {
            get { return result; }
            set
            {
                result = value;
                RaisePropertyChange("Result");
            }
        }

        /// <summary>
        /// 声明与View相关的命令属性
        /// </summary>
        public DelegateCommand AddCommand { get; set; }

        public DelegateCommand SaveCommand { get; set; }

        public MainWindowViewModel()
        {
            AddCommand = new DelegateCommand();
            SaveCommand = new DelegateCommand();
            AddCommand.ExecuteAction += Add;
            SaveCommand.ExecuteAction += Save;
        }

        private void Save(object param)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog();
        }
        private void Add(object param)
        {
            Result = Input1 + Input2;
        }
    }
}
