using HieuEMart.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HieuEMart.Models
{
    public class ProductModel
    {
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Yêu cầu nhập tên Sản phẩm")]
		public string Name { get; set; }
		public string Slug { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập mô tả Sản phẩm")]
        [MinLength(4, ErrorMessage = "Mô tả Sản phẩm phải có ít nhất 4 ký tự")]
        public string Description { get; set; }
		[Required(ErrorMessage = "Yêu cầu nhập giá Sản phẩm")]
		[Range(0.01, double.MaxValue)]
		[Column(TypeName = "decimal(8, 2)")]
		public decimal Price { get; set; }
		[Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một thương hiệu")]
		public int BrandId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một danh mục")]
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public CategoryModel Category { get; set; }
		public BrandModel Brand { get; set; }


        [NotMapped]
        [FileExtension]
        public IFormFile? ImageUpload { get; set; }
    }
}
