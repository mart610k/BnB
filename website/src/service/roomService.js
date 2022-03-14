import EnvironmentService from "./environmentService";

export default class RoomService{
    constructor(){
        this.environmentService = new EnvironmentService();
    }

    async RetrieveSimpleRooms() {
        let result = await fetch(this.environmentService.getEnvironmentHost() + "/api/Room/list");

        let responseOK = result && result.ok;

        if(responseOK){
            let data = await result.json();
            return data;
        } else {
            return result;
        }
    }

    async RetrieveDetailedRoom(id){
        let result = await fetch(this.environmentService.getEnvironmentHost() + "/api/Room/room?id="+id);
        let responseOK = result && result.ok;
        if(responseOK){
            let data = await result.json();
            return data;
        } else {
            return result;
        }
    }

    async RetrieveRoomSearch(string){
        let result = await fetch(this.environmentService.getEnvironmentHost() + "/api/Room/search?param="+string);
        let responseOK = result && result.ok;
        if(responseOK){
            let data = await result.json();
            return data;
        } else {
            return result;
        }
    }

    async RegisterRoom(data = {},access_token){
        data.price = parseInt(data.price);
        let result = await fetch(this.environmentService.getEnvironmentHost() + "/api/Room/create",{
            method: "POST",
            headers: {
                'Content-Type': 'Application/JSON',
                'Authorization': 'Bearer ' + access_token
            },
            body: JSON.stringify(data)
        });

        let responseOK = result && result.ok;

        if(responseOK){
            let data = await result.json();
            return data;
        }
        else{
            return result;
        }
    }
}
