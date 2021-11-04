import axios from 'axios';

class Api {
  constructor() {
    let service = axios.create({
      headers: {csrf: 'token'}
    });
    service.interceptors.response.use(this.handleSuccess, this.handleError);
    this.service = service;
  }

  handleSuccess(response) {
    return response;
  }

  handleError = (error) => {

    if(!error || !error.response)
      return Promise.reject(new Error('Something went wrong communicating with the server.'))

    return Promise.reject(error)
  }

  get(path, callback) {
    return this.service.get(path).then(
      (response) => callback(response.status, response.data)
    );
  }
}

export default new Api();