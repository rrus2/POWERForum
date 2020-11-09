import React, { Component } from 'react';

export class ThreadComponent extends Component {
    constructor(props) {
        super(props);
        console.log(this.props);
    }

    render() {
        if (this.props.threads) {
            for (var i = 0; i < this.props.threads.length; i++) {
                return (
                    <h2>{this.props.threads[0].Name}</h2>
                )
            }
        }
        else {
            return (
                <h2>Nothing to see here</h2>
            )
        }
  }
}
