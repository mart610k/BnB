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
}