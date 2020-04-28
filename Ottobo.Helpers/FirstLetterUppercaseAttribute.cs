using System.ComponentModel.DataAnnotations;

namespace Ottobo.Helpers
{      
    public class FirstLetterUppercaseAttribute : ValidationAttribute
    {
        protected  override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value== null || string.IsNullOrEmpty(value.ToString())){
                return ValidationResult.Success;
            }
            else{
                if(value.ToString()[0].ToString() != value.ToString()[0].ToString().ToUpper()){
                    return new ValidationResult("First letter should be uppercase.");
                }
            }

            return ValidationResult.Success;
        }
    }

    
}