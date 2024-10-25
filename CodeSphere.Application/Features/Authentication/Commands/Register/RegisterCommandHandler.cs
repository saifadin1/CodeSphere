﻿using AutoMapper;
using CodeSphere.Domain.Abstractions;
using CodeSphere.Domain.Models.Identity;
using CodeSphere.Domain.Premitives;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Application.Features.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterCommandHandler(IUnitOfWork unitOfWork,
                                      IMapper mapper,
                                      UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<Response> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var mappedUser = _mapper.Map<RegisterCommand, ApplicationUser>(request);

            var applicationUser = await _userManager.FindByEmailAsync(request.Email);

            if (applicationUser is not null)
                return await Response.FailureAsync("Email is Existed!", System.Net.HttpStatusCode.BadRequest);

            var applicationUserByUserName = await _userManager.FindByNameAsync(request.UserName);

            if (applicationUserByUserName is not null)
                return await Response.FailureAsync("UserName is Existed!", System.Net.HttpStatusCode.BadRequest);

            var registerResult = await _userManager.CreateAsync(mappedUser, request.Password);
            if (!registerResult.Succeeded)
                return await Response.FailureAsync(
    string.Join(", ", registerResult.Errors.Select(x => x.Description)),
    System.Net.HttpStatusCode.BadRequest
);

            return await Response.SuccessAsync(request, "Register Successfully");


        }
    }
}
