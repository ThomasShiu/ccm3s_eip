using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM3S_EIP.Areas.Chart.Models
{
    public class ChartModel
    {
    }


    //自訂一個類別，裡面有兩個List<int>的屬性，Category1跟Category2
    //等等出來的報表會有兩條線
    public class ChartData
    {
        public List<string> xBar_list { get; set; }
        public List<int?> a_cnt_list { get; set; }
        public List<int?> o_cnt_list { get; set; }
    }

    //Linq 欄位轉型
    public class ColData
    {
        public DateTime cdate;
        public int? a_cnt;
        public int? o_cnt;
    }
}
