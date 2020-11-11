import React, { Component } from 'react';
import axios from 'axios';

export class Register extends Component {
    constructor() {
        super();
        this.state = {
            emailerror: "",
            birthdateerror: "",
            passworderror: "",
            repeatpassworderror: ""
        }
    }

    handleSubmit(event) {
        let email = event.target[0].value;
        let birthdate = event.target[1].value;
        let password = event.target[2].value;
        let repeatpassword = event.target[3].value;

        if (birthdate === null) {
            this.setState({
                birthdateerror: "You must provide a birthdate"
            })
        }

        if (password === "") {
            this.setState({
                passworderror: "You must provide a password"
            });
        };

        if (repeatpassword === "") {
            this.setState({
                repeatpassworderror: "You must repeat your password"
            });
        };

        if (password !== repeatpassword) {
            this.setState({
                repeatpassworderror: "Passwords do not match"
            });
        };

        let userFD = new FormData();
        userFD.append('email', email);
        userFD.append('birthdate', birthdate);
        userFD.append('password', password);
        userFD.append('repeatpassword', repeatpassword);

        const config = {
            headers: { 'content-type': 'multipart/form-data' }
        }

        const user = axios.post("https://localhost:44303/api/users/createuser", userFD, config);

        event.preventDefault();
    }

    render() {
        return (
            <form className="text-center" onSubmit={this.handleSubmit.bind(this)}>
                <div className="form-group">
                    <input className="form-control-sm" type="email" placeholder="Email" name="email" value={this.email} />
                    <p style={{ color: "red" }}>{this.state.emailerror}</p>
                </div>
                <div className="form-group">
                    <input type="date" className="form-control-sm" name="birthdate" value={this.birthdate} />
                </div>
                <div className="form-group">
                    <input className="form-control-sm" type="password" placeholder="Password" name="password" value={this.password} />
                    <p style={{ color: "red" }}>{this.state.passworderror}</p>
                </div>
                <div className="form-group">
                    <input className="form-control-sm" type="password" placeholder="Repeat password" name="repeatpassword" value={this.repeatpassword} />
                    <p style={{ color: "red" }}>{this.state.repeatpassworderror}</p>
                </div>
                <div className="form-group">
                    <input className="btn btn-primary" type="submit" value="Register" />
                </div>
            </form>
        )
    }
}
