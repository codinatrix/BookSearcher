import React, { Component } from 'react';

class BookList extends Component {
  render() {
    const {
      books
    } = this.props;

    return (
      <div className="book-list">
      { !books.length && <div>We don't have any books like that. Try for example selecting "genre" and searching "fantasy".</div> }
      {books.map(book =>
        <div key={book.id} className="book-list-item">
          <span className="book-id">
            {book.id}: 
          </span>
          <span className="book-title">
            {book.title}
          </span>
          <span className="book-author">
            {book.author}
          </span>
          <span className="book-genre">
            {book.genre}
          </span>
          <span >
          {book.price} kr
          </span>
          <span className="book-published">
          {new Date(book.published).toDateString()}
          </span>
          <div >
          {book.description}
          </div>
        </div>
      )}
    </div>
    );
  }
}

export default BookList;