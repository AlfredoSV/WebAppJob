﻿
using System;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IRepositoryJob
    {
        PaginationList<List<Job>> ListJobsByPage(int page, int pageSize, string search);
        
    }
}