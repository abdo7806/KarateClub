using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;
namespace KarateClub.Core
{

    public class User : IdentityUser<int>
    {
        [Key]
        public int UserID { get; set; } // معرف فريد لكل مستخدم

        [Required(ErrorMessage = "اسم المستخدم مطلوب.")]
        [StringLength(50)]
        public string Username { get; set; } // اسم المستخدم
        public string UserName { get; set; } // اسم المستخدم

       // [Required]
      //  public string PasswordHash { get; set; } // تجزئة كلمة المرور

        [Required]
        public string Role { get; set; } // دور المستخدم (مثل عضو، مدرب، إداري)
        [Required]
        public int PersonId { get; set; } // دور المستخدم (مثل عضو، مدرب، إداري)
        public bool IsActive { get; set; } = true; // دور المستخدم (مثل عضو، مدرب، إداري)
        

        public virtual Person Person { get; set; } = null;


        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "يجب أن تتكون كلمة المرور من 6 أحرف على الأقل.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "يجب أن تحتوي كلمة المرور على حرف كبير واحد على الأقل، حرف صغير واحد على الأقل، ورقم واحد.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "يرجى تأكيد كلمة المرور.")]
        [Compare("PasswordHash", ErrorMessage = "كلمات المرور غير متطابقة.")]
        public string ConfirmPassword { get; set; }
    }
}