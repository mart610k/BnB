import React, { Component } from 'react';
import UserService from '../service/userService.js';
import { useNavigate } from "react-router-dom";
import "../css/registerSite.css";


// Wrap and export
export default function(props) {
    const navigation = useNavigate();
  
    return <RegisterSite {...props} history={navigation} />;
  }


class RegisterSite extends Component {
   

    constructor(props){
        
        super(props);
        this.userService = new UserService();

        this.checkAndRegisterUser = this.checkAndRegisterUser.bind(this);
        this.checkPasswords = this.checkPasswords.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleCheckboxChange = this.handleCheckboxChange.bind(this);
        this.onSubmit = this.onSubmit.bind(this);

        this.state = {
            emailInput : "",
            nameInput : "",
            passwordInput : "",
            passwordRepeatedInput: "",
            passwordVerified : false,
            showpass: false
        }
    }

    onSubmit(event){
        event.preventDefault();
        
        this.checkAndRegisterUser();
    }

    async checkAndRegisterUser(){
        this.checkPasswords();

        if(this.state.passwordInput.length !== 0 && this.state.passwordVerified){


           let result = await this.userService.registerUser({
               Email: this.state.emailInput,
               Name: this.state.nameInput,
               Password: this.state.passwordInput
           });

           if(result.statusCode === 200){
               alert("Your account have been created, you may now log in.");
               this.props.history("/login");
           }
           else if (result.statusCode === 409){
               alert("this email is already in use.");
           }
           else{
               alert("wrong input or some other issue have happened");
           }
        }
        else{
            alert("Your Passwords does not match each other or the field is empty");
        }
    }


    checkPasswords(){
        let result = false;

        if(this.state.passwordInput.length === 0 && this.state.passwordRepeatedInput.length === 0){
        
            result = true;
        }
        else{
            if(this.state.passwordRepeatedInput === this.state.passwordInput){
                result = true;
            }
            else{
                result = false;
            }
        }

        this.setState({
            passwordVerified: result
        }) 
    }

    handleChange(event){
        this.setState({
            [event.target.name] : event.target.value
        }, () => {
            if(event.target.name === "passwordRepeatedInput"){
                this.checkPasswords(event.target.value);
            }
        });
    }

    
    handleCheckboxChange(event){

        this.setState({
            showpass : !this.state.showpass
        });

    }

    componentDidMount(){
//        this.getTest();
        this.checkPasswords();
    }


    render()
    {
        return (
            <div id="registerUser">
                <form onSubmit={this.onSubmit}>
                    <h1>Register</h1>

                    <label htmlFor="emailInput">Email:</label><br/>
                    <input className="InputField" pattern="^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$" type="text" name="emailInput" value={this.state.emailInput} onChange={this.handleChange} placeholder="joe@email.com" required/> <br/>
                    
                    <label htmlFor="nameInput">Name:</label><br/>
                    <input type="text" className="InputField" name="nameInput" value={this.state.nameInput} onChange={this.handleChange} placeholder="joe" required/> <br/>

                    <label htmlFor="passwordInput" >Password:</label><br/>
                    <input type={this.state.showpass ? "text" : "password"} className="InputField" name="passwordInput" value={this.state.passwordInput} onChange={this.handleChange} placeholder="Password" required />

                    <input className="showpass" name="showpass" type="checkbox" checked={this.state.showpass} onChange={this.handleCheckboxChange}/> Show password

                    <br/>
                    
                    <label htmlFor="passwordRepeatedInput">Password Repeated:</label><br/>
                    <input type="password" className="InputField"  style={this.state.passwordVerified ? {} : {"backgroundColor":"#c43131"}} name="passwordRepeatedInput" value={this.state.passwordRepeatedInput} onChange={this.handleChange} placeholder="Password" required/> <br/>

                    <button className="SubmitButton" type="submit" >Register</button>
                </form>
            </div>
        )
    }
}
