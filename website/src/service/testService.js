export default class TestService{
    
    async retrieveTestAPI(){
        let result = await fetch("http://localhost:65273/test");

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