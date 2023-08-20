﻿using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IServiceJob
    {
        DtoResponse<List<Job>> GetJobsList(int take, int skip, string search);
        void CreateJob(DtoRequest<Job> dtoRequest);
        DtoResponse<Job> GetDetailJob(Guid jobId);
        DtoResponse<ApplyCompetitorJob> ApplyNewJob(Guid idCompetitor,
            Guid idCreated, Guid idJob);

    }
}
