import React, { Component } from 'react';
import { fetchUser } from '../store/actions';

export class Login extends Component {
    constructor() {
        super()
        this.state = {
            username: "",
            password: "",
            usernameerror: "",
            passworderror: "",
            loginmessage:""
        }
    }

    handleUsername = (e) => {
        this.setState({
            username: e.value
        })
    }

    handlePassword = (e) => {
        this.setState({
            password: e.value
        })
    }

    handleSubmit = (e) => {
        const username = e.target[0].value;
        const password = e.target[1].value;

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

        const user = fetchUser(loginFD)();

        console.log(user);
        if (user != undefined) {

        }
        else {
            this.setState({
                loginmessage: "Login failed"
            })
        }

        e.preventDefault();
    }

    render() {
        return (
            <form className="text-center" onSubmit={this.handleSubmit.bind(this)}>
                <div className="form-group">
                    <p style={{ color: "red" }} value={this.state.loginmessage}></p>
                </div>
                <div className="form-group">
                    <input type="text" className="form-control-sm" name="username" onChange={this.handleUsername.bind(this)} value={this.state.name} placeholder="Username" />
                    <p style={{ color: "red" }} value={this.state.usernameerror}></p>
                </div>
                <div className="form-group">
                    <input type="password" className="form-control-sm" name="password" onChange={this.handlePassword.bind(this)} value={this.state.password} placeholder="Password" />
                    <p style={{ color: "red" }} value={this.state.passworderror}></p>
                </div>
                <div className="form-group">
                    <input type="submit" className="btn btn-primary" value="Login" />
                </div>
            </form>
            )
    }
}