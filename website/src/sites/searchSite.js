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
                facilities : []
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

        handleChange(event){
            this.setState({
                [event.target.name] : event.target.value
            });
        }

        componentDidMount(){
            this.GetsRoomBySearch()
            this.GetAllFacilities()
        }
    
        render() {
            const facilities = this.state.facilities;
            if(facilities.length === 0) return null;
            if(!facilities) return null;

            return (
                <div id="searchBox">
                    <h2>Facilities</h2>
                    <div id="facilityBox">
                        {facilities.map(facility => (
                            <div id='checkBox'>
                                <label id='facilityLabel' htmlFor='facility'>{facility.facilityName}</label>
                                <input id='facility' type={'checkbox'}></input> 
                            </div>
                        ))}
                    </div>
                    <div id="inputBox">
                        {/* <input id='searchInput' onChange={this.handleChange} name="searchParam" value={this.state.searchParam} type="text" placeholder='Søg'></input> */}
                        <button id='simpleSearch' onClick={() => this.GetsRoomBySearch()}>Søg</button>
                    </div>
                </div>
            )
        }
}

