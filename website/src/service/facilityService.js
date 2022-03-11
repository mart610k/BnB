import EnvironmentService from "./environmentService";

export default class FacilityService {
    constructor(){
        this.environmentService = new EnvironmentService();
    }

    async RetrieveAllFacilities() {
        let result = await fetch(this.environmentService.getEnvironmentHost() + "/api/facility");

        let responseOK = result && result.ok;

        if(responseOK){
            let data = await result.json();
            return data;
        } else {
            return result;
        }
    }
}