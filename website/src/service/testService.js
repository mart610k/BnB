import EnvironmentService from "./environmentService";


export default class TestService{

    constructor(){
        this.environmentService = new EnvironmentService();
    }

    async retrieveTestAPI(){
        let result = await fetch(this.environmentService.getEnvironmentHost() + "/test");

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