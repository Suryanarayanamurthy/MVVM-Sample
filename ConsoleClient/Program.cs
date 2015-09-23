using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductsPrice;
using System.Collections.ObjectModel;

/// <summary>
/// /// <summary>
/// all the final work is done/ reused from the viewmodel object, 
/// this project is just a UI interface for doing exactly the same
///  thing that you can do on GUI application
/// </summary>
/// </summary>
namespace ProductsPrice
{
    class Program
    {
        static Program thisObj;
        
        ProductsViewModel VM_Obj;
        // the main command, (linke menu items).
        List<string> commandlist = new List<string> { "help : for help vailable ",
            "add: for adding new item ",
            "update : for updating an exiting item ",
            "delete : for deleting an existing item ",
            "save : for saving the products list ",
            "exit : for exiting the application",
            "showall : to display all available products items"
        };
        public Program()
        {
            VM_Obj = new ProductsViewModel();
            showAvailableCommands();
            MainThread();
        }

        // entry point for a console application.
        static void Main(string[] args)
        {

            //Console.WriteLine("Welcome");
            thisObj = new Program();
        }
        
        /// <summary>
        /// load all the command available to user, like the main menu item.
        /// </summary>
        private void showAvailableCommands()
        {
        foreach(string commandString in commandlist)
            {
                Console.WriteLine(commandString);
            }
        }
        int index;

        /// <summary>
        /// show the products list
        /// </summary>
        private void showProductsList()
        {
            index = 0;
            Console.WriteLine("{index: \t Product Name: \t Product price}");
            Console.WriteLine("--------------------------------------------");
            
            //better solution is to have a dictionary with key valuepair. since already the implimentation for viewModel is done,
            // just doing a workaround to get the index of the product.

            foreach (Product item in VM_Obj.ProductsList)
            {
                Console.WriteLine("{"+index++.ToString() +"\t"+ item.ProductName +"\t"+item.ProductPrice +"}");
            }
        }
        /// <summary>
        /// as name indicated this is the mainthread of the application.
        /// </summary>
        private void MainThread()
        {
            string userInput = string.Empty;
            while (userInput != "exit")
            {
                // display a prompt
                Console.Write("> ");
                // get the input
                userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case "help":
                        showAvailableCommands();
                        break;
                    case "add":
                        addnewItem();
                        break;
                    case "update":
                        update();
                        break;
                    case "delete":
                        delete();
                        break;
                    case "save":
                        save();
                        break;
                    case "exit":
                        //
                        break;
                    case "showall":
                        showProductsList();
                        break;
                    default:
                        {
                            Console.WriteLine("Plz enter a valid command:");
                        }
                        break;
                }
                showAvailableCommands();
            }
        }

        /// <summary>
        /// simply call the viewmodel's save method.
        /// </summary>
        private void save()
        {
            VM_Obj.savelist(null);
        }

        /// <summary>
        /// get the selected item and from the user and delete using th VM's logic.
        /// </summary>
        private void delete()
        {
            showProductsList();
            Console.WriteLine("enter the index value of the product which you would like to delete.");
            // 1st setep is to make the user select one of the products items.
            if (selectProductsItem())
            {
                
                // check to see if the selected item is not null
                if (VM_Obj.selectedItem != null)
                {
                    Console.WriteLine("Successfully deleted the item: " + VM_Obj.ProductName);
                    VM_Obj.deleteItem(null);
                }
            }
        }

        /// <summary>
        /// /
        /// </summary>
        private void update()
        {
            // 1st step is to make the user select one of the products items.
            if (selectProductsItem())
            {
                Console.WriteLine("you hav selected the item: " + VM_Obj.selectedItem.ProductName );
                //2nd get info if the user wants to update by value or increment by percentage.
                if(update_type())
                {
                    //3rd step read the entered value and update it to the list;
                    if (updatedvalue())
                    {
                        // now we got the new value entered by the user, we can update the product value
                        if (VM_Obj.selectedItem != null)
                        {

                            VM_Obj.UpdateSelected(null);
                            Console.WriteLine("successfully updated!!!");

                        }
                    }

                }

            }

            
           

        }

