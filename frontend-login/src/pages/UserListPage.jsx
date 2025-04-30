import React, { useEffect, useState } from 'react';
import { fetchUsers, logout } from '../services/api';
import Pagination from '../components/Pagination';
import { useNavigate } from 'react-router-dom';

function UserListPage() {
  const [users, setUsers] = useState([]);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(1);
  const pageSize = 10;
  const navigate = useNavigate();

  useEffect(() => {
    async function load() {
      try {
        const data = await fetchUsers(page, pageSize);
        setUsers(data.users);
        setTotal(data.total);
      } catch {
        // Se der 401 ou outro erro, force logout
        await handleLogout();
      }
    }
    load();
  }, [page]);

  const handleLogout = async () => {
    await logout();
    navigate('/login', {
      replace: true
    });
  };

  return (
    <div>
      <header style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <h1>Lista de Usuários</h1>
        <button onClick={handleLogout}>
          Logout
        </button>
      </header>

      <ul>
        {users.map(u => (
          <li key={u.id}>{u.username}</li>
        ))}
      </ul>

      <Pagination
        currentPage={page}
        totalCount={total}
        pageSize={pageSize}
        onPageChange={setPage}
      />
    </div>
  );
}

export default UserListPage;
