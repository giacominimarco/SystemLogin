import axios from 'axios';

// Base configuration for API calls
const api = axios.create({
  baseURL: 'http://localhost:5000/api/user',
  headers: {
    'Content-Type': 'application/json',
  },
});

export async function login(username, password) {
  const response = await api.post('/login', { username, password });
  return response.data;
}

export async function fetchUsers(page = 1, pageSize = 10) {
  const response = await api.get('/list', {
    params: { page, pageSize },
  });
  return response.data;
}

export default api;
