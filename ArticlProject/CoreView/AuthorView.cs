using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ArticlProject.CoreView
{
    public class AuthorView
    {
        [Required]
        [Display(Name = "المعرف")]
        public int Id { get; set; }
        [Required]
        [Display(Name = " معرف المستخدم")]

        public string UserId { get; set; }
        [Required]
        [Display(Name = "اسم المستخدم")]

        public string UserName { get; set; }
        [Required]
        [Display(Name = "الاسم الكامل")]

        public string FullName { get; set; }
        [Display(Name = "الصورة")]

        public IFormFile ProfileImgaeUrl { get; set; }
        [Display(Name = "السيرة الذاتيه")]

        public string Bio { get; set; }
        [Display(Name = "فيسبوك")]

        public string FaceBook { get; set; }
        [Display(Name = "انستقرام")]

        public string Instgram { get; set; }
        [Display(Name = "تويتر")]

        public string Twitter { get; set; }
    }
}
