import React from 'react';

interface PaginationProps {
  currentPage: number;
  totalCount: number;
  pageSize: number;
  onPageChange: (page: number) => void;
}

function Pagination({ currentPage, totalCount, pageSize, onPageChange }: PaginationProps) {
  const totalPages = Math.ceil(totalCount / pageSize);
  const pages: React.ReactNode[] = [];

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