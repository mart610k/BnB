export default class TestService{
    
    async registerUser(data = {}){
        let result = await fetch("http://localhost:65273/api/user/register",{
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