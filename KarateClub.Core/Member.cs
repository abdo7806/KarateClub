using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarateClub.Core
{
    public partial class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "معرف الشخص مطلوب.")]
        public int PersonId { get; set; }

        [Required(ErrorMessage = "معلومات الاتصال الطارئ مطلوبة.")]
        [StringLength(100, ErrorMessage = "يجب أن يكون طول معلومات الاتصال الطارئ أقل من 100 حرف.")]
        public string EmergencyContactInfo { get; set; } = null!;

        [Required(ErrorMessage = "يجب تحديد آخر رتبة حزام.")]
        public int LastBeltRank { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<BeltTest> BeltTests { get; set; } = new List<BeltTest>();

        public virtual BeltRank LastBeltRankNavigation { get; set; } = null!;

        public virtual ICollection<MemberInstructor> MemberInstructors { get; set; } = new List<MemberInstructor>();

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        [Required(ErrorMessage = "الشخص مطلوب.")]
        public virtual Person Person { get; set; } = null!;

        public virtual ICollection<SubscriptionPeriod> SubscriptionPeriods { get; set; } = new List<SubscriptionPeriod>();
    }
}