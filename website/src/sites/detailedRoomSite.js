import React, { Component } from 'react';
import RoomService from '../service/roomService';
import '../css/room.css';
import missingimage from '../icons/svg/missingimage.svg';
import { useParams, useNavigate } from "react-router-dom";

import { handleChange } from '../helper/reactHelper';

export default function(props){
    const params = useParams();
    const navigation = useNavigate();

    return <DetailedRoomSite {...props} params={params} history={navigation}/>
    
}

class DetailedRoomSite extends Component {
    
    constructor(props) {
        super(props);
        this.roomService = new RoomService();
        this.handleChange = handleChange.bind(this);

        this.state = {
            detailedRoom : {},
            orderStarted : false,
            startingDate : "",
            endingDate : ""
        }
    }

    async GetDetailedRoom(){
        let result = await this.roomService.RetrieveDetailedRoom(this.props.params.RoomID);
        if(result.status === 401){
        }
        else {
            this.setState({
                detailedRoom : result
            })
        }
    }

    GetValueFromCookie(cname){
        let name = cname + "=";
        let ca = document.cookie.split(';');
        for(let i = 0; i < ca.length; i++) {
          let c = ca[i];
          while (c.charAt(0) === ' ') {
            c = c.substring(1);
          }
          if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
          }
        }
        return "";
      }

    OrderStart(){
        this.setState({
            orderStarted : true
        })
    }

    async OrderRoom(){
        this.setState({
            orderStarted : false
        })
        await this.roomService.BookRoom(
            this.state.detailedRoom.roomID,
            this.state.startingDate, 
            this.state.endingDate,
            this.GetValueFromCookie("access_token")
        );       
    }

    componentDidMount(){
        this.GetDetailedRoom();
    }

    render() {
        const room = this.state.detailedRoom;
        if(room.length === 0) return null;
        if(!room) return null;

        const facilities = this.state.detailedRoom.roomFacilities;
        if(!facilities) return null

        const images = this.state.detailedRoom.roomPictures;
        if(images.length === 0) {
            images[0] = missingimage;
        }
        return (
            <div>
                <div className='RoomDetailed' style={this.state.orderStarted ? {"display":"none"} : {"display":"block"}} key={room.roomID}>
                    <div id="detailedImageBox">
                        <img id='detailedImage' src={images[0]} alt="detailed room"></img>
                    </div>
                    <div id='detailedpBox'>
                    <p className='detailedroomP'>Address: {room.roomAddress}</p>
                    <p className='detailedroomP'>Owner: {room.roomOwner}</p>
                    <p className='detailedroomP'>Price: €{room.price}</p>
                    <p className='detailedroomP'>Status: {room.booked ? "Booked": "Available"}</p>
                    </div>
                    <div id='detailedtextBox'>
                        <p className='roomDesc'>{room.roomDesc}</p>
                        <button className='orderButton' onClick={() => this.OrderStart()}>Til Bestilling</button>
                    </div>
                    
                    <div id='detailedfacilities'>
                        <p id="facTitle">Facilities</p>
                        {facilities.map(facility => (
                            <p className='detailedfacilityP'>{facility.facilityName}</p>
                        ))}
                    </div>
                    <div id='smallImageBox'>
                        {images.slice(1).map(img => (
                            <img id='smallImage'src={img} alt="small room"></img>
                        ))}
                    </div>
                </div>
                
                <div className='orderBox' style={this.state.orderStarted ? {"display":"block"} : {"display":"none"}}>
                    <h3>Bestil</h3>
                    <label className='DateLabel'>Start Date:</label>
                    <input className='DateTime' value={this.state.startingDate} name="startingDate" onChange={this.handleChange} type="date"></input>
                    <label className='DateLabel'>End Date:</label>
                    <input className='DateTime' value={this.state.endingDate} name="endingDate" onChange={this.handleChange} type="date"></input>
                    <button className='sendOrder' onClick={() => this.OrderRoom()}>Bestil</button>
                </div>              
            </div>
        )
    }
}