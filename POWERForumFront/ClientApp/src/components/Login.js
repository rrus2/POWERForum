import React, { Component } from 'react';
import { fetchUser } from '../store/actions';

export class Login extends Component {
    constructor() {
        super()
        this.state = {
            username: "",
            password: "",
            usernameerror: "",
            passworderror: ""
        }
    }

    handleSubmit = (e) => {
        username = e.target[0].value;
        password = e.target[1].value;

        if (username === null || username === "") {
            this.setState({
                usernameerror: "username can not be null"
            })
        }

        if (password === null || password === "") {
            this.setState({
                passworderror: "password can not be null"
            })
        }

        let loginFD = new FormData();
        loginFD.append('username', username);
        loginFD.append('password', password);

        const user = fetchUser(loginFD);

        if (user !== null) {

        }
        else {
            alert("Login FAIL");
        }

        e.preventDefault();
    }

    render() {
        return (
            <form className="text-center" onSubmit={this.handleSubmit.bind(this)}>
                <div className="form-group">
                    <input type="text" className="form-control-sm" name="username" value={this.state.name} />
                    <p style={{ color: "red" }} value={this.state.usernameerror}></p>
                </div>
                <div className="form-group">
                    <input type="text" className="form-control-sm" name="password" value={this.state.password} />
                    <p style={{ color: "red" }} value={this.state.passworderror}></p>
                </div>
                <div className="form-group">
                    <input type="submit" className="btn btn-primary" />
                </div>
            </form>
            )
    }
}