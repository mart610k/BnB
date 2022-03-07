import React, { Component } from 'react';
import RoomService from '../service/roomService';
import '../css/room.css';
import logo from '../logo.svg';
import { useParams } from "react-router-dom"

export default function(props){
    const params = useParams();
    return <DetailedRoomSite {...props} params={params} />
}

class DetailedRoomSite extends Component {
    
    constructor(props) {
        super(props);
        this.roomService = new RoomService();

        this.state = {
            detailedRoom : {},
        }
    }

    async GetDetailedRoom(){
        console.log(this.props.params.RoomID);
        let result = await this.roomService.RetrieveDetailedRoom(this.props.params.RoomID);
        if(result.status === 401){
        }
        else {
            console.log(result)
            this.setState({
                detailedRoom : result
            })
        }
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
            images[0] = logo;
        }

        console.log(facilities);
        return (
            <div>
                <div id='RoomDetailed' key={room.roomID}>
                    <div id="detailedImageBox">
                        <img id='detailedImage' src={images[0]}></img>
                    </div>
                    <div id='detailedpBox'>
                    <p className='detailedroomP'>Address: {room.roomAddress}</p>
                    <p className='detailedroomP'>Owner: {room.roomOwner}</p>
                    <p className='detailedroomP'>Status: {room.roomStatus}</p>
                    </div>
                    <div id='detailedtextBox'>
                        <p className='roomDesc'>{room.roomDesc}</p>
                    </div>
                    
                    <div id='detailedfacilities'>
                        <p id="facTitle">Facilities</p>
                        {facilities.map(facility => (
                            <p className='detailedfacilityP'>{facility}</p>
                        ))}
                    </div>
                    <div id='smallImageBox'>
                        {images.slice(1).map(img => (
                            <img id='smallImage'src={img}></img>
                        ))}
                    </div>
                </div>                
            </div>
        )
    }
}