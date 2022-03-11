import { Component } from "react";
import React, { useState } from "react";
import RoomService from "../service/roomService";


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
          
                    </div>
                    <div id='SearchBox'>
                    <button onClick={() => window.location.href="/search"}>Go To Search</button>
                    </div>
                    <div id='LoginBox'>
                    
                    </div>
                </header>
            )
        }
}

