using CrazyWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyWPF.ViewModels
{
    public class DishMenuItemViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Dish Dish { get; set; }

        /// <summary>
        /// 是否被选中，与view属性一致
        /// </summary>

        private bool isSelected; 
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                //通知binding属性发生改变
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
            }
        }

    }
}
