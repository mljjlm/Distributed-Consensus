﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Common.DTO.Event;
using Common.DTO.Server;
using Common.Exceptions;
using Server.Exceptions;
using Server.Interfaces;

namespace Server.Controllers
{
    /// <summary>
    /// UsersController handles HTTP-request regarding users on Server
    /// </summary>
    public class UsersController : ApiController
    {
        private readonly IServerLogic _logic;

        /// <summary>
        /// Constructor used for dependency-injection during testing
        /// </summary>
        /// <param name="logic"></param>
        public UsersController(IServerLogic logic)
        {
            _logic = logic;
        }

 
        /// <summary>
        /// Returns the users roles on all workflows.
        /// </summary>
        /// <param name="loginDto">Contains the login-information needed for login-attempt</param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public async Task<RolesOnWorkflowsDto> Login([FromBody] LoginDto loginDto)
        {
            // Check input
            if (!ModelState.IsValid)
            {
                var toThrow = new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "The provided input could not be mapped onto an instance of LoginDto"));
                /*await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                {
                    HttpRequestType = "POST",
                    Message = "Threw: " + toThrow.GetType(),
                    MethodCalledOnSender = "PostWorkflow"
                });*/
                throw toThrow;
            }

            try
            {
                var toReturn = await _logic.Login(loginDto);
                /*await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                {
                    HttpRequestType = "POST",
                    Message = "Called: Login with username: " + loginDto.Username,
                    MethodCalledOnSender = "Login",
                });*/

                return toReturn;
            }
            catch (ArgumentNullException)
            {
                //await _historyLogic.SaveHistory(new ActionModel
                //{
                //    EventId = "",
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + e.GetType(),
                //    MethodCalledOnSender = "Login"
                //});

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Seems input was not satisfactory"));
            }
            catch (UnauthorizedException)
            {
                //await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + e.GetType(),
                //    MethodCalledOnSender = "Login",
                //});

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                    "Username or password does not correspond to a user."));
            }
            catch (Exception e)
            { 
                //await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + e.GetType(),
                //    MethodCalledOnSender = "Login",
                //});

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e));
            }
        }

        /// <summary>
        /// CreateUser attempts to create a user given the provided UserDto
        /// </summary>
        /// <param name="dto">Contains login-information and given roles for the user</param>
        /// <returns></returns>
        [Route("users")] 
        [HttpPost]
        public async Task CreateUser([FromBody] UserDto dto)
        {
            if (!ModelState.IsValid)
            {
                var toThrow = new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
                //await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + toThrow.GetType(),
                //    MethodCalledOnSender = "CreateUser",
                //});

                throw toThrow;
            }

            try
            {
                await _logic.AddUser(dto);
                //await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Called: CreateUser with username: " + dto.Name,
                //    MethodCalledOnSender = "CreateUser",
                //});
            }
            catch (ArgumentNullException)
            {
                //await _historyLogic.SaveHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + e.GetType() + " with username: " + dto.Name,
                //    MethodCalledOnSender = "CreateUser"
                //});

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Seems input was not satisfactory"));
            }
            catch (NotFoundException)
            {
                //await _historyLogic.SaveHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + e.GetType() + " with username: " + dto.Name,
                //    MethodCalledOnSender = "CreateUser"
                //});
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "A role attached to the provided user could not be found"));
            }
            catch (UserExistsException)
            {
                //await _historyLogic.SaveHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + e.GetType() + " with username: " + dto.Name,
                //    MethodCalledOnSender = "CreateUser"
                //});
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict,
                    "The provided user already exists at Server."));
            }
            catch (InvalidOperationException)
            {
                //await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + e.GetType() + " with username: " + dto.Name,
                //    MethodCalledOnSender = "CreateUser",
                //});

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "One of the roles does not exist."));

            }
            catch (ArgumentException e)
            {
                if (e.ParamName != null && e.ParamName.Equals("user"))
                {
                    //await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                    //{
                    //    HttpRequestType = "POST",
                    //    Message = "Threw: " + e.GetType() + " with username: " + dto.Name,
                    //    MethodCalledOnSender = "CreateUser",
                    //});

                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict,
                        "A user with that username already exists."));
                }
                else {
                    //await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                    //{
                    //    HttpRequestType = "POST",
                    //    Message = "Threw: " + e.GetType() + " with username: " + dto.Name,
                    //    MethodCalledOnSender = "CreateUser",
                    //});

                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e));
                }
            }
            catch (Exception e)
            {
                //await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + e.GetType(),
                //    MethodCalledOnSender = "CreateUser",
                //});

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e));
            }
        }

        /// <summary>
        /// Add roles to an already existing user.
        /// </summary>
        /// <param name="username">The username of the user which should have the roles added.</param>
        /// <param name="roles">The roles to add.</param>
        /// <returns></returns>
        [Route("users/{username}/roles")]
        [HttpPost]
        public async Task AddRolesToUser(string username, [FromBody] IEnumerable<WorkflowRole> roles)
        {
            if (!ModelState.IsValid)
            {
                var toThrow = new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
                //await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + toThrow.GetType(),
                //    MethodCalledOnSender = "AddRolesToUser",
                //});

                throw toThrow;
            }

            try
            {
                await _logic.AddRolesToUser(username, roles);
                //await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Called: AddRolesToUser with username: " + username,
                //    MethodCalledOnSender = "AddRolesToUser",
                //});
            }
            catch (ArgumentNullException)
            {
                //await _historyLogic.SaveHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + e.GetType() + " with username: " + username,
                //    MethodCalledOnSender = "AddRolesToUser"
                //});

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Seems input was not satisfactory"));
            }
            catch (NotFoundException)
            {
                //await _historyLogic.SaveHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + e.GetType() + " with username: " + username,
                //    MethodCalledOnSender = "AddRolesToUser"
                //});
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "The user, or the role could not be found."));
            }
            catch (Exception e)
            {
                //await _historyLogic.SaveNoneWorkflowSpecificHistory(new ActionModel
                //{
                //    HttpRequestType = "POST",
                //    Message = "Threw: " + e.GetType(),
                //    MethodCalledOnSender = "AddRolesToUser",
                //});

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, e));
            }
        }

        protected override void Dispose(bool disposing)
        {
            _logic.Dispose();
            base.Dispose(disposing);
        }
    }
}