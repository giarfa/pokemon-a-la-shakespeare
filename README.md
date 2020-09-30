# Pokemon à la Shakespeare
A small project for integration between APIs of:
- https://pokeapi.co/
- https://funtranslations.com/api/shakespeare

### Requirements
.net core 3.1 or higher is required  
https://dotnet.microsoft.com/download

### Build instructions
```
cd pokemon-a-la-shakespeare  
dotnet build  
```

### Run instructions
```
dotnet run  
```
website will should available on localhost:5000  
try it with some pokemons like:  
- http://localhost:5000/pokemon/pikachu
- http://localhost:5000/pokemon/ditto

### Tests instructions
Please start with non manual tests
```
dotnet test --filter TestCategory!=ManualTest  
```
  
After non manual tests you can run ManualTests
```
dotnet test --filter TestCategory=ManualTest  
```
Please keep in mind that due to an api usage limit of funtranslations.com you should not be able to get new Shakespeare translations for about one hour.  
Final recommendation is to first play with the website and then run tests in the specified order.