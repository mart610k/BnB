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
            version : null,
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

    componentDidMount(){
        //this.getTest();
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
