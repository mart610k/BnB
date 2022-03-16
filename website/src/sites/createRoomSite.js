import React, { Component } from 'react';
import RoomService from '../service/roomService';
import FacilityService from '../service/facilityService';

import '../css/createRoomSite.css';
import { useNavigate } from "react-router-dom"

export default function(props){
    const navigation = useNavigate();

    return <CreateRoomSite {...props} history={navigation}/>
}

class CreateRoomSite extends Component {
    
    constructor(props) {
        super(props);
        this.roomService = new RoomService();
        this.facilityService = new FacilityService();

        this.handleChange = this.handleChange.bind(this);
        this.handleFacilityChange= this.handleFacilityChange.bind(this);
        this.RegisterRoom = this.RegisterRoom.bind(this);

        this.state = {
            address : "",
            description: "",
            briefdescription : "",
            price : 0,
            facilities : [],
            allfacilities: []
        }
    }

    async GetAllFacilities(){
        let result = await this.facilityService.RetrieveAllFacilities();
        if(result.status === 401){
        }
        else {
            console.log(result);
            this.setState({
                allfacilities : result
            })
        }
    }

    GetValueFromCookue(cname){
        let name = cname + "=";
        let ca = document.cookie.split(';');
        for(let i = 0; i < ca.length; i++) {
          let c = ca[i];
          while (c.charAt(0) == ' ') {
            c = c.substring(1);
          }
          if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
          }
        }
        return "";
      }

    async RegisterRoom(event){
        event.preventDefault();
        let result = await this.roomService.RegisterRoom(this.state,this.GetValueFromCookue("access_token"));
        console.log(result);
        if(result.statusCode === 401){

        }
        else if (result.status == 403){
            alert("You do not have rights for this endpoint you will now be redirected the base site");
            this.props.history("/");
        }
        else if(result.statusCode === undefined){
            alert("Your room have now been created you will now be redirected to the base site")
            this.props.history("/");
        }
        else {
            alert("unknown issue occured");
            
        }
    }

    handleChange(event){
        this.setState({
            [event.target.name] : event.target.value
        });
    }

    handleFacilityChange(event){

        let facilityList = this.state.facilities;
        
        if(event.target.checked){
            facilityList.push(parseInt(event.target.value));
        }
        else{
            let indexToRemove = facilityList.indexOf(parseInt(event.target.value))
            facilityList.splice(indexToRemove,1);
        }
        this.setState({
            facilities : facilityList
        })

    }

    componentDidMount(){
        this.GetAllFacilities();
    }

    render() {
        const facilities = this.state.allfacilities;
        if(!facilities) return null


        return (
            <div id="createRoom">
                <form onSubmit={this.RegisterRoom}>
                    <h1>Create Room</h1>
                    <label htmlFor="address">Address:</label><br/>
                    <input type="text" required className="InputField" name="address" maxLength="45" value={this.state.address} onChange={this.handleChange} /><br/> 
                        
                    <label htmlFor="description">Description:</label><br/>
                    <textarea type="text" required className="InputField ExpandableHorizontal" name="description" value={this.state.description} onChange={this.handleChange} /><br/>  

                    <label htmlFor="briefdescription">brief description:</label><br/>
                    <textarea type="text" required className="InputField ExpandableHorizontal" name="briefdescription" maxLength="45" value={this.state.briefdescription} onChange={this.handleChange} /><br/>    

                    <label htmlFor="price">Price:</label><br/>
                    <input type="number" required className="InputField" name="price" value={this.state.price} onChange={this.handleChange} /><br/>
                    
                    <label htmlFor="facilities">Facilities:</label><br/>

                    <div id="facilities"> 
                    {facilities.map(facility => (
                            <div>
                                <label htmlFor={"facilityID_" +facility.facilityID}>{facility.facilityName}: </label>
                                <input className="facilityInput" type="checkbox" value={facility.facilityID} name={"facilityID_" +  facility.facilityID} onChange={this.handleFacilityChange} /> <br/>
                            </div>
                            ))
                    }
                    </div> 

                    <button className="SubmitButton" type="submit">Create Room</button>
                </form>
            </div>
        )
    }
}