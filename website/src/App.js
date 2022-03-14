import {
  BrowserRouter as Router,
  Routes,
  Route
} from "react-router-dom";
import React, { useState } from "react";
import './App.css';
import ListRoomSite from './sites/listRoomSite';
import DetailedRoomSite from './sites/detailedRoomSite';
import TestSite from "./sites/testSite.js";
import RegisterSite from "./sites/registerSite.js";
import HeaderSite from "./sites/headerSite";
import SearchSite from "./sites/searchSite";
import LoginSite from './sites/loginSite';
import CreateRoomSite from "./sites/createRoomSite";

//import { useNavigate } from "react-router-dom";

function App() {
  return (
    <div className="App">
      <HeaderSite />
      <section className='App-section'>

      <Router>
          <Routes>
            <Route path="/test" element={<TestSite />} />
            <Route exact path="/" element={<ListRoomSite />} />
            <Route exact path="/room/:RoomID" element={<DetailedRoomSite />} />
            <Route path="/register" element={<RegisterSite />} />
            <Route path="/search" element={<SearchSite />} />
            <Route path="/room" element={<ListRoomSite />} />
            <Route path="/login" element={<LoginSite />} />
            <Route path="/room/create" element={<CreateRoomSite />} />
          </Routes>
        </Router>
      </section>
      <footer>
        
      </footer>
    </div>
  );
}

export default App;