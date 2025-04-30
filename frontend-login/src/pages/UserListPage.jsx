import React, { useEffect, useState } from 'react';
import { fetchUsers } from '../services/api';
import Pagination from '../components/Pagination';

function UserListPage() {
  const [users, setUsers] = useState([]);
  const [total, setTotal] = useState(0);
  const [page, setPage] = useState(1);
  const pageSize = 10;

  useEffect(() => {
    async function load() {
      const data = await fetchUsers(page, pageSize);
      setUsers(data.users);
      setTotal(data.total);
    }
    load();
  }, [page]);

  return (
    <div>
      <h1>Lista de Usuários</h1>
      <ul>
        {users.map((u) => (
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
