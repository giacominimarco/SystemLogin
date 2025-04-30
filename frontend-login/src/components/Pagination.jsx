import React from 'react';

function Pagination({ currentPage, totalCount, pageSize, onPageChange }) {
  const totalPages = Math.ceil(totalCount / pageSize);
  const pages = [];

  for (let i = 1; i <= totalPages; i++) {
    pages.push(
      <button
        key={i}
        disabled={i === currentPage}
        onClick={() => onPageChange(i)}
      >
        {i}
      </button>
    );
  }

  return <div>{pages}</div>;
}

export default Pagination;