        /// <summary>
        /// read the entered value.
        /// </summary>
        /// <returns></returns>
        private bool updatedvalue()
        {
            bool _return = false;

            Console.WriteLine("enter the price update value:");
            double enteredValue;
            bool continueLoop = true;
            while (continueLoop)
            {
                string stringEntered = Console.ReadLine();
                switch (stringEntered)
                {
                    case "back":
                        {
                            continueLoop = false;
                            _return = false;
                        }
                        break;

                    default:
                        if (double.TryParse(stringEntered, out enteredValue))
                        {
                            VM_Obj.currentValue = enteredValue;
                            continueLoop = false;
                            _return = true;
                        }
                        else
                        {
                            Console.WriteLine("enter numeric value to updaate the price");
                            Console.WriteLine("back: go back without adding");
                        }
                        break;
                }
            }
            return _return;
        }

        /// <summary>
        /// make the user enter the index number for seleted item.
        /// </summary>
        /// <returns></returns>
        private bool selectProductsItem()
        {

            showProductsList();
            bool _return = false;
            Console.WriteLine("enter the product's index number to select an product item");
            bool continueLoop = true;
            int selectedIndex;
            while (continueLoop)
            {
                string stringentered = Console.ReadLine();
                switch (stringentered)
                {
                    case "back":
                        {
                            continueLoop = false;
                            _return = false;

                        }
                        break;

                    default:
                        if (int.TryParse(stringentered, out selectedIndex))
                        {
                            if ((selectedIndex >= VM_Obj.ProductsList.Count) || (selectedIndex < 0))
                            {
                                showProductsList();
                                Console.WriteLine("entered index is out of range, refer the above list to enter correct index.");
                                Console.WriteLine("back: go back without updating");
                                break;
                            }
                            else
                            {
                                try
                                {
                                    continueLoop = false;
                                    VM_Obj.selectedItem = VM_Obj.ProductsList[selectedIndex];
                                    _return = true;
                                }
                                //exception , is possiable for invalid index value.
                                catch (Exception ex)
                                {
                                    Console.WriteLine("exception at : " + ex.Source);
                                    _return = false;
                                }
                            }
                        }
                        else
                        {
                            showProductsList();
                            Console.WriteLine("back: go back without updating");
                        }
                        break;
                }
            }
            return _return;
        }

        /// <summary>
        /// get info from unser , on what type of update they want , update by value or increment by percentage.
        /// </summary>
        /// <returns></returns>
        private bool update_type()
        {
            bool _return = false;
            Console.WriteLine("enter 'p' for update by percentage,\n"+
                "enther 'v' for update the price by value,\n"+
                "enter 'back' to exit to main menu without updating.\n");
            Console.WriteLine(">");
            bool continueLoop = true;
            while (continueLoop)
            {
                string stringentered = Console.ReadLine().ToLower();
                switch (stringentered)
                {
                    case "back":
                        {
                            continueLoop = false;
                            _return= false;
                        }
                        break;
                    case "p":
                        {
                            continueLoop = false;
                            _return = true;
                            VM_Obj.IsPercentage = true;
                        }
                        break;
                    case "v":
                        {
                            continueLoop = false;
                            _return = true;
                            VM_Obj.IsValue = true;
                        }
                        break;


                    default:
                        {
                            Console.WriteLine("enter 'p' for update by percentage,\n" +
                "enther 'v' for update the price by value,\n" +
                "enter 'back' to exit to main menu without updating.\n");
                            Console.WriteLine(">");
                        }
                        break;
                }
            }
            return _return;
        }

        /// <summary>
        /// getting user entered data, and making use of the view model to add the new product.
        /// </summary>
        private void addnewItem()
        {
            Product newProduct = new Product();

            Console.WriteLine("enter the product name:");
            newProduct.ProductName = Console.ReadLine();
            Console.WriteLine("enter the price of the product:");
            double productPrice;
            bool continueLoop = true;
            while (continueLoop)
            {
                string stringEntered = Console.ReadLine();
                switch (stringEntered)
                {
                    case "back":
                        {
                            continueLoop = false;
                            newProduct = null;
                        }
                        break;

                    default:
                        if (double.TryParse(stringEntered, out productPrice))
                        {
                            newProduct.ProductPrice = productPrice;
                            continueLoop = false;
                        }
                        else
                        {
                            Console.WriteLine("enter numeric value for price");
                            Console.WriteLine("back: go back without adding");
                        }
                        break;
                }
            }
            if(newProduct != null)
            {
                VM_Obj.ProductsList.Add(newProduct);
            }
        }
    }
}
