import { Component } from "react";
import React, { useState } from "react";
import UserService from "../service/userService";
import login from '../icons/svg/login.svg';
import logo from '../icons/svg/logo.svg';
import user from '../icons/svg/user.svg';
import AuthService from "../service/authService";

import { handleChange } from '../helper/reactHelper';

export default function (props) {
    const [show, setShow] = useState(false);
    return <HeaderSite {...props} setShow={setShow} show={show}/>
}

class HeaderSite extends Component{
        constructor(props) {
            super(props);
            this.userService = new UserService();
            this.authService = new AuthService();

            this.handleChange = handleChange.bind(this);

            this.state = {
                searchParam : '',
                rooms : [],
                validToken : false
            }
        }

        getValueFromCookie(cname){
            let name = cname + "=";
            let ca = document.cookie.split(';');
            for(let i = 0; i < ca.length; i++) {
              let c = ca[i];
              while (c.charAt(0) === ' ') {
                c = c.substring(1);
              }
              if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
              }
            }
            return "";
          }

        async getValidAccessToken (){

            let valid = await this.authService.checkAndUpdateAccessToken(this.getValueFromCookie("access_token"),this.getValueFromCookie("refresh_token"),this.getValueFromCookie("token_expires"));

            this.setState({
                validToken : valid
            });
        }

        componentDidMount(){
            this.getValidAccessToken();
        }
    
        render() {
            const loggedInStatus = this.state.validToken;


            return (
                <header className="App-header">
                    <div id='LogoBox'>
                        <img id='sitelogo' alt="site logo" onClick={() => window.location.href="/"} src={logo}></img>
                    </div>
                    <div id='SearchBox'>
                        <button id="SearchButton" onClick={() => window.location.href="/search"}>Go To Search</button>
                    </div>
                   
                    <div id="profileBox"onClick={() => window.location.href="/profile"}>
                        <img id="profileIcon" alt="profile" src={user}></img>
                        <p id="ProfileText">Username</p>
                    </div>
                    <div id='LoginBox' onClick={() => window.location.href="/login"}>
                        <img id="loginIcon" alt="Login" src={login} ></img>
                        <p id="LoginText">{ loggedInStatus === true ? "Logout" : "Login"}</p>
                    </div>
                   
                </header>
            )
        }
}

