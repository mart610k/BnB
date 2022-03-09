import React, { Component } from 'react';
import RoomService from '../service/roomService';
import '../css/room.css';

export default class ListRoomSite extends Component {

    constructor(props){
        super(props);
        this.roomService = new RoomService();

        this.state = {
            rooms : []
        }   
    }

    async getSimpleRooms(){
        let result = await this.roomService.RetrieveSimpleRooms();
        if(result.status === 401){
            
        }
        else {
            this.setState({
                rooms : result
            })
        }

    }

    componentDidMount(){
        this.getSimpleRooms();
    }
 

    render()
    {
        const rooms = this.state.rooms;
        if(rooms.length === 0) return null;
        if(!rooms) return null;
        return (
            <div>
                {rooms.map(rooms => (
                    
                    <div id='Room' onClick={() => (window.location.href="/room/" + rooms.roomID)} key={rooms.roomID}>
                        <div id="imageBox">

                        </div>
                        <div id='pBox'>
                        <p className='roomP'>Address: {rooms.roomAddress}</p>
                        <p className='roomP'>Owner: {rooms.roomOwner}</p>
                        <p className='roomP'>Status: {rooms.roomStatus}</p>
                        </div>
                        <div id='textBox'>
                            <p className='roomBriefDesc'>{rooms.roomDesc}</p>
                        </div>
                        <div id="Facilities">
                            
                        </div>
                    </div>
                    
                    
                ))}
                
            </div>
        )
    }
}
