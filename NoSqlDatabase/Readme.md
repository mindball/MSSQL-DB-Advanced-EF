# NoSQL and MongoDB 

## Relational and Non-Relational Database

## Database Scalability

## CAP Theorem

## Distributed Systems

## NoSQL database types

## MongoDB Overview

## CRUD Operations

## Some features with NoSql database:

### Replication 

[Data Replication In NoSQL Databases](http://highscalability.com/blog/2012/7/9/data-replication-in-nosql-databases.html)
```
Replication in computing involves sharing information so as to ensure 
consistency between redundant resources, such as software or hardware 
components, to improve reliability, fault-tolerance, or accessibility.

A replica set is a group of mongod instances that maintain the
same data set

If the primary is unavailable, an eligible secondary will hold an
ELECTION!!!!! to elect itself the new primary

All reads and writes happen from the primary (configurable)
```

Heartbeats
```
Replica set members send heartbeats (pings) to each other every two seconds. 
If a heartbeat does not return within 10 seconds, the other members 
mark the delinquent member as inaccessible.
```

### Sharding

NoSQL databases scale through Sharding, an approach that breaks large data 
pools into more manageable units.

```
Sharding is a method for distributing data across multiple
machines
```

```
NoSQL databases can perform better than traditional databases in 
cases of very large volumes of information that only need to be dealt 
with simply. One example is Twitter, which stores many Tweets but doesn’t 
have to access or manipulate them with complex queries. Additionally, 
they’re much easier to scale, through Sharding, since each shard can be 
stored on a different database server, allowing for the dynamic 
provisioning of additional resources.
```





