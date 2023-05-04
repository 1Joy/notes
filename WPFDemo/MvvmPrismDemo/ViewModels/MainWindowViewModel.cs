using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmPrismDemo
{
    public class MainWindowViewModel:BindableBase,INotifyDataErrorInfo
    {

		private int _value1;

		public int Value1
		{
			get { return _value1; }
			set { _value1 = value; }
		}


		private int _value;

		public int Value
		{
			get { return _value; }
			set {

				if (value > 50)
				{
                    //添加异常描述，会触发事件回调OnErrorContainerCreate()
                    _errorsContainer.SetErrors(nameof(value), new string[] { "值不能大于50" });
				}

				//属性的五种通知方式

				//第一种方式
				SetProperty(ref _value, value);

				////第二种方式
				//this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(Value)));

				////第三种方式
				//this.RaisePropertyChanged();

				////第四种方式：可以通知到另一个属性
				//SetProperty(ref _value, value,"Value1");

				////第五种方式
				//SetProperty(ref _value, value, OnValueChanged);
			}
		}

        

        private void OnValueChanged()
		{
			//当属性变化时，执行某些操作
		}



		private ErrorsContainer<string> _errorsContainer;

		public ErrorsContainer<string> ErrorsContainer
		{
			get {
				if(_errorsContainer is null)
				{
					//创建异常容器，
					_errorsContainer = new ErrorsContainer<string>(OnErrorContainerCreate);
				}
				return _errorsContainer; }
			set { _errorsContainer = value; }
		}

		private void OnErrorContainerCreate(string arg)
		{
			//触发异常消息的添加
			ErrorsChanged?.Invoke(this,new DataErrorsChangedEventArgs(arg));
		}

		#region INotifyDataErrorInfo

		//是否存在异常
		public bool HasErrors => ErrorsContainer.HasErrors;
        public IEnumerable GetErrors(string? propertyName)
        {
           return _errorsContainer.GetErrors(propertyName);
        }

		//通知，通知到属性的ValidationError
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        #endregion

        #region command
		
		//基本使用，只是关联了一个行为过程
		public ICommand BtnCommand { get => new DelegateCommand(BtnCommandHandler);  }

		public ICommand BtnCheckCommand { get => new DelegateCommand(BtnCommandHandler,CanExecute); }

		public DelegateCommand BtnCheckCommand1 { get; set; }
		public DelegateCommand BtnAsyncCommand { get; set; }

		public DelegateCommand<object> BtnEventCommand { get; set; }

		private bool _canExecuteProp;

		public bool CanExecuteProp
		{
			get { 
				//命令状态检查逻辑
				return Value==50;
			}
			set { _canExecuteProp = value; }
		}


		private void InitCmd()
		{
            //初始化命令
            //命令状态检查的第一种方式
            //利用RaiseCanExecuteChanged进行手动触发
            //BtnCheckCommand1 = new DelegateCommand(BtnCommandHandler,CanExecute);

            //命令状态检查的第二种方式
            //ObservesProperty：表示监听一个或多个属性的变化，当有属性值发生变化时，自动进行命令状态检查；就不需要手动触发RaiseCanExecuteChanged
            //可同时通过ObservesProperty监听多个属性
            //BtnCheckCommand1 = new DelegateCommand(BtnCommandHandler, CanExecute).ObservesProperty(()=>Value);
            // BtnCheckCommand1 = new DelegateCommand(BtnCommandHandler, CanExecute).ObservesProperty(() => Value).ObservesProperty(()=>Value1);

            //命令状态检查的第三种方式
            //ObservesCanExecute：通过观察一个属性来判断是否能够执行命令，且该属性必须是一个bool类型
            //ObservesCanExecute()的判断优先级高于在初始化时传入的canExecute()
            BtnCheckCommand1 = new DelegateCommand(BtnCommandHandler).ObservesCanExecute(()=> CanExecuteProp);

			BtnAsyncCommand = new DelegateCommand(BtnCommandAsyncHandler);
			BtnEventCommand = new DelegateCommand<object>(BtnEventCommandHandler);
        }

        private void BtnCommandHandler()
		{
			///todo
		}

		private async void BtnCommandAsyncHandler()
		{
			await Task.Delay(50);
			///todo
		}

		private void BtnEventCommandHandler(object args)
		{
			//args：表示触发的控件事件里面的参数
		}
		private bool CanExecute()
		{

            ///todo,自定义属性状态检查
            //true:表示命令可用；false:表示命令不可用
            //(BtnCheckCommand as DelegateCommand)?.RaiseCanExecuteChanged();通过此方法触发CanExecute()执行
            return true;
		}
        #endregion


        public CompositeCommand CustomCompositeCommand { get; set; }

		public DelegateCommand CustomCommand1 { get; set; }
		public DelegateCommand CustomCommand2 { get; set; }

		public void InitCmd1()
		{
			CustomCompositeCommand = new CompositeCommand();

			CustomCommand1 = new DelegateCommand(() => { });
			CustomCommand2 = new DelegateCommand(() => { });
			CustomCompositeCommand.RegisterCommand(CustomCommand1);
			CustomCompositeCommand.RegisterCommand(CustomCommand2);
		}
    }
}
