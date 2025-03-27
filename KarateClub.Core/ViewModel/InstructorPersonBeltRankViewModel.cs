using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateClub.Core.ViewModel
{
    public class InstructorPersonBeltRankViewModel
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public string ContactInfo { get; set; } = null!;

        public string? Qualification { get; set; }// مؤهلات المدرب

        public virtual List<BeltRank> BeltRanks { get; set; }


    }
}
