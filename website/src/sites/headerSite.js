import { Component } from "react";
import React, { useState } from "react";
import RoomService from "../service/roomService";
import logo from '../logo.svg';


export default function (props) {
    const [show, setShow] = useState(false);
    return <HeaderSite {...props} setShow={setShow} show={show}/>
}
class HeaderSite extends Component{
        constructor(props) {
            super(props);
            this.roomService = new RoomService();

            this.handleChange = this.handleChange.bind(this);

            this.state = {
                searchParam : '',
                rooms : []
            }
        }


        async GetsRoomBySearch(){   
            let result = await this.roomService.RetrieveRoomSearch(this.state.searchParam);
            if(result.status === 401){
                
            }
            else {
                this.setState({
                    rooms : result
                })
            }
    
        }

        handleChange(event){
            this.setState({
                [event.target.name] : event.target.value
            });
        }

        componentDidMount(){
        }
    
        render() {
            
            return (
                <header className="App-header">
                    <div id='LogoBox'>
                        <img id='detailedImage' onClick={() => window.location.href="/"} src={logo}></img>
                    </div>
                    <div id='SearchBox'>
                        <button id="SearchButton" onClick={() => window.location.href="/search"}>Go To Search</button>
                    </div>
                    <div id='LoginBox' onClick={() => window.location.href="/login"}>
                        <img id="loginIcon" src={logo} ></img>
                        <p id="LoginText"> Login</p>
                    </div>
                </header>
            )
        }
}

