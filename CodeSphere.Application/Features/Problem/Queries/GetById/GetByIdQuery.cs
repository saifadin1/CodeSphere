﻿using CodeSphere.Domain.Premitives;
using MediatR;

namespace CodeSphere.Application.Features.Problem.Queries.GetById
{
    public class GetByIdQuery : IRequest<Response>
    {
        public int ProblemId { get; set; }

        public GetByIdQuery(int problemId)
        {
            ProblemId = problemId;
        }
    }


}
