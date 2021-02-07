using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class ReadUsersDTO
    {
        public List<AppUser> _users { get; set; }
        public ReadUsersDTO(List<AppUser> users)
        {
            _users = users;
        }
    }
}
