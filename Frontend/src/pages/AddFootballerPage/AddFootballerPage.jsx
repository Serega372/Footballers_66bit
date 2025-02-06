import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import {addFootballer, deleteTeam, getTeams} from '../../api/api.js';
import styles from './AddFootballerPage.module.css';
import AddTeamModal from "../../components/AddTeamModal/AddTeamModal.jsx";

const AddFootballerPage = () => {
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [gender, setGender] = useState('');
    const [birthday, setBirthday] = useState('');
    const [teamId, setTeamId] = useState('');
    const [country, setCountry] = useState('');
    const [teams, setTeams] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const navigate = useNavigate();

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
        const footballer = { name, surname, gender, birthday: formattedBirthday, teamId, country };
        await addFootballer(footballer);
        navigate('/footballers');
    };

    const handleAddTeam = async (newTeam) => {
        setTeams((prevTeams) => [...prevTeams, newTeam]);
        setTeamId(newTeam.id);
    }

    const handleDeleteTeam = async (id) => {
        await deleteTeam(id);
        setTeams(teams.filter((team) => team.id !== id));
        if(teamId === id) setTeamId('');
    }

    const handleTeamChange = (e) => {
        const selectedValue = e.target.value;
        if(selectedValue === 'add') {
            setIsModalOpen(true);
            setTeamId('');
        } else {
            setTeamId(selectedValue);
        }
    }

    return (
        <div className={styles['add-footballer-page']}>
            <h1>Add footballer</h1>
            <div className={styles['content-container']}>
                <form onSubmit={handleSubmit}>
                    <label>Name:</label>
                    <input placeholder="Enter Name" type="text" value={name} onChange={(e) => setName(e.target.value)} required />
                    <label>Surname:</label>
                    <input placeholder="Enter Surname" type="text" value={surname} onChange={(e) => setSurname(e.target.value)} required />
                    <label>Gender:</label>
                    <select value={gender} onChange={(e) => setGender(e.target.value)} required>
                        <option>Choose gender</option>
                        <option value="Male">Male</option>
                        <option value="Female">Female</option>
                    </select>
                    <label>Birthday:</label>
                    <input type="date" value={birthday} onChange={(e) => setBirthday(e.target.value)} required
                           min="1960-01-01"
                           max="2021-12-31"/>
                    <label>Team:</label>
                    <select value={teamId} onChange={handleTeamChange} required>
                        <option value="">Choose team</option>
                        {teams.map((team) => (
                            <option key={team.id} value={team.id}>
                                {team.teamTitle}
                            </option>
                        ))}
                        <option value="add">Add team</option>
                    </select>
                    {teamId && teamId !== 'add' && (
                        <button
                            type="button"
                            className={styles['delete-team-button']}
                            onClick={() => handleDeleteTeam(teamId)}
                        >Delete team</button>
                    )}
                    <label>Country:</label>
                    <select value={country} onChange={(e) => setCountry(e.target.value)} required>
                        <option value="">Choose Country</option>
                        <option value="Russia">Russia</option>
                        <option value="USA">USA</option>
                        <option value="Italy">Italy</option>
                    </select>
                    <button className={styles['add-button']}>Add</button>
                </form>
            </div>
            <AddTeamModal
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                onTeamAdded={handleAddTeam}
            />
        </div>
    );
};

export default AddFootballerPage;