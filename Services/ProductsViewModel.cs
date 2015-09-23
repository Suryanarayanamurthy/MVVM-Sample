using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Windows.Data;
using System.Windows;

namespace ProductsPrice
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        // default constructor, initializing required fields and loading the products list from the file.
        public ProductsViewModel()
        {
            _ProductsList = new ObservableCollection<Product>();
            _selectedItem = new Product();
            _ProductPrice = 0.0;
            loadFromXml();
            addCmd = new RelayCommand(addNewItem, IsNotDirty);
            deleteCmd = new RelayCommand(deleteItem, IsNotDirty);
            closeCmd = new RelayCommand(closeApplication, IsNotDirty);
            saveCmd = new RelayCommand(savelist, IsNotDirty);
            UpdateCmd = new RelayCommand(UpdateSelected, IsNotDirty);
        }

        #region fields
        //required implimentation of the interface InotifyPropertchange
        public event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<Product> _ProductsList;
        private Product _selectedItem;
        // to get the root directory of the current execution, needed to access the products.xml file(to load and to save the lsit).
        string basedir = AppDomain.CurrentDomain.BaseDirectory;
        string _newProductName;
        double _newPriceValue;
        bool _IsnewItem;
        private double _ProductPrice;


        /// <summary>
        /// it is possiable to use only one bool here, 
        /// but if the application is extended in the future and
        /// if we have 2 or more checkboxes then all we need to do here is to add the remaining bool variables and continue with the logic,
        /// the existing implimentation need not be changed.
        /// </summary>
        bool _IsValue;
        bool _IsPercentage;


        #endregion //fields

        #region properties

        #region IcommandProperties
        public ICommand addCmd { get; private set; }
        public ICommand deleteCmd { get; private set; }
        public ICommand closeCmd { get; private set; }
        public ICommand saveCmd { get; private set; }
        public ICommand UpdateCmd { get; private set; }
        #endregion //IcommandProperties


        /// <summary>
        /// currently used to check if the selected item is changed and is dirty, 
        /// can be used to handel, other business logic.
        /// </summary>
        public bool IsDirty
        {
            get { return _selectedItem.IsDirty; }
            set
            {
                if (_selectedItem != null)
                {
                    if (_selectedItem.IsDirty != value)
                    {
                        _selectedItem.IsDirty = value;
                        OnPropertyChanged("IsDirty");
                    }
                }
            }
        }

        /// <summary>
        /// the main list in the application, holds a list of objects of type product.
        /// binded to the listview, of the UI.
        /// same list is used by the console project also.
        /// </summary>
        public ObservableCollection<Product> ProductsList
        {
            get
            {

                return _ProductsList;
            }
            //set
            //{
            //    _ProductsList = value;
            //    OnPropertyChanged("ProductsList");
            //}
        }

        public string ProductName
        {
            get
            {
                return _selectedItem.ProductName;
                //      return _ProductName;
            }
            set
            {
                _selectedItem.ProductName = value;
                //_ProductName = value;
                OnPropertyChanged("ProductName");
            }
        }

        /// <summary>
        /// when, product price is updated in the, this property holds the user entered value.
        /// same valuse is used to increment the product price by percentage or to simply update the product prive by value;
        /// decission based on the radiobox selectedon the UI.
        /// </summary>
        public double currentValue
        {
            get
            {
                return _ProductPrice;
            }
            set
            {
                _ProductPrice = value;
                OnPropertyChanged("currentValue");
            }
        }

        /// <summary>
        /// product Price shouwn on the listview.
        /// </summary>
        public double ProductPrice
        {
            get { return _selectedItem.ProductPrice; }
            set
            {
                _selectedItem.ProductPrice = value;
                OnPropertyChanged("ProductPrice");
            }
        }
        /// <summary>
        /// the product item selected by the user on the list view is binded to, this property.
        /// </summary>
        public Product selectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value != null)
                {

                    _selectedItem = value;
                    currentValue = _selectedItem.ProductPrice;
                    IsDirty = false;
                }
                OnPropertyChanged("selectedItem");
            }
        }


        /// <summary>
        /// when new product item is added
        /// </summary>
        public string newProductName
        {
            get { return _newProductName; }
            set
            {
                _newProductName = value;
                IsnewItem = true;
                OnPropertyChanged("newProductName");
            }
        }
        /// <summary>
        /// when new product item is added
        /// </summary>
        public double newPriceValue
        {
            get { return _newPriceValue; }
            set
            {
                _newPriceValue = value;
                OnPropertyChanged("newPriceValue");
            }
        }
        /// <summary>
        /// to enable or desable the add new product button.
        /// </summary>
        public bool IsnewItem
        {
            get { return _IsnewItem; }
            set
            {
                if (!string.IsNullOrEmpty(newProductName))
                {
                    _IsnewItem = true;
                }
                else { _IsnewItem = false; }
                OnPropertyChanged("IsnewItem");
            }
        }

        /// <summary>
        /// radio button
        /// </summary>
        public bool IsValue
        {
            get { return _IsValue; }
            set
            {
                if (_IsValue != value)
                {
                    _IsValue = value;
                    IsDirty = true;
                    OnPropertyChanged("IsValue");
                }
            }
        }
        /// <summary>
        /// radio button
        /// </summary>
        public bool IsPercentage
        {
            get { return _IsPercentage; }
            set
            {
                if (_IsPercentage != value)
                {

                    _IsPercentage = value;
                    IsDirty = true;
                    OnPropertyChanged("IsPercentage");
                }
            }
        }

        #endregion //properties

        #region methods

        /// <summary>
        /// currently returns true all the time, and update the same method for raising or removing an even from being handled.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool IsNotDirty(object obj)
        {
            return (true);
        }


        /// <summary>
        /// update an existing product item.
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateSelected(object obj)
        {
            IsDirty = true;
            ProductsList.Remove(selectedItem);
            Product newItem = new Product(selectedItem.ProductName, selectedItem.ProductPrice);
            if (_IsValue)
            {
                newItem.ProductPrice = currentValue;
            }
            else if (_IsPercentage)
            {
                newItem.ProductPrice = newItem.UpdateByPercentage(currentValue);
            }
            // little bit work around for making the observable collection to refresh on the list view.
            // if found a better solution to do this, shouls update this method accordingly.
            ProductsList.Add(newItem);
            selectedItem = newItem;
            IsDirty = false;
            IsValue = false;
            IsPercentage = false;
            currentValue = 0.0;
        }

        /// <summary>
        /// load the products list from products.xml file
        /// </summary>
        private void loadFromXml()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Product>));
            if (File.Exists(basedir + "/Products.xml"))
            {
                using (StreamReader rd = new StreamReader(basedir + "/Products.xml"))
                {
                    _ProductsList = xs.Deserialize(rd) as ObservableCollection<Product>;
                }
            }
        }

        /// <summary>
        /// save the productslist to the products.xml file.
        /// </summary>
        /// <param name="obj"></param>
        public void savelist(object obj)
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Product>));
            using (StreamWriter wr = new StreamWriter(basedir + "/Products.xml"))
            {
                xs.Serialize(wr, _ProductsList);
            }
        }

        /// <summary>
        /// todo: check if any object is dirty and set the flag/warning etc,. to the user
        /// </summary>
        /// <param name="obj"></param>
        private void closeApplication(object obj)
        {

        }

        /// <summary>
        /// remove the selected item from the products collection.
        /// </summary>
        /// <param name="obj"></param>
        public void deleteItem(object obj)
        {
            _ProductsList.Remove(_selectedItem);
        }

        /// <summary>
        /// create a new products object and add it to the productsList.
        /// </summary>
        /// <param name="obj"></param>
        private void addNewItem(object obj)
        {
            Product newItem = new Product(_newProductName, _newPriceValue);
            //newItem.IsDirty = true;
            _ProductsList.Add(newItem);
            selectedItem = newItem;
            newProductName = String.Empty;
            newPriceValue = 0;
        }

        /// <summary>
        /// to notify property changed to any of the scrubcribers.
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            //Fire the PropertyChanged event in case somebody subscribed to it
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                //if (!propertyName.Equals("IsDirty"))
                //{ IsDirty = true; }
            }
        }


        #endregion //methods
    }
}
