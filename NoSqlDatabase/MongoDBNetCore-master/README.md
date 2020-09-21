# Type of NoSql db

[35+ Use Cases For Choosing Your Next NoSQL Database](http://highscalability.com/blog/2011/6/20/35-use-cases-for-choosing-your-next-nosql-database.html)

[What The Heck Are You Actually Using NoSQL For?](http://highscalability.com/blog/2010/12/6/what-the-heck-are-you-actually-using-nosql-for.html)


## Key-value

### Use cases
```
Session store
A session-oriented application such as a web application starts
a session when a user logs in and is active until the user 
logs out or the session times out. During this period, 
the application stores all session-related data either in 
the main memory or in a database. Session data may include 
user profile information, messages, personalized data and themes,
recommendations, targeted promotions, and discounts. 
Each user session has a unique identifier. Session data is 
never queried by anything other than a primary key, so a 
fast key-value store is a better fit for session data. 
In general, key-value databases may provide smaller per-page 
overhead than relational databases.
```
```
Shopping cart
During the holiday shopping season, an e-commerce website 
may receive billions of orders in seconds. Key-value databases
can handle the scaling of large amounts of data and extremely 
high volumes of state changes while servicing millions of 
simultaneous users through distributed processing and storage.
Key-value databases also have built-in redundancy, which 
can handle the loss of storage nodes.
```
```
See Hazelcast
```

## Document-OrientedDatabases

Document-oriented databases, or document stores, are NoSQL
databases that store data in the form of documents.
```
Document stores are a type of key-value store:
	Each document has a unique identifier
	Document itself serves as the value
```
```
Usually stored as JSON, XML,
Proto-Buff, etc.
```
### Use cases

Content management
```
A document database is a great choice for content management 
applications such as blogs and video platforms. With a document
database, each entity that the application tracks can be stored
as a single document. The document database is more intuitive 
for a developer to update an application as the requirements 
evolve. In addition, if the data model needs to change, only 
the affected documents need to be updated. No schema update is
required and no database downtime is necessary to make the changes. 
```
Catalogs
```
Document databases are efficient and effective for storing catalog 
information. For example, in an e-commerce application, different 
products usually have different numbers of attributes. Managing 
thousands of attributes in relational databases is inefficient, and 
the reading performance is affected. Using a document database, each 
product’s attributes can be described in a single document for easy 
management and faster reading speed. Changing the attributes of one 
product won’t affect others.
```

## Columnar Databases
```
Columnar databases, are database systems that store data in columns
Each column is stored in a separate file or region in the system’s storage
```
```
Like other NoSQL databases, column-oriented databases are designed to 
scale “out” using distributed clusters of low-cost hardware to increase 
throughput, making them ideal for data warehousing and Big Data processing.
```
```
Column-oriented storage for database tables is an important factor in 
analytic query performance because it drastically reduces the overall 
disk I/O requirements and reduces the amount of data you need to load 
from disk.
```
```
Key benefits of column store databases include faster performance in load, 
search, and aggregate functions
```
```
Column-oriented organizations are more efficient when an aggregate needs 
to be computed over many rows but only for a notably smaller subset of all 
columns of data
```
```
Not efficient when many columns of a row are required at the same time
```
### Type of Columnar Databases

* Apache Cassandra
* MariaDB
* Apache Hbase

## Graph Database
```
Allow simple and fast retrieval of complex hierarchical structures that are 
difficult to model in relational systems
No universal query language is present for graph databases (like SQL). 
Each database has own implementation of queries
```
A graph database contains a collection of nodes and edges
```
A node represents an object
An edge represents the connection or relationship between two objects

```
* Each node is identified by unique identifier that 
  expresses key-value pairs
* Each edge is defined by a unique identifier that details a 
  starting or ending node, along with a set of properties

