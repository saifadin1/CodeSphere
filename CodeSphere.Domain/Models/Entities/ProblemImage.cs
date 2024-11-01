﻿using CodeSphere.Domain.Premitives;

namespace CodeSphere.Domain.Models.Entities
{
    public class ProblemImage : BaseEntity
    {
        public Guid ProblemId { get; set; }
        public string ImagePath { get; set; }
        public Problem Problem { get; set; }
    }
}