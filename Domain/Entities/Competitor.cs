﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("competitor")]
    public class Competitor
    {
        public Guid Id { get; set; }
        public string NameCompetitor { get; set; }
        public string LastNameCompetitor { get; set; }
        public DateTime BirthdayDate { get; set; }
        public int Age { get; set; }
        public Guid IdEnglishLevel { get; set; }
        public string Cv { get; set; }
        public int YearsOfExperience { get; set; }
        public Guid IdLastGradeStudies { get; set; }
        public Guid IdCivilStatus { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdUserCreated { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
