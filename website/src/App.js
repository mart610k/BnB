import './App.css';
import TestSite from "./sites/testSite.js";
import RegisterSite from "./sites/registerSite.js";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  useParams,
  useNavigate
} from "react-router-dom";
import ListRoomSite from './sites/listRoomSite';
import DetailedRoomSite from './sites/detailedRoomSite';

//import { useNavigate } from "react-router-dom";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        
      </header>
      <section className='App-section'>
      <Router>
          <Routes>
            <Route path="/test" element={<TestSite />} />
            <Route exact path="/" element={<ListRoomSite />} />
            <Route exact path="/room/:RoomID" element={<DetailedRoomSite />} />
            <Route path="/register" element={<RegisterSite />} />
            <Route path="/room" element={<ListRoomSite />} />
          </Routes>
        </Router>
      </section>
    </div>
    
  );
}

export default App;