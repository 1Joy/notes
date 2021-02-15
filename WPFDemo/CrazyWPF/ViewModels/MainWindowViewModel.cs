using CrazyWPF.Models;
using CrazyWPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyWPF.ViewModels
{
    public class MainWindowViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Restaurant restaurant;
        public Restaurant Restaurant
        {
            get { return restaurant; }
            set {
                restaurant = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Restaurant"));
            }
        }

        private List<DishMenuItemViewModel> dishList;
        public List<DishMenuItemViewModel> DishList
        {
            get { return dishList; }
            set {
                dishList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DishList"));
            }
        }


        public MainWindowViewModel()
        {
            LoadRestaurant();
            LoadDish();
        }
        
        /// <summary>
        /// 加载餐馆信息
        /// </summary>
        private void LoadRestaurant()
        {
            Restaurant = new Restaurant()
            {
                Name = "Crazy",
                Address = "成都市大院北二街",
                PhoneNumber = "12345678"
            };
        }

        private void LoadDish()
        {
            IDataService dataService = new XmlDataService();
            var dishes = dataService.GetAllDishes();
            DishList = new List<DishMenuItemViewModel>();
            foreach (var item in dishes)
            {
                DishMenuItemViewModel dishMenuItemViewModel = new DishMenuItemViewModel() {
                    Dish = item,
                    IsSelected=false
                };
                DishList.Add(dishMenuItemViewModel);

            }
        }

        public void SelectDishMenuItem()
        {

        }
    }
}
