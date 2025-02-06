import { Routes, Route } from 'react-router-dom';
import './App.css';
import AddFootballerPage from './pages/AddFootballerPage/AddFootballerPage.jsx';
import FootballersCatalogPage from './pages/FootballersCatalogPage/FootballersCatalogPage.jsx';
import Navbar from './components/Navbar/Navbar.jsx';

const App = () => {
    return (
        <>
            <Navbar />
            <Routes>
                <Route path="/" element={<AddFootballerPage />} />
                <Route path="/footballers" element={<FootballersCatalogPage />} />
            </Routes>
        </>
    );
};

export default App;