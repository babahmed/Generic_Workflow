﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Interfaces.Service
{
    public interface IPublishService
    {
        Task PublishProcess(long processId);
    }
}