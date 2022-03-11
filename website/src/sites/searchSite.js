import { Component } from "react";
import React, { useState } from "react";
import RoomService from "../service/roomService";
import FacilityService from "../service/facilityService";
import '../css/search.css';



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
                selectedFacilities : []
            }
        }


        async GetsRoomBySearch(string){   
            console.log(string);
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
            const facilities = this.state.facilities;
            if(facilities.length === 0) return null;
            if(!facilities) return null;
            const checked = this.state.selectedFacilities;
            return (
                <div id="searchBox">
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
            )
        }
}

