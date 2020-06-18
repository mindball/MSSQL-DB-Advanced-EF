# Data Access Models
```
ADO.NET supports two different programming environments: connected and disconnected.
```
## Connected Model
```
Applicable to an environment where the database is constantly available
```
### Benefits:
```
* Concurrency control is easier to maintain
* Better chance to work with the most recent version of the data
```
### Drawbacks:
```
Needs a constant reliable network
Problems when scalability is an issue
```
#ADO.NET Architecture
## ADO.NET is a standard .NET class library for accessing databases, processing data and XML
## Supports connected, disconnected and ORM data access models
```
Excellent integration with LINQ
Allows executing SQL in RDBMS systems
Allows accessing data in the ORM approach
```
## Data Providers in ADO.NET
### Data Providers are collections of classes that provide access to various databases
```
For different RDBMS systems different Data Providers are available
```
### Several common objects are defined
```
Connection – to connect to the database
Command – to run an SQL command
DataReader – to retrieve data
```
### Data Providers in ADO.NET 
```
Several standard ADO.NET Data Providers come as part of .NET Framework:
	* SqlClient – accessing SQL Server
	* OleDB – accessing standard OLE DB data sources
	* Odbc – accessing standard ODBC data sources
	* Oracle – accessing Oracle databases
```
### Third party Data Providers are available for:
```
MySQL, PostgreSQL, Interbase, DB2, SQLite
Other RDBMS systems and data sources:
	* SQL Azure, Salesforce CRM, Amazon SimpleDB, ...
```
## Disconnected Model
```
The DataSet object is central to supporting disconnected,
distributed data scenarios with ADO.NET
```
```
DataSet is a memory-resident representation of data that
provides a consistent relational programming model regardless 
of the data source
```
## ORM Model
### ORM data access model (Entity Framework Core)
```
Maps database tables to classes and objects
Objects can be automatically persisted in the database
Can operate in both connected and disconnected modes
```
### ORM model benefits
```
Less code
Use objects with associations instead of tables and SQL
Integrated object query mechanism
```
### ORM model drawbacks:
```
Less flexibility
SQL is automatically generated
Performance issues (sometimes
```
### Entity Framework Core is a generic ORM framework
```
Create entity data model mapping the database
Open an object context
Retrieve data with LINQ / modify the tables in the object context
Persist the object context changes into the DB
Connection is automatically managed
```
#Accessing SQL Server from ADO.NET
## SqlConnection
```
Establish database connection to SQL Server 
```
## SqlCommand
```
Executes SQL commands on the SQL Server through an established connection
Could accept parameters (SQLParameter)
```
## SqlDataReader
```
Retrieves data (record set) from SQL Server as a result of SQL query execution
```
#SQL Injection
## Preventing SQL Injection
### SQL-escape all data coming from the user
```
Not recommended: use as last resort only!
```
### Preferred approach
```
Use parameterized queries
Separate the SQL command from its arguments
```












