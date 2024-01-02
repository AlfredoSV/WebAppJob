﻿using Application.IServices;
using AutoMapper;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppJob.Models;

namespace WebAppJob.Controllers
{
    [Route("[controller]")]
    public class JobController : BaseController
    {
        #region Abstract services

        private readonly IMapper _mapper;
        private readonly IServiceJob _serviceJob;
        private readonly IServiceCatalog<Company> _serviceCatalogCompany;
        private readonly IServiceCatalog<Area> _serviceCatalogArea;

        #endregion

        #region Constructors
        public JobController(IServiceJob serviceJob, IMapper autoMapper, IServiceCatalog<Company> serviceCatalog, IServiceCatalog<Area> serviceCatalogArea)
        {
            _serviceCatalogArea = serviceCatalogArea;
            _serviceCatalogCompany = serviceCatalog;
            _serviceJob = serviceJob;
            _mapper = autoMapper;
        }

        #endregion

        #region Return Partial View

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetDetailPartial(Guid id)
        {
            try
            {
                IQueryCollection context = HttpContext.Request.Query;
                JobViewModel jobViewModel = new JobViewModel();
                Job job = (await _serviceJob.GetDetailJob(id)).Data;

                _mapper.Map(job, jobViewModel);

                jobViewModel.SelectListItemsAreas = (await
                    _serviceCatalogArea.GetAllAsync())
                    .ToList()
                    .Select(com => new SelectListItem() { Text = com.NameArea,
                        Value = com.Id.ToString() }).ToList();

                jobViewModel.SelectListItemsCompanies = (await
                    _serviceCatalogCompany.GetAllAsync())
                    .ToList()
                    .Select(com => new SelectListItem()
                    {
                        Text = com.NameCompany,
                        Value = com.Id.ToString()
                    }).ToList();

                return PartialView("_DetailJob", jobViewModel);
            }
            catch (CommonException ex)
            {
                return ReturnResponseErrorCommon(ex);
            }
            catch (Exception ex)
            {
                return ReturnResponseIncorrect(ex);
            }


        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> EditJob(Guid id)
        {
            try
            {
                JobViewModel jobViewModel = new JobViewModel();
                Job job = (await _serviceJob.GetDetailJob(id)).Data;

                _mapper.Map(job, jobViewModel);

                jobViewModel.SelectListItemsAreas = (await
                    _serviceCatalogArea.GetAllAsync())
                    .ToList()
                    .Select(com => new SelectListItem()
                    {
                        Text = com.NameArea,
                        Value = com.Id.ToString()
                    }).ToList();

                jobViewModel.SelectListItemsCompanies = (await
                    _serviceCatalogCompany.GetAllAsync())
                    .ToList()
                    .Select(com => new SelectListItem()
                    {
                        Text = com.NameCompany,
                        Value = com.Id.ToString()
                    }).ToList();

                return PartialView("_EditJob", jobViewModel);
            }
            catch (CommonException ex)
            {
                return ReturnResponseErrorCommon(ex);
            }
            catch (Exception ex)
            {
                return ReturnResponseIncorrect(ex);
            }
            
        } 

        [HttpPost("[action]")]
        public async Task<PartialViewResult> ListJobs(int page, int pageSize, string searchText, string citySearch)
        {
            List<JobViewModel> jobsResult = new List<JobViewModel>();

            DtoPaginationViewModel<JobViewModel> dtoPaginationViewModel =
            new DtoPaginationViewModel<JobViewModel>();

            DtoResponse<List<Job>> response;

            if (string.IsNullOrEmpty(searchText))
                searchText = string.Empty;

            if (string.IsNullOrEmpty(citySearch))
                citySearch = string.Empty;

            response = await _serviceJob.GetJobsList(page, pageSize, searchText, citySearch);

            _mapper.Map(response.Data, jobsResult);

            dtoPaginationViewModel.Data = jobsResult;
            dtoPaginationViewModel.PaginationViewModel = new PaginationViewModel()
            {
                TotalCount = response.Count,
                PageSize = pageSize,
                PageIndex = page++
            };

            return PartialView("_ListJobs", dtoPaginationViewModel);

        }

        #endregion

        #region Return object json for fetch request

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DetailJob(Guid id)
        {
            try
            {
                IQueryCollection context = HttpContext.Request.Query;
                JobViewModel jobViewModel = new JobViewModel();
                Job job = (await _serviceJob.GetDetailJob(id)).Data;

                _mapper.Map(job, jobViewModel);

                return Ok(jobViewModel);
            }
            catch (Exception ex)
            {

                return BadRequest(new { Error = "A error was ocurred, the ticket is: " });

            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateJob()
        {
            try
            {

                JobViewModel jobViewModel = new JobViewModel();

                jobViewModel.SelectListItemsAreas = (await
                    _serviceCatalogArea.GetAllAsync())
                    .ToList()
                    .Select(com => new SelectListItem()
                    {
                        Text = com.NameArea,
                        Value = com.Id.ToString()
                    }).ToList();

                jobViewModel.SelectListItemsCompanies = (await
                    _serviceCatalogCompany.GetAllAsync())
                    .ToList()
                    .Select(com => new SelectListItem()
                    {
                        Text = com.NameCompany,
                        Value = com.Id.ToString()
                    }).ToList();

                return PartialView("_CreateJob",jobViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "A error was ocurred, the ticket is: " + SaveErrror(ex) });

            }

        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            try
            {
                await _serviceJob.DeleteJob(new DtoRequest<Guid> { Data = id });
                return Ok(new { message = "The job was delete successful" });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Error = "A error was ocurred, the ticket is: " });

            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditJob([FromBody] JobViewModel jobView)
        {
            DtoRequest<Job> dtoRequ = new DtoRequest<Job>();
            dtoRequ.Data = new Job();
            Guid idError = Guid.NewGuid();
            try
            {
                if (ModelState.IsValid)
                {
                    _mapper.Map(jobView, dtoRequ.Data);
                    await _serviceJob.UpdateJob(dtoRequ);

                    return Ok(new { message = "The job was edited successful" });
                }
                else
                {
                    return BadRequest(new { message = "Please, check your form data." });
                }


            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "A error was ocurred, the ticket is: " + SaveErrror(ex) });
            }

        }

        #endregion

        #region Return View

        [HttpGet]
        public IActionResult ApplicationsJobs() => View();

        #endregion

    }
}
