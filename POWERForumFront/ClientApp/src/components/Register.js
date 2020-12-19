import React, { useState } from 'react';
import axios from 'axios';
import { useHistory } from 'react-router-dom';
import { userHasAuthenticated, useAppContext } from '../lib/contextLib';

export default function Register() {
    const history = useHistory();
    const {userHasAuthenticated} = useAppContext();
    const [email, setEmail] = useState("");
    const [date, setDate] = useState("");
    const [password, setPassword] = useState("");
    const [repeatpassword, setRepeatpassword] = useState("");

    function handleSubmit(event) {
        const email = event.target[0].value;
        const birthdate = event.target[1].value;
        const password = event.target[2].value;
        const repeatpassword = event.target[3].value;

        if (birthdate === null) {
            document.getElementById("dateerror").innerHTML = "Birthdate can not be null";
        }

        if (password === "") {
            document.getElementById("passworderror").innerHTML = "Password can not be null";
        };

        if (birthdate > Date.now()) {
            document.getElementById("dateerror").innerHTML = "Birthdate can not be larger than today";
        }
        if (repeatpassword === "") {
            document.getElementById("repeatpassworderror").innerHTML = "Repeat password can not be null";
        };

        if (password !== repeatpassword) {
            document.getElementById("repeatpassworderror").innerHTML = "Passwords must match";
        };

        let userFD = new FormData();
        userFD.append('email', email);
        userFD.append('birthdate', birthdate);
        userFD.append('password', password);
        userFD.append('repeatpassword', repeatpassword);

        const config = {
            headers: { 'content-type': 'multipart/form-data' }
        }

        const user = null;
        axios.post("https://localhost:44303/api/users/createuser", userFD, config)
            .then(x => { user = x.data })
            .catch(function (e) {
                for (var i = 0; i < e.response.data.length; i++) {
                    document.getElementById("registererror").innerHTML += e.response.data[i].description + "<br />";
                }
                console.log(e.response.data);
            });
        console.log(user);
        if (user) {
            userHasAuthenticated(true);
            history.push("/");
        }
        else {
            document.getElementById("registererror").innerHTML = "Registration failed for some reason <br />";
        }

        event.preventDefault();
    }

    return (
        <form className="text-center" onSubmit={handleSubmit.bind(this)}>
            <div className="form-group">
                <p style={{ color: "red" }} id="registererror"></p>
            </div>
            <div className="form-group">
                <input className="form-control-sm" type="email" placeholder="Email" value={email} name="email" onChange={e => setEmail(e.target.value)} />
                <p style={{ color: "red" }} id="emailerror"></p>
            </div>
            <div className="form-group">
                <input type="date" className="form-control-sm" value={date} name="birthdate" onChange={e => setDate(e.target.value)} />
                <p style={{ color: "red" }} id="dateerror"></p>
            </div>
            <div className="form-group">
                <input className="form-control-sm" type="password" value={password} placeholder="Password" name="password" onChange={e => setPassword(e.target.value)} />
                <p style={{ color: "red" }} id="passworderror"></p>
            </div>
            <div className="form-group">
                <input className="form-control-sm" type="password" value={repeatpassword} placeholder="Repeat password" name="repeatpassword" onChange={e => setRepeatpassword(e.target.value)} />
                <p style={{ color: "red" }} id="repeatpassworderror"></p>
            </div>
            <div className="form-group">
                <input className="btn btn-primary" type="submit" value="Register" />
            </div>
        </form>
    )
}
