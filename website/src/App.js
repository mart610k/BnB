import './App.css';
import TestSite from "./sites/testSite.js";
import RegisterSite from "./sites/registerSite.js";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  useNavigate
} from "react-router-dom";
import ListRoomSite from './sites/listRoomSite';

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
            <Route path="/register" element={<RegisterSite />} />
            <Route path="/room" element={<ListRoomSite />} />
          </Routes>
        </Router>
      </section>
    </div>
  );
}

export default App;