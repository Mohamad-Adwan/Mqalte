using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace ArticlProject.Core
{
   public class Category
    {
        [Required]
        [Display(Name="المعرف")]
        public int Id { get; set; }

        [Required (ErrorMessage ="هذا الحقل مطلوب")]
        [Display(Name = "اسم الصنف")]
        [MaxLength(50,ErrorMessage = "اعلى قيمة للادخال هي 50 حرف")]
        [MinLength(2, ErrorMessage = "اقل قيمة للادخال هي حرفان")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        //Navigation
        public virtual List<AuthorPost> AuthorPosts { get; set; }
    }
}
