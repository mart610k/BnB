
import React, { Component } from 'react';
import { useNavigate } from "react-router-dom";
import "../css/loginSite.css";
import AuthService from "../service/authService";
import {
    Link
}
from "react-router-dom";

// Wrap and export
export default function(props) {
    const navigation = useNavigate();
  
    return <LoginSite {...props} history={navigation} />;
  }


class LoginSite extends Component {
    constructor(props){
        super(props);
    
        this.authService = new AuthService();

        this.handleChange = this.handleChange.bind(this);
        this.getAccessToken = this.getAccessToken.bind(this);


        this.state = {
            usernameInput: "",
            passwordInput: ""
        }
    }


    async getAccessToken(event){
        event.preventDefault();

        let result = await this.authService.RetrieveAccesstoken({grant_type:"password", username: this.state.usernameInput, password: this.state.passwordInput});

        console.log(result);
        document.cookie = "access_token="+ result.access_token //+ ";refesh_token=" + result.refresh_token + ";expires_in=" + result.expires_in + ";"
        document.cookie = "refesh_token=" + result.refresh_token;
        let date = new Date();
        date.setSeconds(date.getSeconds() + result.expires_in);
        document.cookie = "token_expires=" + date;
        
        this.props.history("/");

    }

    handleChange(event){
        this.setState({
            [event.target.name] : event.target.value
        });
    }

    render()
    {
        return (
            <div id="loginUser">
                <form onSubmit={this.getAccessToken}>
                    <h1>Log in</h1>

                    <label htmlFor='usernameInput'>Username:</label>
                    <input className='InputField' type="text" name='usernameInput' value={this.state.usernameInput} onChange={this.handleChange} placeholder="Username" required/>

                    <label htmlFor="passwordInput" >Password:</label>
                    <input type={this.state.showpass ? "text" : "password"} className="InputField" name="passwordInput" value={this.state.passwordInput} onChange={this.handleChange} placeholder="Password" required />
                    

                    <button className="SubmitButton" type="submit" >Login</button>
                    
                    <label><Link to="/register">Got no account yet? Create one here</Link></label><br/>

                </form>
            </div>
        )
    }
}