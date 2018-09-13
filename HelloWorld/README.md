# HelloWorld
Purpose: Write a Hello World program 

Requirements:

1. The program has 1 current business requirement a write Hello World to the console/screen. 

2. The program should have an API that is separated from the program logic to eventually support mobile applications, web applications, console applications or windows services. 

3. The program should support future enhancements for writing to a database, console application, etc. 

    a. Use common design patterns (inheritance, e.g.) to account for these future concerns. 

    b. Use configuration files or another industry standard mechanism for determining where to write the information to. 

4. Write unit tests to support the API
 
The Hello World Application consists of:

1. ConsoleApp - A Console Application that will write to the Console

2. HelloWorldAPI - A Web API for the Console Application to communicate with

3. HelloWorldInfrastructure - A Class library containing reusable code 
 
4. HelloWorldAPI.Tests - A Test project containing unit tests for the application

To run the application:

1. Build the solution (all of the projects)

2. Set the HelloWorldAPI project as the 'Startup Project'

3. Debug/run the HelloWorldAPI project

4. Open a cmd window

5. Run the ConsoleApp binary in the cmd window (HelloWorld\ConsoleApp\bin\Debug\ConsoleApp.exe)
