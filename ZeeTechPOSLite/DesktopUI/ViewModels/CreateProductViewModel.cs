using DesktopUI.Commands;
using DesktopUI.Commands.CreateProductCommands;
using DesktopUI.Models;
using DesktopUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopUI.ViewModels
{
    public class CreateProductViewModel : ViewModelBase
    {
        #region Input Properties


        private string _ProductName;

        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                _ProductName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }


        private string _productDescription;

        public string ProductDescription
        {
            get { return _productDescription; }
            set
            {
                _productDescription = value;
                OnPropertyChanged(nameof(ProductDescription));
            }
        }


        private string _barcode;

        public string Barcode
        {
            get { return _barcode; }
            set
            {
                _barcode = value;
                OnPropertyChanged(nameof(Barcode));
            }
        }


        private decimal _averageCost;

        public decimal AverageCost
        {
            get { return _averageCost; }
            set
            {
                _averageCost = value;
                OnPropertyChanged(nameof(AverageCost));
            }
        }



        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private ObservableCollection<DepartmantModel> _departments;

        public ObservableCollection<DepartmantModel> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }

        private DepartmantModel _selectedDepartment;

        public DepartmantModel SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
            }
        }

        #endregion

        #region Commands
        public ICommand CloseCommand { get; }
        public ICommand CreateCommand { get; }

        #endregion

        #region Constructor

        public CreateProductViewModel(INavigationService closeNavigationService)
        {
            Departments = new ObservableCollection<DepartmantModel>()
            {
                new DepartmantModel(){ DepartmentName = "Mobile"},
                new DepartmantModel(){ DepartmentName = "Computer"},
                new DepartmantModel(){ DepartmentName = "Camera"},
                new DepartmantModel(){ DepartmentName = "Home"},
                new DepartmantModel(){ DepartmentName = "AV"},
                new DepartmantModel(){ DepartmentName = "Repair"}
            };

            CloseCommand = new CloseModalCommand(closeNavigationService);

            CreateCommand = new CreateCommand(this, closeNavigationService);
        }
        #endregion

        #region Methods
        

        #endregion
    }
}
