﻿using AutoMapper;
using CodeSphere.Domain.Abstractions;
using CodeSphere.Domain.DTOs;
using CodeSphere.Domain.Premitives;
using MediatR;

namespace CodeSphere.Application.Features.Contest.Command.Update
{
    public class UpdateContestCommandHandler : IRequestHandler<UpdateContestCommand, Response>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateContestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Response> Handle(UpdateContestCommand request, CancellationToken cancellationToken)
        {
            var contest = await unitOfWork.Repository<Domain.Models.Entities.Contest>().GetByIdAsync(request.Id);
            if (contest == null)
            {
                return await Response.FailureAsync("Contest not found", System.Net.HttpStatusCode.NotFound);
            }

            mapper.Map(request, contest);
            await unitOfWork.Repository<Domain.Models.Entities.Contest>().UpdateAsync(contest);
            await unitOfWork.CompleteAsync();

            var responseDto = new ContestResponseDto
            {
                Id = contest.Id,
                Name = contest.Name
            };

            return await Response.SuccessAsync(responseDto, "Contest updated successfully", System.Net.HttpStatusCode.OK);
        }
    }
}
