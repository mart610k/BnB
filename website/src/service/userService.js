import EnvironmentService from "./environmentService";

export default class UserService{
    
    constructor(){
        this.environmentService = new EnvironmentService();
    }

    async registerUser(data = {}){
        let result = await fetch(this.environmentService.getEnvironmentHost() + "/api/user/register",{
            method: "POST",
            headers: {
                'Content-Type': 'Application/JSON'
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