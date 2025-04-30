// src/services/auth.js
const TOKEN_KEY = 'systemlogin_token';

export function saveToken(token) {
  localStorage.setItem(TOKEN_KEY, token);
}

export function getToken() {
  return localStorage.getItem(TOKEN_KEY);
}

export function clearToken() {
  localStorage.removeItem(TOKEN_KEY);
}

export function isAuthenticated() {
  const token = getToken();
  // Verifica se o token existe e é válido
  return token && !isTokenExpired(token);
}

function isTokenExpired(token) {
  try {
    const decoded = JSON.parse(atob(token.split('.')[1]));
    return decoded.exp < Date.now() / 1000;
  } catch {
    return true;
  }
}