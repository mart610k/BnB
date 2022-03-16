
import React, { Component } from 'react';



export default class LogoutSite extends Component {
constructor(props){
    super(props);

    this.clearCookie = this.clearCookie.bind(this)

    


}



async clearCookie(){

    document.cookie = "access_token=;expires=Thu, 01 Jan 1970 00:00:00 UTC;";
    document.cookie = "refresh_token=;expires=Thu, 01 Jan 1970 00:00:00 UTC;";
    document.cookie = "token_expires=;expires=Thu, 01 Jan 1970 00:00:00 UTC;";
    window.location.href="/";
    // document.cookie = "Max-Age=";
    // document.cookie="access_token=";
    // document.cookie = "refresh_token=";
    // document.cookie="token_expires=";
}

render()
{
    this.clearCookie();
    return (
        <div>
        </div>
    );
}
}