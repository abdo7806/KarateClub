using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarateClub.Core
{
    // المدربين
    public partial class Instructor
    {
        [Key]
        public int InstructorId { get; set; }

        [Required(ErrorMessage = "معرف الشخص مطلوب.")]
        public int PersonId { get; set; }

        [StringLength(200, ErrorMessage = "يجب أن يكون طول المؤهل أقل من 200 حرف.")]
        public string? Qualification { get; set; } // مؤهلات المدرب

        public virtual ICollection<BeltTest> BeltTests { get; set; } = new List<BeltTest>();

        public virtual ICollection<MemberInstructor> MemberInstructors { get; set; } = new List<MemberInstructor>();

        [Required(ErrorMessage = "الشخص مطلوب.")]
        public virtual Person Person { get; set; } = null!;
    }
}