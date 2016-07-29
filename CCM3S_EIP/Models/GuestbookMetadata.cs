using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM3S_EIP.Models
{
    // 定義Guestbook資料表的驗證
    [MetadataType(typeof(GuestbookMetadata))]
    public partial class BU_GUESTBOOK
    {
        private class GuestbookMetadata
        {
            [DisplayName("編號:")]
            public int id { get; set; }

            [DisplayName("名字:")]
            [Required(ErrorMessage = "請輸入名字")]
            [StringLength(20, ErrorMessage = "名字不可超過20字元")]
            public string Name { get; set; }

            [DisplayName("留言內容:")]
            [Required(ErrorMessage = "請輸入留言內容")]
            [StringLength(100, ErrorMessage = "留言內容不可超過100字元")]
            [DataType(DataType.MultilineText)]
            public string Content { get; set; }

            [DisplayName("新增時間:")]
            public DateTime CreateTime { get; set; }

            [DisplayName("回覆內容:")]
            [StringLength(100, ErrorMessage = "回覆內容不可超過100字元")]
            [DataType(DataType.MultilineText)]
            public string Reply { get; set; }

            [DisplayName("回覆時間:")]
            public DateTime ReplyTime { get; set; }

        }
    }
}
