## Welcome to the Invoice kit Docs

Invoice Kit is a C# Class library for generating Invoices,It is built with .Net 5 and is currently underdevelopment 

### Installation

To get started with this library you need to first download and install the library on nuget.
On the CLI type `dotnet add package InvoiceKit --version 1.0.0-alpha` to install the current stable version
On Package Manager uess `Install-Package InvoiceKit -Version 1.0.0-alpha`

to verify your installation ,open the csproj file and look for the following package in your ItemGroup Attribute 
 * `<PackageReference Include="InvoiceKit" Version="1.0.0-alpha" />`

### Getting Started

 Getting started with a library is easy,the library uses a fluent Builder API class called InvoiceBuilder which you can use to build the library.
 a full documentation of every step to create an invoice will be created soon mean while if you need to learn more about using the library please look at the cli example which comes with the library.
 
## Requirements 
  - [.Net 5](https://dotnet.microsoft.com/download/dotnet/5.0)
  - Visual Studio Code or Visual Studio

## Getting Started 
  - On Solution Folder open console and enter `dotnet restore`
  - Use the Cli example to get started
  - Still working on a Xamarin and Aspnet core Example as well
  - Running the cli, Navigate to InvoiceKit.Cli project and type `dotnet run`  



### Support or Contact
