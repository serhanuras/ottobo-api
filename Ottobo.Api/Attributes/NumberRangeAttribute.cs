using System.ComponentModel.DataAnnotations;

namespace Ottobo.Api.Attributes
{
    public class NumberInRangeAttribute: ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;
        public NumberInRangeAttribute(int min, int max)
        {
            this._min = min;
            this._max = max;
        }
        protected  override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int intValue = (int) value;
            if(intValue >_min && intValue<=_max){
                return ValidationResult.Success;
            }
            else{
                return new ValidationResult($"Number should be between {this._min.ToString()} and {this._max.ToString()}");
            }
        }
    }
}