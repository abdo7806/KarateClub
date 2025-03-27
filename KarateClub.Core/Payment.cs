using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarateClub.Core
{
    public partial class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "المبلغ المدفوع مطلوب.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "يجب أن يكون المبلغ المدفوع أكبر من 0.")]
        public decimal Amount { get; set; } // المبلغ المدفوع

        [Required(ErrorMessage = "تاريخ الدفع مطلوب.")]
        public DateTime Date { get; set; } // تاريخ الدفع

        [Required(ErrorMessage = "معرف العضو مطلوب.")]
        public int MemberId { get; set; } // مفتاح خارجي يشير إلى العضو الذي قام بالدفع

        public virtual ICollection<BeltTest> BeltTests { get; set; } = new List<BeltTest>();

        [Required(ErrorMessage = "العضو مطلوب.")]
        public virtual Member Member { get; set; } = null!;

        public virtual ICollection<SubscriptionPeriod> SubscriptionPeriods { get; set; } = new List<SubscriptionPeriod>();
    }
}