import Api from './Api'

const HOME_PATH = 'http://localhost:9598/api/books/';

class BookApi {

  getBooks(searchTerm, selectedField, callback) {

    let path = HOME_PATH;

    if (selectedField) {
      path += selectedField + '/';

      if (searchTerm)
        path += searchTerm;
    }

    return Api.get(path, callback);
  }
}

export default new BookApi();