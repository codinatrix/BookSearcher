import React, { Component } from 'react';
import BookList from './components/BookList/BookList';
import FieldSelect from './components/FieldSelect/FieldSelect';
import SearchInput from './components/SearchInput/SearchInput'
import BookApi from './api/BookApi'
import './App.scss';

const DEFAULT_SEARCH_TERM = 'B01';

class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      books: [],
      searchTerm: DEFAULT_SEARCH_TERM,
      selectedField: '',
      error: null,
      isLoading: false,
    };

    this.setBooks = this.setBooksOnState.bind(this);
    this.fetchBooks = this.fetchBooks.bind(this);
    this.onSearchChange = this.onSearchChange.bind(this);
    this.onSearchSubmit = this.onSearchSubmit.bind(this);
  }

  setBooksOnState(books) {
    this.setState({ books: books });
  }

  fetchBooks(searchTerm) {
    const { selectedField } = this.state;
    this.setState({ isLoading: true });
    this.setState({ error: null })

    BookApi.getBooks(searchTerm, selectedField,
        (status, data) => {
          this.setBooksOnState(data)
          this.setState({ isLoading: false });
        }
    )
    .catch(
      (error) => {
        if (error?.response?.status === 400 || error?.response?.status === 404)
          error = "Sorry, we don't support that kind of search."
        else
          error = "Something went wrong."

        this.setState({ error })
        this.setState({ isLoading: false });
      }
    )    
  }

  getDropdownSelection = (selectedField) =>{
    this.setState({selectedField: selectedField})
  }

  onSearchChange(event) {
    this.setState({ searchTerm: event.target.value });
  }

  onSearchSubmit(event) {
    const { searchTerm } = this.state;
    this.setState({ searchTerm: searchTerm });
    this.fetchBooks(searchTerm);

    event.preventDefault();
  }

  componentDidMount() {
    const { searchTerm } = this.state;
    this.fetchBooks(searchTerm);
  }

  render() {
    const {
      searchTerm,
      books,
      error,
      isLoading
    } = this.state;

    return (
      <div className="page">
        <div className="search-container">
          <FieldSelect onSelectedFieldChange = { this.getDropdownSelection } />
          <SearchInput
            value={searchTerm}
            onChange={this.onSearchChange}
            onSubmit={this.onSearchSubmit}/>
          <div className="search-help-text">
            Try searching title "Jruby", price "30.0&35.0" or published "2012/8/15"
          </div>
        </div>
        { isLoading
            ? 
              <div>Loading ...</div>
            : 
            <div>
            { error
              ? 
                <div>
                  <p>{error}</p>
                </div>
              : <BookList
                books={books}/>
            }
          </div>
          }
      </div>
    );
  }
}

export default App;