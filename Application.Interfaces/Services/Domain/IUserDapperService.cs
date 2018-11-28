﻿using Application.Interfaces.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;

namespace Application.Interfaces.Services.Domain
{
    public interface IUserDapperService : IUserService<IUserDapperRepository>, IServiceBase<User>
    {
    }
}
