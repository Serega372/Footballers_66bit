import React, { useState, useEffect } from 'react';
import { getFootballers, deleteFootballer } from '../../api/api.js';
import styles from './FootballersCatalogPage.module.css';
import EditFootballerModal from "../../components/EditFootballerModal/EditFootballerModal.jsx";

const FootballersCatalogPage = () => {
    const [footballers, setFootballers] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedFootballer, setSelectedFootballer] = useState(null);

    useEffect(() => {
        const fetchFootballers = async () => {
            const data = await getFootballers();
            setFootballers(data);
        };
        fetchFootballers();
    }, []);

    const handleDelete = async (id) => {
        await deleteFootballer(id);
        setFootballers(footballers.filter((f) => f.id !== id));
    };

    const handleEdit = (footballer) => {
        setSelectedFootballer(footballer);
        setIsModalOpen(true);
    };

    const handleSave = (updatedFootballer) => {
        setFootballers(footballers.map((f) => (f.id === updatedFootballer.id ? updatedFootballer : f)));
    };

    return (
        <div className={styles['footballers-catalog-page']}>
            <h1>Footballers catalog</h1>
            {footballers.length === 0 ? (
                <p className={styles['empty-title']}>Catalog is empty. You can add a footballer on the "Add footballer" page.</p>
            ) : (
                <div className={styles['content-container']}>
                    <table>
                        <thead>
                        <tr>
                            <th>Name</th>
                            <th>Surname</th>
                            <th>Gender</th>
                            <th>Birthday</th>
                            <th>Team</th>
                            <th>Country</th>
                            <th>Actions</th>
                        </tr>
                        </thead>
                        <tbody>
                        {footballers.map((footballer) => (
                            <tr key={footballer.id}>
                                <td>{footballer.name}</td>
                                <td>{footballer.surname}</td>
                                <td>{footballer.gender}</td>
                                <td>{new Date(footballer.birthday).toLocaleDateString()}</td>
                                <td>{footballer.teamTitle}</td>
                                <td>{footballer.country}</td>
                                <td>
                                    <div className={styles['button-container']}>
                                        <button className={styles['edit-button']} onClick={() => handleEdit(footballer)}>Edit</button>
                                        <button className={styles['delete-button']} onClick={() => handleDelete(footballer.id)}>Delete</button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
                </div>
            )}
            {selectedFootballer && (
                <EditFootballerModal
                    isOpen={isModalOpen}
                    onClose={() => setIsModalOpen(false)}
                    footballer={selectedFootballer}
                    onSave={handleSave}
                />
            )}
        </div>
    );
};

export default FootballersCatalogPage;