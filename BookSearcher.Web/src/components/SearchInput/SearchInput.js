const SearchInput = ({
  value,
  onChange,
  onSubmit
}) =>
  <form onSubmit={onSubmit} className="search-input">
    <input 
      className="search-input-field"
      type="text"
      value={value}
      onChange={onChange}
    />
  </form>

export default SearchInput