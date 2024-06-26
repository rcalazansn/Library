﻿using Library.Domain.Models;

namespace Library.Domain.Repositories
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        Task<List<Book>> GetAsync(string query);
        Task<List<Book>> GetAsync();
        Task<Book?> GetByIdAsync(int id);
        Task RemoveAsync(int id);
    }
}
