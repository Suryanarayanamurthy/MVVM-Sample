using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace interSalesAssignment
{
    /// <summary>
    /// Class to hold data in the ListView
    /// </summary>
    public class ListViewData
    {
        public ListViewData()
        {
            // default constructor
        }
        int _ProductPrice;
        public ListViewData(string col1, string col2)
        {
            ProductName = col1;
            ProductPrice = col2;
        }
        
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        //get { return _ProductPrice.ToString(); }
        //set {
        //  value
        //}
    //}
    }
}
