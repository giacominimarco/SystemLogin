import axios from 'axios';
import { saveToken, clearToken } from './auth';

const api = axios.create({
  baseURL: 'http://localhost:5129/api/user',
  headers: { 'Content-Type': 'application/json' },
});

// adicionar o token no header Authorization
api.interceptors.request.use(config => {
  const token = localStorage.getItem('systemlogin_token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export async function login(username, password) {
  try {
    const { data } = await api.post('/login', { username, password });
    if (data.token) {
      saveToken(data.token);
      return data.token;
    }
    throw new Error('Token não retornado pelo servidor');
  } catch (err) {
    if (err.response?.status === 401) {
      throw new Error('Credenciais inválidas');
    }
    throw new Error('Erro de autenticação. Tente novamente.');
  }
}

export async function logout() {
  clearToken();
}

export async function fetchUsers(page = 1, pageSize = 10) {
  const response = await api.get('/list', {
    params: { page, pageSize },
  });
  return response.data;
}