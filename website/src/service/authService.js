import EnvironmentService from "./environmentService";

export default class RoomService{
    constructor(){
        this.environmentService = new EnvironmentService();
    }

    async RetrieveAccesstoken(data = {}) {
        
        var basicheader = "Basic " + btoa("FS?[s(?%C4X$&%)A:}_3}3LV*(Vsjr?S.");  

        let result = await fetch(this.environmentService.getEnvironmentHost() + "/api/auth/token",{
            method : "POST",
            headers: {
                "Content-Type": "multipart/form-data",
                "authorization": basicheader 
            },
            body: "grant_type=" + data.grant_type + "&username=" + data.username + "&password=" + data.password  
        });

        let responseOK = result && result.ok;

        if(responseOK){
            let data = await result.json();
            return data;
        } else {
            return result;
        }
    }
}