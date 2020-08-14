# JSON PROCESSING
## JSON Data Format
### JSON (JavaScript Object Notation) is a lightweight data format
### Human and machine-readable plain text
### Based on JavaScript objects
### Independent of development platforms and languages
### JSON data consists of:
```
Values (strings, numbers, etc.)
Key-value pairs: { key : value }
Arrays: [value1, value2,
```
## Configuring JSON.NET
### Deserializing to anonymous types
```
Äîáðà ïðàêòèêà å äà ñå èçïîëçâàò, êîãàòî áúðçî íè òðÿáâàò äà 
ïðî÷åòåì íÿêàêâà ÷àñò äàííè, êîèòî äà íå ñà ïóáëè÷íè è 
äà íå çàìúðñÿâàò ãëîáàëíèÿ ñïèñúê ñ êëàñîâå 
```
### JSON.NET Attributes
```
Ïðîïúðòèòà ñ "access modifier (internal or private)" íå ñå 
ïðî÷èòàò îò JSON. Íå å äîáðà ïðàêòèêà äà ñå ïðîìåíÿò 
"access modifier" ñàìî çàðàäè òîâà JSON äà íå ãî ïðî÷èòà.
Äîáðà ïðàêòèêà å äà ñå èçïîëçàò "attributes"
```
### JSON.NET Parsing of Objects
```
Ñïèñúê ñ íàñòðîéêè
```

## Newtonsoft.Json vs System.Text.Json
* [Performance comparison]
* [Table of differences]

 [Performance comparison]: <https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-apis/#user-content-systemtextjson-in-aspnet-core-mvc>
 [Table of differences]: <https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to#table-of-differences-between-newtonsoftjson-and-systemtextjson>
