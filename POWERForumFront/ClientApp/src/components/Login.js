import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';
import { userHasAuthenticated, useAppContext } from '../lib/contextLib';
import axios from 'axios';

export default function Login() {
    const history = useHistory();
    const { userHasAuthenticated } = useAppContext();
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    let passworderror = "";
    let usernameerror = "";
    let loginmessage = "";

    function handleSubmit(e) {
        const username = e.target[0].value;
        const password = e.target[1].value;

        if (username === null || username === "") {
            document.getElementById("usernameerror").innerHTML = "Username can not be null";
        }

        if (password === null || password === "") {
            document.getElementById("passworderror").innerHTML = "Password can not be null";
        }

        let loginFD = new FormData();
        loginFD.append('username', username);
        loginFD.append('password', password);

        const config = {
            headers: { 'content-type': 'multipart/form-data' }
        }

        const user = null;
        axios.post("https://localhost:44303/api/users/loginuser", loginFD, config).then(user => user);

        console.log(user);
        if (user) {
            userHasAuthenticated(true);
            history.push("/");
        }
        else {
            loginmessage = "Login failed";
            document.getElementById("loginmessage").innerHTML = "Login failed";
        }

        e.preventDefault();
    }

    return (
        <form className="text-center" onSubmit={handleSubmit.bind(this)}>
            <div className="form-group">
                <p id="loginmessage" style={{ color: "red" }} value={loginmessage}></p>
            </div>
            <div className="form-group">
                <input type="text" className="form-control-sm" name="username" onChange={e => setUsername(e.target.value)} value={username} placeholder="Username" />
                <p id="usernameerror" style={{ color: "red" }} value={usernameerror}></p>
            </div>
            <div className="form-group">
                <input type="password" className="form-control-sm" name="password" onChange={e => setPassword(e.target.value)} value={password} placeholder="Password" />
                <p id="passworderror" style={{ color: "red" }} value={passworderror}></p>
            </div>
            <div className="form-group">
                <input type="submit" className="btn btn-primary" value="Login" />
            </div>
        </form>
    )
}