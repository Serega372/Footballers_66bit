import React, { useState } from 'react';
import { addTeam } from '../../api/api.js';
import styles from './AddTeamModal.module.css';

const AddTeamModal = ({ isOpen, onClose, onTeamAdded }) => {
    const [teamTitle, setTeamTitle] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!teamTitle) return;

        const newTeam = await addTeam({ teamTitle });
        onTeamAdded(newTeam);
        setTeamTitle('');
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className={styles['modal-overlay']}>
            <div className={styles['modal']}>
                <h2>Add team</h2>
                <form onSubmit={handleSubmit}>
                    <label className={styles['sss']}>Team title:</label>
                    <input
                        type="text"
                        value={teamTitle}
                        onChange={(e) => setTeamTitle(e.target.value)}
                        required
                    />
                    <div className={styles['modal-buttons']}>
                        <button className={styles['add-button']}>Add</button>
                        <button onClick={onClose} className={styles['cancel-button']}>Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default AddTeamModal;