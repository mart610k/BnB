import './App.css';
import TestSite from "./sites/testSite.js";
import {
  BrowserRouter as Router,
  Routes,
  Route
} from "react-router-dom";
import ListRoomSite from './sites/listRoomSite';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        
      </header>
      <section className='App-section'>
      <Router>
          <Routes>
            <Route path="/test" element={<TestSite />} />
            <Route path="/room" element={<ListRoomSite />} />
          </Routes>
        </Router>
      </section>
    </div>
  );
}

export default App;
