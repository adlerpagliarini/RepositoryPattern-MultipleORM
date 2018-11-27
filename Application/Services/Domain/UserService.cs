using Application.Interfaces.Services;
using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Dapper;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Interfaces.Repositories.EFCore;
using Infrastructure.Interfaces.Repositories.Standard;
using System;

namespace Application.Services.Domain
{
    public class UserService<TUserRepository> : ServiceBase<User>, 
                                                IUserService<TUserRepository>
                        where TUserRepository : IUserRepository
    {
        internal UserService(Func<Type, IUserRepository> repository) : base(repository(typeof(TUserRepository)))
        {
        }

        //where T : new()
        // public UserService() : base(new T()) { }
    }
}
