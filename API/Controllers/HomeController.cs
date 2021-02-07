using Application.CQRS;
using Application.Models;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class HomeController : ControllerBase
    {
        IMediator _Mediator;
        public HomeController(IMediator Mediator)
        {
            _Mediator = Mediator;
        }
        public async Task<ActionResult<ReadUsersDTO>> Get()
        {
            return await _Mediator.Send(new ReadUsers.Query());
        }
    }
}
