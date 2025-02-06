import React from 'react';
import { Link } from 'react-router-dom';
import styles from './Navbar.module.css';

const Navbar = () => {
    return (
        <nav className={styles['navbar']}>
            <Link to="/">
                <p>Add footballer</p>
            </Link>
            <Link to="/footballers">
                <p>Footballers catalog</p>
            </Link>
        </nav>
    );
};

export default Navbar;