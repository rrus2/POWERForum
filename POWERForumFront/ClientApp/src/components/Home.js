import React, { Component } from 'react';
import axios from 'axios';
import { ThreadComponent } from './ThreadComponent';

export class Home extends Component {
    static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state= {
            threads: []
        }
    }

    componentDidMount() {
        let data;
        const getStuff = async () => {
            let stuff = await axios.get("https://localhost:44303/api/threads")
            data = stuff.data;
        }
        this.setState({
            threads: data
        })
    }

  render () {
      return (
      <div>
         <ThreadComponent props={this.state.threads} />
      </div>
    );
  }
}
