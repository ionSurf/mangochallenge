using System;
using LoggerService;
using AutoMapper;
using Repositories;
using Entities.Models;
using UserServiceLib;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Filters.ActionFilters;

namespace Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private IUserService _userService;

        public AuthController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, IUserService userService)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
        }

        [EnableCors("MangoPolicy")]
        [HttpPost, Route("login")]
        public IActionResult JWTLogin([FromBody] AuthenticateRequest reqUser)
        {
            try {
                var sqlUser     = _repository.User.GetUserByUserName( reqUser.UserName );
                var token       = _userService.Authenticate( reqUser, sqlUser );
                if ( token != null )
                    return Ok(token );
                return Unauthorized();

            } catch( Exception e ) {
                _logger.LogError($"Something went wrong inside AuthLogin action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [EnableCors("MangoPolicy")]
        [HttpPost, Route("login-optimized")]
        public IActionResult JWTLoginOptimized([FromBody] AuthenticateRequest reqUser)
        {
            var sqlUser     = _repository.User.GetUserByUserName( reqUser.UserName );
            var token       = _userService.Authenticate( reqUser, sqlUser );
            if ( token != null )
                return Ok(token );
            return Unauthorized();
        }
    }
}