using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Ottobo.Api.Helpers
{
    public class FileSizeValidatorAttribute : ValidationAttribute
    {

        private readonly int maxFileSizeInMbs;
        public FileSizeValidatorAttribute(int MaxFileSizeInMbs)
        {

            this.maxFileSizeInMbs = MaxFileSizeInMbs;

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {

                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if(formFile.Length > maxFileSizeInMbs * 1024 * 1024){
                return new ValidationResult($"File size cannot be bigger than {maxFileSizeInMbs} MB.");
            }



            return ValidationResult.Success;
        }
    }


}