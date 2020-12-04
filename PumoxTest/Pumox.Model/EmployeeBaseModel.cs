using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Pumox.Model
{
    public enum JobTitleEnum
    {
        Undefined = 0,
        Administrator,
        Developer,
        Architect,
        Manager,
    }

    public abstract class EmployeeBaseModel
    {
        protected JobTitleEnum _JobTitle;
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string JobTitle
        {
            get
            {
                return (_JobTitle != JobTitleEnum.Undefined) ? _JobTitle.ToString()
                    : JobTitleEnum.Undefined.ToString();
            }
            set
            {
                if (JobTitleCompare(value))
                {
                    TextInfo textInfo = new CultureInfo("pl-PL", false).TextInfo;
                    Enum.TryParse<JobTitleEnum>(textInfo.ToTitleCase(value), out _JobTitle);
                }
            }
        }
        protected bool JobTitleCompare(string jobTitle)
        {
            foreach (var item in Enum.GetValues(typeof(JobTitleEnum)))
            {
                if (item.ToString().ToLower() == jobTitle.ToLower())
                    return true;
            }
            return false;
        }
    }
}
