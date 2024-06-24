using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace HieuEMart.Repository.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName); //123.jpg
                string[] extensions = { "jpg", "png", "jpeg" };
                bool result = extensions.Any(x => extension.EndsWith(x, System.StringComparison.OrdinalIgnoreCase));

                if (!result)
                {
                    return new ValidationResult("Chỉ cho phép ảnh có dạng là: JPG, PNG, JPEG");
                }
            }
            return ValidationResult.Success;
        }
    }
}
