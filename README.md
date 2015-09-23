# MVVM-Sample
product name and price control
Product  Store  in  .NET :

A product store application, which can be used to save and modify product information. The product information consists of a product name (string) and a product price (numerical data type). The user can add and delete products to / from the store as well as increase the product price based on either a percentage or an absolute price	 amount.
Functionality:
1. Add product with name and price
2. Delete product by name
3. Increase prices of all products either
	a. by a percentage
	b. or an absolute amount
Storage:
The data should be persisted in a file on disc, so that the product information is still available after the application is restarted.
 You can choose the proper format of the file to store the product information.

User interface:
There should be two user interfaces available:
1. A command line interface (CLI): Console Application
2. A graphical user interface (GUI): WPF Application

Both user interfaces provide the functionality as previously described.

The CLI and the GUI application are both able to work with the same file format and can read or write to the same product information file on disc.