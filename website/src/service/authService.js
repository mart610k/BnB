import EnvironmentService from "./environmentService";

export default class AuthService{
    constructor(){
        this.environmentService = new EnvironmentService();
    }

    async RetrieveAccesstoken(data = {}) {
        
        var basicheader = "Basic " + this.environmentService.getClientKeySecretBase64Encoded();  

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

    async updateAccesstokenFromRefreshToken(refresh_token){
        var basicheader = "Basic " + this.environmentService.getClientKeySecretBase64Encoded();  

        let result = await fetch(this.environmentService.getEnvironmentHost() + "/api/auth/token",{
            method : "POST",
            headers: {
                "Content-Type": "multipart/form-data",
                "authorization": basicheader 
            },
            body: "grant_type=" + "refresh_token" + "&refresh_token=" + refresh_token  
        });

        let responseOK = result && result.ok;

        if(responseOK){
            let data = await result.json();
            return data;
        } else {
            return result;
        }
    }


    async checkAndUpdateAccessToken(access_token, refresh_token, validto){
        if(validto !== ""){
            
            if (!(Date.now() < Date.parse(validto) - (1000 * 60 *2)) ){
                if(access_token !== "" & refresh_token !== ""){
                    let result = await this.updateAccesstokenFromRefreshToken(refresh_token);
                    document.cookie = "access_token="+ result.access_token;
                    document.cookie = "refresh_token="+ result.refresh_token;
                    let date = new Date();
                    date.setSeconds(date.getSeconds() + result.expires_in);
                    document.cookie = "token_expires=" + date;
                    console.log("Token was refreshed");
                    return true;
                }
                else{
                }
            }
            else{
                return true;
            }
        }
        else{
        }
        return false;
    }
}