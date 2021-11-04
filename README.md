


## Book Searcher
A web app for book searches.

* **Author**: Layla Alvey
* **Built with:** .NET Core 3.1


## Instructions

**Prerequisites**: 
*  .NET Core 3.1
* Visual Studio (verified with Visual Studio Professional 2019)
* Node
* Npm

**To run**: 
1. Clone the repo
   ```sh
   git clone https://github.com/codinatrix/BookSearcher.git
   ``` 
   or download from https://github.com/codinatrix/BookSearcher/archive/refs/heads/master.zip
2. Open the `BookSearcher` folder
3. Open `BookSearcher.sln`
4. Press F5 on your keyboard or click on the green play button at the top of Visual Studio
5. http://localhost:9598/api/books/ should open in your browser
6. Open the folder BookSearcher.Web in a command line
7. Run `npm install`
8. Run `npm start`
9. http://localhost:3000/ should open in your browser

## Potential improvements
Improvements that could be made to this app:

 - Ready the app for deploy. This app is not production-ready.
 - More sophisticated error handling in both frontend and backend.
 - Create a Book class in frontend that the json is loaded directly into.
 - Find a way to route single price requests (e.g. /api/books/price/30.0) and price range requests (e.g. /api/books/price/30.0&35.0) into two separate action methods if possible.
 

## Discussion
 
 - The solution currently loads the entire json file into memory to search it. This would be a major performance issue if the data was to get larger, in which case we would want to load this data into a database. Searching a file could be suitable for an MVP with little data, but the code in the generic repository would need to be swapped out if the data gets large enough to require a db. A generic repository with a layer obscuring the data source was chosen to make it simpler to switch to a database with ORM in the future. 
 - The repository exposes IQueryable, a practice which is a source of heated debate, but has the advantage of allowing the service to use linq expressions with deferred execution.
 - It is a questionable requirement to query backend for a range of prices with a url string such as "30.0&35.0". A request such as /api/books/price/range/30.0/35.0 would be more maintainable.

## Contact
* [LinkedIn](https://www.linkedin.com/in/laylaalvey/)
* Project Link: [https://github.com/codinatrix/BookSearcher](https://github.com/codinatrix/WeatherReport)

