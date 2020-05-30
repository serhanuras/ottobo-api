using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Ottobo.Api.Attributes
{
    public class ContentTypeValidator : ValidationAttribute
    {

        private readonly string[] _contentTypes;
        private readonly string[] imageContentTypes = new string[] { "image/jpeg", "image/png", "image/gif"};

        public ContentTypeValidator(string[] contentTypes)
        {
            this._contentTypes= contentTypes;
        }

        public ContentTypeValidator(ContentTypeGroup contentType)
        {

            switch(contentType)
            {
                case ContentTypeGroup.Image:
                    _contentTypes = imageContentTypes;
                break;
            }
           

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


            if(this._contentTypes.Contains(formFile.ContentType)){
                return ValidationResult.Success;
            }
            else{
                return new ValidationResult($"Content type should be { String.Join(",", this._contentTypes)} MB.");
            }
        }
    }

    public enum ContentTypeGroup
    {
        Image
    }


}