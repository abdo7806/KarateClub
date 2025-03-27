using KarateClub.Core;

namespace KarateClub.Core.ViewModel
{
    public class MemberPersonBeltRankViewModel
    {
    
        public int MemberPersonBeltRankViewModelId { get; set; }

        public int PersonId { get; set; }

        // معلومات الاتصال في حالة الطوارء
        public string EmergencyContactInfo { get; set; } = null!;

        // اخر رتبة حصل عليها
        public int LastBeltRank { get; set; }// مفتاح اجنبي مرتبط باجدول الرتب

        public bool IsActive { get; set; }




        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public string ContactInfo { get; set; } = null!;



        public string RankName { get; set; } = null!;





        public virtual BeltRank LastBeltRankNavigation { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;

        public virtual List<BeltRank> BeltRanks { get; set; }

    }
}
