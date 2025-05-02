import React, { useEffect, useState } from 'react';
import { fetchUsers, logout } from '../services/api';
import Pagination from '../components/Pagination';
import { useNavigate } from 'react-router-dom';
import '../styles/UserList.css';

function UserListPage() {
  const [users, setUsers] = useState([]);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const pageSize = 10;
  const navigate = useNavigate();

  useEffect(() => {
    async function load() {
      setLoading(true);
      setError('');
      try {
        const data = await fetchUsers(page, pageSize);
        setUsers(Array.isArray(data.users) ? data.users : []);
        setTotal(Number(data.total) || 0);
      } catch (e) {
        setError('Erro ao carregar usuários. Redirecionando…');
        await handleLogout();
      } finally {
        setLoading(false);
      }
    }
    load();
  }, [page]);

  const handleLogout = async () => {
    await logout();
    navigate('/login', { replace: true });
  };

  return (
    <div className="login-container">
      <div className="userlist-box">
        <header className="userlist-header">
          <h1>Lista de Usuários</h1>
          <button onClick={handleLogout} className="primary-button">
            Logout
          </button>
        </header>

        <div className="userlist-content">
          {loading ? (
            <p className="info-message">Carregando usuários…</p>
          ) : error ? (
            <p className="error-message">{error}</p>
          ) : users.length === 0 ? (
            <p className="info-message">Nenhum usuário encontrado.</p>
          ) : (
            <ul className="userlist">
              {users.map(u => (
                <li key={u.id} className="userlist-item">
                  {u.username || '[sem nome]'}
                </li>
              ))}
            </ul>
          )}
        </div>

        {!loading && users.length > 0 && (
          <div className="pagination-wrapper">
            <Pagination
              currentPage={page}
              totalCount={total}
              pageSize={pageSize}
              onPageChange={setPage}
            />
          </div>
        )}
      </div>
    </div>
  );
}

export default UserListPage;