﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppJob.Models
{
    public class JobViewModel
    {
        public Guid Id { get; set; }
        public string NameJob { get; set; }
        public Guid IdCompany { get; set; }
        public decimal SalaryMax { get; set; }
        public decimal SalaryMin { get; set; }
        public int VacancyNumbers { get; set; }
        public Guid IdArea { get; set; }
        public string DescriptionJob { get; set; }
        public Guid IdUserCreated { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public string Logo { get; set; } 
        public string Tags { get; set; } 
        public List<SelectListItem> SelectListItemsAreas { get; set; }
        public List<SelectListItem> SelectListItemsCompanies { get; set; }

    }
}
