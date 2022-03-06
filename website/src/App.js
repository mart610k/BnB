import './App.css';
import TestSite from "./sites/testSite.js";
import RegisterSite from "./sites/registerSite.js";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  useNavigate
} from "react-router-dom";

//import { useNavigate } from "react-router-dom";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Router>
          <Routes>
            <Route path="/test" element={<TestSite />} />
            <Route path="/register" element={<RegisterSite />} />
          </Routes>
        </Router>
      </header>
    </div>
  );
}

export default App;