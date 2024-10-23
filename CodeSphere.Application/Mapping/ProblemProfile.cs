﻿using AutoMapper;
using CodeSphere.Application.Features.Problem.Commands.Create;
using CodeSphere.Domain.Models;

namespace CodeSphere.Application.Mapping
{
    public class ProblemProfile : Profile
    {
        public ProblemProfile()
        {
            CreateMap<CreateProblemCommand, Problem>();
        }
    }
}
