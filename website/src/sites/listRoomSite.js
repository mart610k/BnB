import React, { Component } from 'react';
import RoomService from '../service/RoomService';



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
            console.log(result);
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
        console.log(rooms);
        return (
            <div>
                {rooms.map(rooms => (
                    <div key={rooms.roomID}>
                        <p>Room Address: {rooms.roomAddress}</p>
                        <p>Room Owner: {rooms.roomOwner}</p>
                        <p>Room Status: {rooms.roomStatus}</p>
                    </div>
                ))}
                
            </div>
        )
    }
}
