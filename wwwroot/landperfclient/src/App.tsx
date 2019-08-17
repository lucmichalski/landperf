import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';

class App extends Component {
  state = { data: null };
  async componentDidMount() {
    // url origin will come from a .env file located on each machine I believe.
    const url = process.env.NODE_ENV === 'production' ? '/api/lighthouse/1' : 'https://localhost:5001/api/lighthouse/1';
    const dataResponse = await fetch(url);
    console.log(dataResponse);
    console.log(process.env);
    const data = await dataResponse.json();
    console.log(data);
    this.setState({ data });
  }
  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Edit <code>src/App.tsx</code> and save to reload.
          </p>
          <a className="App-link" href="https://reactjs.org" target="_blank" rel="noopener noreferrer">
            Learn React
          </a>
        </header>
      </div>
    );
  }
}

export default App;
