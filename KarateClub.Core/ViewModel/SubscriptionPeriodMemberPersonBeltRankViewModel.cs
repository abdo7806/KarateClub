using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateClub.Core.ViewModel
{
    
    public class SubscriptionPeriodMemberPersonBeltRankViewModel
    {


        public int Id { get; set; }//(معرف الفترة) - المفتاح الأساسي.

        public DateTime StartDate { get; set; }//(تاريخ البدء) - تاريخ بداية الاشتراك.

        public DateTime EndDate { get; set; }//(تاريخ الانتهاء) - تاريخ انتهاء الاشتراك.

        public decimal Fees { get; set; }//(الرسوم) - رسوم الاشتراك.

        public bool Paid { get; set; }// هل هوا مدفوع او لا

        public int MemberId { get; set; }//(معرف العضو) - مفتاح خارجي يشير إلى العضو.

        public int PaymentId { get; set; }// (معرف الدفع) - مفتاح خارجي يشير إلى الدفع المرتبط بالاشتراك.

        public virtual Member Member { get; set; } = null!;

        public virtual Payment? Payment { get; set; }



        public int PersonId { get; set; }

        // معلومات الاتصال في حالة الطوارء
        public string EmergencyContactInfo { get; set; } = null!;


        public bool IsActive { get; set; }




        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public string ContactInfo { get; set; } = null!;



        public string RankName { get; set; } = null!;





        public DateTime PaymentDate { get; set; }//(التاريخ) - تاريخ الدفع.



        public virtual BeltRank LastBeltRankNavigation { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;

        public virtual List<BeltRank> BeltRanks { get; set; }
        public virtual List<MemberPersonBeltRankViewModel> Members { get; set; }
    }
}
