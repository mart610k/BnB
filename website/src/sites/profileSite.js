import React, { Component } from 'react';
import AuthService from '../service/authService';
import UserService from '../service/userService';
import "../css/profile.css";



export default class ProfileSite extends Component {

    constructor(props){
        super(props);
        this.userService = new UserService();

        this.authservice = new AuthService();
        
        this.state = {
            
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
                    <div id='Password' className='fullSize'>
                        <h2 className='profileH2'>Ændre Password</h2>
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
                        <button className='profileButton'>Gem</button>
                    </div>
                    <div id='Email' className='fullSize'>
                        <h2 className='profileH2'>Ændre Email</h2>
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
                        <button className='profileButton'>Gem</button>
                    </div>
                    <div className='fullSize'>

                    </div>
                </div>
            </div>
        )
    }
}
