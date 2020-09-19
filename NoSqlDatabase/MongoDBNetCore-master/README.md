# Type of NoSql db

[35+ Use Cases For Choosing Your Next NoSQL Database](http://highscalability.com/blog/2011/6/20/35-use-cases-for-choosing-your-next-nosql-database.html)
[What The Heck Are You Actually Using NoSQL For?](http://highscalability.com/blog/2010/12/6/what-the-heck-are-you-actually-using-nosql-for.html)


```
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