import {
  BrowserRouter as Router,
  Routes,
  Route,
  useParams,
  useNavigate
} from "react-router-dom";
import React, { useState } from "react";
import './App.css';
import ListRoomSite from './sites/listRoomSite';
import DetailedRoomSite from './sites/detailedRoomSite';
import TestSite from "./sites/testSite.js";
import RegisterSite from "./sites/registerSite.js";
import HeaderSite from "./sites/headerSite";
import SearchSite from "./sites/searchSite";

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
          </Routes>
        </Router>
      </section>
    </div>
  );
}

export default App;