using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarateClub.Core
{
    public partial class BeltRank
    {
        [Key]
        public int RankId { get; set; } // المفتاح الأساسي

        [Required(ErrorMessage = "اسم الرتبة مطلوب.")]
        [StringLength(100, ErrorMessage = "يجب ألا يتجاوز اسم الرتبة 100 حرف.")]
        public string RankName { get; set; } = null!; // اسم الرتبة

        [Required(ErrorMessage = "الرسوم مطلوبة.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "يجب أن تكون الرسوم أكبر من 0.")]
        public decimal TestFees { get; set; } // رسوم الاختبار

        public virtual ICollection<BeltTest> BeltTests { get; set; } = new List<BeltTest>(); // اختبارات الحزام

        public virtual ICollection<Member> Members { get; set; } = new List<Member>(); // الأعضاء
    }
}