import React, { Component } from 'react';

class FieldSelect extends Component {
  constructor(props) {
    super(props);
    this.state = {selectedField: 'id'};
    this.handleChange = this.handleChange.bind(this);

    this.initSelectedFieldOnProps();
  }

  initSelectedFieldOnProps() {
    const { selectedField } = this.state;
    this.props.onSelectedFieldChange(selectedField);
  }

  handleChange(event) {
    let selection = event.target.value;
    this.props.onSelectedFieldChange(selection);
    this.setState({selectedField: selection});
  }

  render() {
    const selectedfield = this.props.selectedField;
    return (
      <div className="field-select">
        <select value={selectedfield} onChange={this.handleChange}>
          <option value="id">id</option>
          <option value="author">author</option>
          <option value="title">title</option>
          <option value="genre">genre</option>
          <option value="price">price</option>
          <option value="published">published</option>
          <option value="description">description</option>
        </select>
      </div>
    );
  }
}

export default FieldSelect;