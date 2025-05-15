import axios, { InternalAxiosRequestConfig } from 'axios';
import { saveToken, clearToken } from './auth';

export interface User {
  id: number;
  username: string;
  // outros campos do usuário, se houver
}

export interface FetchUsersResponse {
  total: number;
  users: User[];
}

const api = axios.create({
  baseURL: 'http://localhost:5129/api/user',
  headers: { 'Content-Type': 'application/json' },
});

api.interceptors.request.use((config: InternalAxiosRequestConfig) => {
  const token = localStorage.getItem('systemlogin_token');
  if (token) {
    // Usar o método set para manter a tipagem correta
    config.headers.set('Authorization', `Bearer ${token}`);
  }
  return config;
});

export async function login(username: string, password: string): Promise<string> {
  try {
    const { data } = await api.post<{ token: string }>('/login', { username, password });
    if (data.token) {
      saveToken(data.token);
      return data.token;
    }
    throw new Error('Token não retornado pelo servidor');
  } catch (err: any) {
    if (err.response?.status === 401) {
      throw new Error('Credenciais inválidas');
    }
    throw new Error('Erro de autenticação. Tente novamente.');
  }
}

export async function logout(): Promise<void> {
  clearToken();
}

export async function fetchUsers(page = 1, pageSize = 10): Promise<FetchUsersResponse> {
  const response = await api.get<FetchUsersResponse>('/list', {
    params: { page, pageSize },
  });
  return response.data;
}