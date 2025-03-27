using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarateClub.Core
{
    // الاشتراكات
    public partial class SubscriptionPeriod
    {
        [Key]
        public int PeriodId { get; set; } // المفتاح الأساسي

        [Required(ErrorMessage = "تاريخ البدء مطلوب.")]
        public DateTime StartDate { get; set; } // تاريخ بداية الاشتراك

        [Required(ErrorMessage = "تاريخ الانتهاء مطلوب.")]
        public DateTime EndDate { get; set; } // تاريخ انتهاء الاشتراك

        [Required(ErrorMessage = "الرسوم مطلوبة.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "يجب أن تكون الرسوم أكبر من 0.")]
        public decimal Fees { get; set; } // رسوم الاشتراك

        public bool Paid { get; set; } // هل هو مدفوع أم لا

        [Required(ErrorMessage = "معرف العضو مطلوب.")]
        public int MemberId { get; set; } // مفتاح خارجي يشير إلى العضو

        public int? PaymentId { get; set; } // مفتاح خارجي يشير إلى الدفع المرتبط بالاشتراك

        [Required(ErrorMessage = "العضو مطلوب.")]
        public virtual Member Member { get; set; } = null!;

        public virtual Payment? Payment { get; set; }
    }
}