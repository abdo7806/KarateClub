using KarateClub.Core.ViewModel;
using System;
using System.Collections.Generic;

namespace KarateClub.Core;

public partial class MemberInstructor
{

    public int MemberId { get; set; }

    public int InstructorId { get; set; }

    public DateTime AssignDate { get; set; }

    public virtual Instructor Instructor { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;

    // الاعضاء
    public virtual List<MemberPersonBeltRankViewModel> Members { get; set; }
    // المدربين
    public virtual List<InstructorPersonBeltRankViewModel> Instructors { get; set; }
}
