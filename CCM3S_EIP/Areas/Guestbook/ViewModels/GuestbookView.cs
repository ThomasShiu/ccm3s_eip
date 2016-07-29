using CCM3S_EIP.Areas.Guestbook.Service;
using CCM3S_EIP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM3S_EIP.Areas.Guestbook.ViewModels
{
    public class GuestbookView
    {
        //搜尋欄位
        [DisplayName("搜尋")]
        public string Search { get; set; }
        //顯示資料陣列
        public List<BU_GUESTBOOK> DataList { get; set; }

        //分頁內容
        public ForPaging Paging { get; set; }
    }
}
