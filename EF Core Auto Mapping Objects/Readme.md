# C# Auto Mapping Objects
## Data Transfer Objects
### A DTO is an object that carries data between processes
```
Used to aggregate only the needed information in a single call
Example: In web applications, between the server and client
```
### Doesn't contain any logic – only stores values
### DTO Usage Scenarios
```
Hide particular properties that clients are not supposed to view
```
```
Remove circular references
```
```
Omit some properties in order to reduce payload size
```
```
Flatten object graphs that contain nested objects to make
them more convenient for clients
```
```
Decouple your service layer from your database layer
```

## AutoMapper Library
```
Automatic Translation of Domain Objects.
Library to eliminate manual mapping code
```
### Multiple Mappings
### Custom Member Mapping
### Flattening Complex Properties
```
Complex objects can be flattened to fractions
of their sizes
```
### Unflattening Complex Objects
### Mapping Profiles
### Mapping Collections
### Inheritance Mapping