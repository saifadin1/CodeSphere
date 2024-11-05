﻿using FluentValidation;

namespace CodeSphere.Application.Features.Problem.Commands.Create
{
    public class CreateProblemCommandValidator : AbstractValidator<CreateProblemCommand>
    {
        public CreateProblemCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(5).MaximumLength(30);
            RuleFor(x => x.Description).NotEmpty().NotNull().MinimumLength(10).MaximumLength(4000);
            RuleFor(x => x.Difficulty).NotEmpty().NotNull();
            RuleFor(x => x.ContestId).NotEmpty().NotNull();
            RuleFor(x => x.ProblemSetterId).NotEmpty().NotNull();
            RuleFor(x => x.RunTimeLimit).NotEmpty().NotNull();
            RuleFor(x => x.MemoryLimit).NotEmpty().NotNull();

        }
    }
}
