import React, { Component } from 'react';
import AuthService from '../service/authService';
import UserService from '../service/userService';
import "../css/profile.css";

import { handleChange } from '../helper/reactHelper';

export default class ProfileSite extends Component {

    constructor(props){
        super(props);
        this.userService = new UserService();
        this.authservice = new AuthService();

        this.requestAsHost = this.requestAsHost.bind(this);
        this.handleChange = handleChange.bind(this);
        
        this.state = {
            oldPassword : "",
            newPassword : "",
            confirmedNewPassword : "",
            oldEmail : "",
            newEmail : "",
            confirmedNewEmail : "",
            requestText : ""
        }
    }


    componentDidMount(){

    }

    GetValueFromCookie(cname){
        let name = cname + "=";
        let ca = document.cookie.split(';');
        for(let i = 0; i < ca.length; i++) {
          let c = ca[i];
          while (c.charAt(0) == ' ') {
            c = c.substring(1);
          }
          if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
          }
        }
        return "";
      }

    async requestAsHost(){
        if(this.state.requestText !== ""){
            let result = await this.userService.requestAsHost({RequestText : this.state.requestText},this.GetValueFromCookie("access_token"));

            if(result.statusCode === 500){
                alert()
            }
            else if (result.statusCode === 409){
                alert("You already have a request to become a host outstanding you cant create another");
            }
            
        }
        else{
            alert("The input field is empty")
        }
    }

    async UpdatePassword(){
            await this.userService.UpdatePassword(
                this.state.oldPassword,
                this.state.newPassword,
                this.state.confirmedNewPassword,
                this.GetValueFromCookie("access_token")
            );
    }

    async UpdateEmail(){
        await this.userService.UpdateEmail(
            this.state.oldEmail,
            this.state.newEmail,
            this.state.confirmedNewEmail,
            this.GetValueFromCookie("access_token")
        );
    }

    render()
    {
        return (
            <div>
                <div id='profileNav'>
                    <div className='navBox'>
                        <a href="#Password" className='navA'>Ændre Password</a>
                    </div>
                    <div className='navBox'>
                        <a href='#Email' className='navA'>Ændre Email</a>
                    </div>
                    <div className='navBox'>
                        <a href='#Udlej' className='navA'>Ansøg udlejer</a>
                    </div>
                </div>
                <div id='profileMain'>
                    
                    <div id="Password" className='fullSize'>
                        <h2>Ændre Password</h2>
                        <div className='inputBox'>
                            <label className='profileLabel'>Nuværende Password: </label>
                            <input className='profileInput' value={this.state.oldPassword} name="oldPassword" onChange={this.handleChange} type="password" placeholder='Nuværende Password'></input>
                        </div>
                        <div className='inputBox'>
                            <label className='profileLabel'>Nyt Password: </label>
                            <input className='profileInput leftMargin' value={this.state.newPassword} name="newPassword" onChange={this.handleChange} type="password" placeholder='Nyt Password'></input>
                        </div>
                        <div className='inputBox'>
                            <label className='profileLabel'>Gentag Nyt Password: </label>
                            <input className='profileInput' onChange={this.handleChange} name="confirmedNewPassword" value={this.state.confirmedNewPassword} type="password" placeholder='Gentag Nyt Password'></input>
                        </div>
                        <button className='profileButton' onClick={() => this.UpdatePassword()}>Gem</button>
                    </div>
                    <div id='Email' className='fullSize'>
                        <h2 className='profileH2'>Ændre Email</h2>
                        <div className='inputBox'>
                            <label className='profileLabel'>Nuværende Email: </label>
                            <input className='profileInput' onChange={this.handleChange} name="oldEmail" value={this.state.oldEmail} type="email" placeholder='Nuværende Email'></input>
                        </div>
                        <div className='inputBox'>
                            <label className='profileLabel'>Nyt Email: </label>
                            <input className='profileInput leftMargin' onChange={this.handleChange} name="newEmail" value={this.state.newEmail} type="email" placeholder='Ny Email'></input>
                        </div>
                        <div className='inputBox'>
                            <label className='profileLabel'>Gentag Nyt Email: </label>
                            <input className='profileInput' onChange={this.handleChange} name="confirmedNewEmail" value={this.state.confirmedNewEmail} type="email" placeholder='Gentag Ny Email'></input>
                        </div>
                        <button className='profileButton' onClick={() => this.UpdateEmail()}>Gem</button>
                    </div>
                    <div id="Udlej" className='fullSize'>
                        <label>Request Text:</label><br/>
                        <textarea value={this.state.requestText} name="requestText" onChange={this.handleChange} type="requestText" placeholder="Reasons why you should become a host" style={{"width": "80%", "height" : "50%"}}>
                        </textarea><br/>
                        <button onClick={() => this.requestAsHost()}>Send Request</button>
                    </div>
                </div>
            </div>
        )
    }
}
