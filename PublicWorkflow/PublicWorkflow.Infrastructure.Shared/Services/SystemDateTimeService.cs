﻿using PublicWorkflow.Application.Interfaces.Shared;
using System;

namespace PublicWorkflow.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}