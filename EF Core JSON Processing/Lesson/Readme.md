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
Добра практика е да се използват, когато бързо ни трябват да 
прочетем някаква част данни, които да не са публични и 
да не замърсяват глобалния списък с класове 
```
### JSON.NET Attributes
```
Пропъртита с "access modifier (internal or private)" не се 
прочитат от JSON. Не е добра практика да се променят 
"access modifier" само заради това JSON да не го прочита.
Добра практика е да се използат "attributes"
```
### JSON.NET Parsing of Objects
```
Списък с настройки
```

## Newtonsoft.Json vs System.Text.Json
* [Performance comparison(https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-apis/#user-content-systemtextjson-in-aspnet-core-mvc)]
* [Table of differences(https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to#table-of-differences-between-newtonsoftjson-and-systemtextjson)]

