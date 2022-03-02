import React, { Component } from 'react';
import TestService from '../service/testService';



export default class TestSite extends Component {

    constructor(props){
        super(props);
        this.testService = new TestService();
        
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
        this.getTest();
    }


    render()
    {
        const version = this.state.version;
        console.log(version);
        if(!version) return null;
        return (
            <div>
                <p>{version}</p>
            </div>
        )
    }
}
