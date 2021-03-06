﻿using Application.Interfaces.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Dapper;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Interfaces.Repositories.EFCore;

namespace Application.Interfaces.Services.Domain
{
    public interface IUserService<TRepository> : IServiceBase<User>
                             where TRepository : IUserRepository
    {
    }
}
