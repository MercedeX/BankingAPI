**Banking API**
It makes use of following packages/ Libraries:

 1. SimpleInjector (Dependency Injection)
 2. Thrower (Preconditions)
 3. NLog (Logging)
 4. EntityFramework (ORM - database)

These tools have been used to create:

 1. Microsoft Sql Server 2017 / Management Studio
 2. Microsoft Visual Studio 2017
 3. NClass (Class designer)
 4. dbDesigner (Database designer)
 5. SourceTree (Git repository manager)
 6. Postman (for testing)

To run this:.

 1. Create a blank database
 2. run the initialize_database.sql from the root of code repository. 
 3. clone the repository on your local machine
 4. Open the project in Visual Studio and Run
 5. Open Postman and call WebAPI.

WebAPI it exposes:

 1. CreateAccount
 2. Deposit
 3. Withdraw
 4. Transfer (from one account to another)
 5. Transactions (all transaction history)
 6. Customer (all accounts of the customer)
