using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Data;

/// <summary>
/// save and retrive to MyData.xml
/// </summary>
namespace interSalesAssignment
{
    public class MyData
    {
        /// <summary>
        /// Saves items to MyData.xml file in bin folder.
        /// </summary>
        /// <param name="items"></param>
        //public void Save(System.Windows.Data.CollectionView items)
        public void Save(System.Windows.Data.DataSourceProvider items)
        {
            //XDocument xdoc = new XDocument();

            //XElement xeRoot = new XElement("Data");
            //XElement xeSubRoot = new XElement("Rows");

            //foreach (var item in items)
            //{
            //    Product lvc = (Product)item;

            //    XElement xRow = new XElement("Row");
            //    xRow.Add(new XElement("col1", lvc.ProductName));
            //    xRow.Add(new XElement("col2", lvc.ProductPrice));

            //    xeSubRoot.Add(xRow);
            //}
            //xeRoot.Add(xeSubRoot);
            //xdoc.Add(xeRoot);

            //xdoc.Save("MyData.xml");
        }

        /// <summary>
        /// Gets data from MyData.xml as rows.  
        /// </summary>
        /// <returns></returns>
       // public IEnumerable<object> GetRows()
        //{
            //List<Product> rows = new List<Product>();

            //if (File.Exists("MyData.xml"))
            //{
            //    // Create the query 
            //    var rowsFromFile = from c in XDocument.Load(
            //                "MyData.xml").Elements(
            //                "Data").Elements("Rows").Elements("Row")
            //                       select c;

            //    // Execute the query 
            //    foreach (var row in rowsFromFile)
            //    {
            //        rows.Add(new Product(row.Element("col1").Value,
            //                row.Element("col2").Value));
            //    }
            //}
            //return rows;
        //}
    }
}
