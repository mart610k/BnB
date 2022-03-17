import React, { Component } from 'react';
import RoomService from '../service/roomService';
import '../css/room.css';
import missingimage from "../icons/svg/missingimage.svg";

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
            <div id='topbox'>
                <h2>Rooms</h2>
                {rooms.map(room => (
                    
                    <div key={"room_" + room.roomID} id='Room' onClick={() => (window.location.href="/room/" + room.roomID)}>
                        <div id="imageBox">
                        <img id='detailedImage' alt="room" src={room.roomPicture[0] === undefined ? missingimage : ""}></img>
                        </div>
                        <div id='pBox'>
                        <p className='roomP'>Address: {room.roomAddress}</p>
                        <p className='roomP'>Owner: {room.roomOwner}</p>
                        <p className='roomP'>Price: €{room.price}</p>
                        <p className='roomP'>Status: {room.booked ? "Booked": "Available"}</p>
                        </div>
                        <div id='textBox'>
                            <p className='roomBriefDesc'>{room.roomDesc}</p>
                        </div>
                        <div id="Facilities">
                            <p id="simplefacTitle">Facilities</p>
                            {room.facilities.map(facility => (
                                <p key={"facilityID_" + facility.facilityID } className='simplefacilityP'>{facility.facilityName}</p>
                            ))}
                        </div>
                    </div>
                    
                    
                ))}
                
            </div>
        )
    }
}
