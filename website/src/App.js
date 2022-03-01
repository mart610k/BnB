import './App.css';
import TestSite from "./sites/testSite.js";
import {
  BrowserRouter as Router,
  Routes,
  Route
} from "react-router-dom";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Router>
          <Routes>
            <Route path="/test" element={<TestSite />} />
          </Routes>
        </Router>
      </header>
    </div>
  );
}

export default App;
