﻿namespace ProjectManagement.Data.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByLoginAsync(string login);
}
