using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarateClub.Core;

public partial class BeltTest
{

    [Key]
    public int TestId { get; set; } // المفتاح الأساسي

    [Required(ErrorMessage = "معرف العضو مطلوب.")]
    public int MemberId { get; set; } // معرف العضو

    [Required(ErrorMessage = "معرف الرتبة مطلوب.")]
    public int RankId { get; set; } // معرف الرتبة

    [Required(ErrorMessage = "نتيجة الاختبار مطلوبة.")]
    public bool Result { get; set; } // نتيجة الاختبار

    [Required(ErrorMessage = "تاريخ الاختبار مطلوب.")]
    public DateTime Date { get; set; } // تاريخ الاختبار

    [Required(ErrorMessage = "معرف المدرب المطلوب.")]
    public int TestedByInstructorId { get; set; } // معرف المدرب الذي أجرى الاختبار

    [Required(ErrorMessage = "معرف الدفع مطلوب.")]
    public int PaymentId { get; set; } // معرف الدفع

    public virtual Member Member { get; set; } = null!;

    public virtual Payment? Payment { get; set; }

    public virtual BeltRank Rank { get; set; } = null!;

    public virtual Instructor TestedByInstructor { get; set; } = null!;
}
