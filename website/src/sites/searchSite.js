import { Component } from "react";
import React, { useState } from "react";
import RoomService from "../service/roomService";
import FacilityService from "../service/facilityService";
import '../css/search.css';
import missingimage from "../icons/svg/missingimage.svg";


export default class SearchSite extends Component{
        constructor(props) {
            super(props);
            this.roomService = new RoomService();
            this.facilityService = new FacilityService();

            this.handleChange = this.handleChange.bind(this);

            this.state = {
                searchParam : '',
                rooms : [],
                facilities : [],
                selectedFacilities : [],
                searched : false
            }
        }


        async GetsRoomBySearch(string){   
            this.setState({
                searched : true
            })
            let result = await this.roomService.RetrieveRoomSearch(string);
            if(result.status === 401){
                
            }
            else {
                this.setState({
                    rooms : result
                })
                console.log(this.state.rooms);
            }
        }

        async GetAllFacilities(){
            let result = await this.facilityService.RetrieveAllFacilities();
            if(result.status === 401){
                
            }
            else {
                this.setState({
                    facilities : result
                })
            }
        }

        handleChange = (e) => {
            const value = e.target.value;
            const checkedFacilities = this.state.selectedFacilities;
            if (!checkedFacilities.includes(value)) {
                checkedFacilities.push(value);
            } else {
                checkedFacilities.splice(checkedFacilities.indexOf(value),1);
            }
            this.setState({
                checkedFacilities : checkedFacilities
            })
            // console.log(this.state.checkedFacilities);
        }

        componentDidMount(){
            this.GetAllFacilities()
        }
    
        render() {
            const rooms = this.state.rooms;
            
            if(!rooms) return null;

            const facilities = this.state.facilities;
            if(facilities.length === 0) return null;
            if(!facilities) return null;

            

            const checked = this.state.selectedFacilities;
            return (
                <div>
                    <div className="searchBox" style={this.state.searched ? {"display":"none"} : {"display":"block"}}>
                        <h2>Facilities</h2>
                        <div id="facilityBox">
                            {facilities.map(facility => (
                                <div key={facility.facilityID} id='checkBox'>
                                    <label id='facilityLabel' htmlFor='facilityName'>{facility.facilityName}</label>
                                    <input id='facility' onChange={this.handleChange} name="facilityName" value={facility.facilityID} type={'checkbox'}></input> 
                                </div>
                            ))}
                        </div>
                        <div id="inputBox">
                            {/* <input id='searchInput' onChange={this.handleChange} name="searchParam" value={this.state.searchParam} type="text" placeholder='Søg'></input> */}
                            <button id='simpleSearch' onClick={() => this.GetsRoomBySearch(checked)}>Søg</button>
                        </div>
                    </div>
                    <div className="searchBox" style={this.state.searched ? {"display":"block"} : {"display":"none"}}>
                        <h2>Searched Rooms</h2>
                        {rooms.map(rooms => (
                            <div id='searchRoom' onClick={() => (window.location.href="/room/" + rooms.roomID)} key={rooms.roomID}>
                                <div id="imageBox">
                                    <img id='detailedImage' src={rooms.roomPicture[0] == undefined ? missingimage : ""}></img>
                                </div>
                                <div id='pBox'>
                                <p className='roomP'>Address: {rooms.roomAddress}</p>
                                <p className='roomP'>Owner: {rooms.roomOwner}</p>
                                <p className='roomP'>Price: €{rooms.price}</p>
                                <p className='roomP'>Status: {rooms.booked ? "Booked": "Available"}</p>
                                </div>
                                <div id='textBox'>
                                    <p className='roomBriefDesc'>{rooms.roomDesc}</p>
                                </div>
                                <div id="Facilities">
                                    <p id="simplefacTitle">Facilities</p>
                                    {rooms.facilities.map(facility => (
                                        <p className='simplefacilityP'>{facility.facilityName}</p>
                                    ))}
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
            )
        }
}

