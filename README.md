# FlooringOrderingSystem

## Description
A system for placing orders for different types of flooring. Users can view a list of orders placed on specific dates, add an order, edit an order, or remove an order.

A C# Console application. CRUD functionality using MVC architecture. Concepts used: Exception Handling, Mocking, Interfaces, System.IO, Inheritance, Unit Testing, Input Validation, Regular Expressions, and DateTime.

## Usage

User is greeted with a menu of choices:

![image](https://user-images.githubusercontent.com/76139710/151393168-653c9f0b-8fe8-44a5-8d6a-25f6a4bd0349.png)

To add an order, a user must enter the order date(in the future), their name, and the State in which the order is being placed:

![image](https://user-images.githubusercontent.com/76139710/151393533-327b4b71-21ce-48c8-97fa-9d54bf086bd6.png)

The user is then prompted to select the type of flooring:

![image](https://user-images.githubusercontent.com/76139710/151393674-3df70275-2572-4faa-8a09-a302f00da60d.png)

They are then prompted for the square footage:

![image](https://user-images.githubusercontent.com/76139710/151393914-d64caf60-389d-4275-a303-689480d0af83.png)

The user is then shown a summary of the order. Tax information, Labor costs and material costs are read in from an external file and used to calculate the order totals based on the tax rate of the State entered and the square footage entered:

![image](https://user-images.githubusercontent.com/76139710/151394009-d71d8a8f-8a38-4648-b122-3071c4ce91ed.png)

If the user chooses to save the order, the order is written to an external file for the date of the order. If the file for that order date exists, the order information is appended to the file. If a file for that order date does not exists, a new file is created for that order date and the order information is added.









