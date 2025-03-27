using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarateClub.Core
{
    public partial class Person
    {
        [Key]
        public int PersonId { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب.")]
        [StringLength(100, ErrorMessage = "يجب أن يكون طول الاسم أقل من 100 حرف.")]
        public string Name { get; set; } = null!;

        [StringLength(200, ErrorMessage = "يجب أن يكون طول العنوان أقل من 200 حرف.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "معلومات الاتصال مطلوبة.")]
        [StringLength(100, ErrorMessage = "يجب أن يكون طول معلومات الاتصال أقل من 100 حرف.")]
        public string ContactInfo { get; set; } = null!;

        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

        public virtual ICollection<Member> Members { get; set; } = new List<Member>();
    }
}