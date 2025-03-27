using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateClub.Core.ViewModel
{
    //نموذج عرض لوحة التحكم
    public class DashboardViewModel
    {
        // عدد الأعضاء في النادي
        public int MemberCount { get; set; }

        // عدد المدربين في النادي
        public int InstructorCount { get; set; }

        // عدد اختبارات الحزام التي تم إجراؤها
        public int TestCount { get; set; }

        // عدد المدفوعات المتأخرة
        public int PaymentDueCount { get; set; }
        public int SubscriptionCount { get; set; }


        // قائمة بمصادر الإيرادات
        public List<RevenueSource> RevenueSources { get; set; }

        // كلاس داخلي يمثل مصدر الإيرادات
        public class RevenueSource
        {
            // اسم مصدر الإيرادات (مثل اشتراكات، اختبارات، إلخ)
            public string Name { get; set; }

            // النسبة المئوية من الإيرادات التي يمثلها هذا المصدر
            public double Percentage { get; set; }
        }
    }
}
