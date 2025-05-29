using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookSale.Management.Domain.Attributes
{
    public class ImageValidationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;

        public ImageValidationAttribute(string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_allowedExtensions.Contains(extension))
                {
                    return new ValidationResult(ErrorMessage ?? "Invalid image file format.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
