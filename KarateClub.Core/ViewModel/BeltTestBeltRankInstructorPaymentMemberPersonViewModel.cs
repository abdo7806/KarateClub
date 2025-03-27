using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateClub.Core.ViewModel
{
    public class BeltTestBeltRankInstructorPaymentMemberPersonViewModel
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int RankId { get; set; }

        // نتيجة الاحتبار
        // true => ناجح
        // fales => راسب
        public string Result { get; set; }

        public DateTime Date { get; set; }

        public int TestedByInstructorId { get; set; }//معرف المدرب الذي أجرى الاختبار.

        public int PaymentId { get; set; }






        public string RankName { get; set; } = null!;

        public decimal TestFees { get; set; }



        public decimal Amount { get; set; }//(المبلغ) - المبلغ المدفوع.



        public int PersonId { get; set; }

        public string EmergencyContactInfo { get; set; } = null!;

        public int LastBeltRank { get; set; }

        public bool IsActive { get; set; }




        public string Name { get; set; } = null!;
        public string InstructorName { get; set; } = null!;

        public string? Address { get; set; }

        public string ContactInfo { get; set; } = null!;



        // الاعضاء
        public virtual List<MemberPersonBeltRankViewModel> Members { get; set; }
        // المدربين
        public virtual List<InstructorPersonBeltRankViewModel> Instructors { get; set; }
        public virtual List<BeltRank> BeltRanks { get; set; }



    }
}
