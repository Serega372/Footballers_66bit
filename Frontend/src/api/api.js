import axios from 'axios';

const API_BASE_URL = 'https://localhost:7256';

export const getFootballers = async () => {
    const response = await axios.get(`${API_BASE_URL}/footballers`);
    return response.data;
};

export const addFootballer = async (footballer) => {
    const response = await axios.post(`${API_BASE_URL}/footballers`, footballer);
    return response.data;
};

export const updateFootballer = async (id, footballer) => {
    const response = await axios.put(`${API_BASE_URL}/footballers/${id}`, footballer);
    return response.data;
};

export const deleteFootballer = async (id) => {
    const response = await axios.delete(`${API_BASE_URL}/footballers/${id}`);
    return response.data;
};

export const getTeams = async () => {
    const response = await axios.get(`${API_BASE_URL}/teams`);
    return response.data;
};

export const addTeam = async (team) => {
    const response = await axios.post(`${API_BASE_URL}/teams`, team);
    return response.data;
}

export const deleteTeam = async (id) => {
    const response = await axios.delete(`${API_BASE_URL}/teams/${id}`);
    return response.data;
};