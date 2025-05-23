﻿using CodeSphere.Domain.Abstractions;
using CodeSphere.Domain.Abstractions.Services;
using CodeSphere.Domain.Premitives;
using MediatR;

namespace CodeSphere.Application.Features.Contest.Queries.GetContestStanding
{
    public class GetContestStandingQuery : IRequest<Response>
    {
        public int ContestId { get; set; }
        public int Start { get; set; }
        public int Stop { get; set; }
        public GetContestStandingQuery(int contestId)
        {
            ContestId = contestId;
            Start = 0;
            Stop = 20;
        }
    }

    public class GetContestStandingQueryHandler : IRequestHandler<GetContestStandingQuery, Response>
    {
        private readonly ICacheService cacheService;
        private readonly IUnitOfWork unitOfWork;

        public GetContestStandingQueryHandler(ICacheService cacheService, IUnitOfWork unitOfWork)
        {
            this.cacheService = cacheService;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response> Handle(GetContestStandingQuery request, CancellationToken cancellationToken)
        {
            var contest = await unitOfWork.Repository<Domain.Models.Entities.Contest>().GetByIdAsync(request.ContestId);
            if (contest == null)
                return await Response.FailureAsync("Contest Not Found", System.Net.HttpStatusCode.NotFound);

            //if (contest.ContestStatus == ContestStatus.Running)
            //{
            //    // return the data from cache
            //    var leaderboard = cacheService.GetContestStanding(request.ContestId, request.Start, request.Stop);
            //    return await Response.SuccessAsync(leaderboard, "Contest Standing Fetched Successfully", System.Net.HttpStatusCode.OK);
            //}

            var standing = await unitOfWork.ContestRepository.GetContestStanding(request.ContestId, 0, 10);
            return await Response.SuccessAsync(standing, "Contest Standing Fetched Successfully", System.Net.HttpStatusCode.OK);


        }
    }
}
