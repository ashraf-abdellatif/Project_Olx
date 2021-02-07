using Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS
{
    public class ReadUsers
    {
        public class Query : IRequest<ReadUsersDTO>
        { }
        public class handler : IRequestHandler<Query, ReadUsersDTO>
        {
            DataContext _dataContext;
            public handler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<ReadUsersDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _dataContext.Users.AsNoTracking().ToListAsync();
                return new ReadUsersDTO(users);
            }
        }
    }
}
