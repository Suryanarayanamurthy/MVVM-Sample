using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductsPrice
{
    /// <summary>
    /// The data model part.
    /// </summary>
    public class Product
    {
        #region constructors

        /// <summary>
        /// default constructor
        /// </summary>
        public Product() : this(string.Empty, 0)
        {
            // default constructor
        }

        /// <summary>
        /// overloaded constructor.
        /// </summary>
        /// <param name="PName"></param>
        /// <param name="PPrice"></param>
        public Product(string PName, double PPrice)
        {
            ProductName = PName;
            _ProductPrice = PPrice;
        }

        #endregion //constructors

        #region field
        double _ProductPrice;
        internal bool IsDirty;
        #endregion // fields


        #region property
        public string ProductName { get; set; }

        public double ProductPrice
        {
            get { return _ProductPrice; }
            set
            {
                _ProductPrice = value;
            }
        }

        public double currentValue { get; set; }
        #endregion //property

        #region methods

        /// <summary>
        /// Increment price value by percentage.
        /// </summary>
        /// <param name="Increasedpercentage"></param>
        /// <returns></returns>
        public double UpdateByPercentage(double Increasedpercentage)
        {
            return (_ProductPrice + (_ProductPrice * (Increasedpercentage * 0.01)));
        }
        #endregion //methods
    }
}
