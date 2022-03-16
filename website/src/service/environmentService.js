export default class EnvironmentService {

    // This method is for telling where the API is located helps with both development and production.
    getEnvironmentHost(){
        if(process.env.NODE_ENV === "development")
        {
            return "http://localhost:65273";
        }
        else 
        {
            return window.location.origin;
        }
    }

    getClientKeySecretBase64Encoded(){
        return btoa("FS?[s(?%C4X$&%)A:}_3}3LV*(Vsjr?S.");
    }
}