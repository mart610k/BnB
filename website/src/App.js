import './App.css';
import TestSite from "./sites/testSite.js";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  useParams
} from "react-router-dom";
import ListRoomSite from './sites/listRoomSite';
import DetailedRoomSite from './sites/detailedRoomSite';

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
          </Routes>
        </Router>
      </section>
    </div>
    
  );
}

export default App;
