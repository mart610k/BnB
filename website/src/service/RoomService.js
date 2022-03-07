export default class RoomService{

    async RetrieveSimpleRooms() {
        let result = await fetch("http://localhost:65273/api/Room/list");

        let responseOK = result && result.ok;

        if(responseOK){
            let data = await result.json();
            return data;
        } else {
            return result;
        }
    }

    async RetrieveDetailedRoom(id){
        let result = await fetch("http://localhost:65273/api/Room/room?id="+id);
        console.log(result);
        let responseOK = result && result.ok;

        if(responseOK){
            let data = await result.json();
            return data;
        } else {
            return result;
        }
    }
}