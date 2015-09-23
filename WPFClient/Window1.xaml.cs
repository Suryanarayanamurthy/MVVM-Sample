/*
Done by Surya.
ProductsPrice sample apllication.
products maintainence application.
*/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProductsPrice;

namespace ProductsPrice
{
    /// <summary>
    /// Code behind for windows1 page.
    /// </summary>
    public partial class Window1 : Window
    {
        private ProductsViewModel _viewmodel;
        public Window1()
        {
            //setting the datacontext to the view model.
            _viewmodel = new ProductsViewModel();
            this.DataContext = _viewmodel;
            InitializeComponent();
        }

        /// <summary>
        /// closeButton click event handler.
        /// Closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // to make sure the user enters only the numbers in the price column.

        private void textBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        //method which excepts numbers and "."
        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "."))
            {
                e.Handled = true;
            }
        }
    }
}
