import React, { useState, useEffect } from 'react';
import { updateFootballer, getTeams } from '../../api/api.js';
import styles from './EditFootballerModal.module.css';

const EditFootballerModal = ({ isOpen, onClose, footballer, onSave }) => {
    const [name, setName] = useState(footballer.name);
    const [surname, setSurname] = useState(footballer.surname);
    const [gender, setGender] = useState(footballer.gender);
    const [birthday, setBirthday] = useState(
        footballer.birthday ? footballer.birthday.split('T')[0] : ''
    );
    const [teamId, setTeamId] = useState(footballer.teamId);
    const [country, setCountry] = useState(footballer.country);
    const [teams, setTeams] = useState([]);

    useEffect(() => {
        const fetchTeams = async () => {
            const data = await getTeams();
            setTeams(data);
        };
        fetchTeams();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const formattedBirthday = new Date(birthday).toISOString();
        const updatedFootballer = {
            name,
            surname,
            gender,
            birthday: formattedBirthday,
            teamId,
            country,
        };
        await updateFootballer(footballer.id, updatedFootballer);
        onSave(updatedFootballer);
        onClose();
    };

    if (!isOpen) return null;

    return (
        <div className={styles['modal-overlay']}>
            <div className={styles['modal']}>
                <h2>Edit Footballer</h2>
                <form onSubmit={handleSubmit}>
                    <label>Name:</label>
                    <input type="text" value={name} onChange={(e) => setName(e.target.value)} required />
                    <label>Surname:</label>
                    <input type="text" value={surname} onChange={(e) => setSurname(e.target.value)} required />
                    <label>Gender:</label>
                    <select value={gender} onChange={(e) => setGender(e.target.value)} required>
                        <option value="">Choose gender</option>
                        <option value="Male">Male</option>
                        <option value="Female">Female</option>
                    </select>
                    <label>Birthday:</label>
                    <input type="date" value={birthday} onChange={(e) => setBirthday(e.target.value)} required
                           min="1960-01-01"
                           max="2021-12-31"/>
                    <label>Team:</label>
                    <select value={teamId} onChange={(e) => setTeamId(e.target.value)} required>
                        <option value="">Choose team</option>
                        {teams.map((team) => (
                            <option key={team.id} value={team.id}>
                                {team.teamTitle}
                            </option>
                        ))}
                    </select>
                    <label>Country:</label>
                    <select value={country} onChange={(e) => setCountry(e.target.value)} required>
                        <option value="">Choose Country</option>
                        <option value="Russia">Russia</option>
                        <option value="USA">USA</option>
                        <option value="Italy">Italy</option>
                    </select>
                    <div className={styles['modal-buttons']}>
                        <button className={styles['save-button']}>Save</button>
                        <button className={styles['cancel-button']} onClick={onClose}>Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default EditFootballerModal;