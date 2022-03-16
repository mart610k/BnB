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
            requestText : ""
        }
    }

    async getTest(){
        let result = await this.testService.retrieveTestAPI();
        if(result.status === 401){
            
        }
        else {
            console.log(result);
            this.setState({
                version : result.version
            })
        }

    }

    async requestAsHost(){
        if(this.state.requestText !== ""){
            let result = this.userService.requestAsHost();

            if(result.statusCode === 500){
                alert()
            }
        }
        else{
            alert("The input field is empty")
        }
    }

    componentDidMount(){
    }


    render()
    {
        
        return (
            <div>
                <div id='profileNav'>
                    <div className='navBox'>
                        <p className='navP'>Ændre Password</p>
                    </div>
                    <div className='navBox'>
                    <p className='navP'>Ændre Email</p>
                    </div>
                    <div className='navBox'>
                    <p className='navP'>Ansøg udlejer</p>
                    </div>
                </div>
                <div id='profileMain'>
                    <div className='fullSize'>
                        <label>Request Text:</label><br/>
                        <textarea value={this.state.requestText} onChange={this.handleChange} type="requestText" placeholder="Reasons why you should become a host" style={{"width": "80%", "height" : "50%"}}>
                        </textarea><br/>
                        <button onClick={() => this.requestAsHost()}>Send Request</button>
                    </div>
                    <div className='fullSize'>
                        <h2>Ændre Password</h2>
                        <div className='inputBox'>
                            <label className='profileLabel'>Nuværende Password: </label>
                            <input className='profileInput' type="password" placeholder='Nuværende Password'></input>
                        </div>
                        <div className='inputBox'>
                            <label className='profileLabel'>Nyt Password: </label>
                            <input className='profileInput leftMargin' type="password" placeholder='Nyt Password'></input>
                        </div>
                        <div className='inputBox'>
                            <label className='profileLabel'>Gentag Nyt Password: </label>
                            <input className='profileInput' type="password" placeholder='Gentag Nyt Password'></input>
                        </div>
                    </div>
                    <div className='fullSize'>
                        <h2>Ændre Email</h2>
                        <div className='inputBox'>
                            <label className='profileLabel'>Nuværende Email: </label>
                            <input className='profileInput' type="email" placeholder='Nuværende Email'></input>
                        </div>
                        <div className='inputBox'>
                            <label className='profileLabel'>Nyt Email: </label>
                            <input className='profileInput leftMargin' type="email" placeholder='Ny Email'></input>
                        </div>
                        <div className='inputBox'>
                            <label className='profileLabel'>Gentag Nyt Email: </label>
                            <input className='profileInput' type="email" placeholder='Gentag Ny Email'></input>
                        </div>
                    </div>
                    <div className='fullSize'>

                    </div>
                </div>
            </div>
        )
    }
}
