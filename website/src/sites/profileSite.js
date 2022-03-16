import React, { Component } from 'react';
import AuthService from '../service/authService';
import UserService from '../service/userService';



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
                <p>Hello</p>
            </div>
        )
    }
}
