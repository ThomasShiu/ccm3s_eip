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
    [MetadataType(typeof(AlbumMetadata))]
    public partial class PU_ALBUMS
    {
        public static implicit operator PU_ALBUMS(List<PU_ALBUMS> v)
        {
            throw new NotImplementedException();
        }

        private class AlbumMetadata
        {
            [DisplayName("編號:")]
            public int Id { get; set; }
            [DisplayName("父階:")]
            public int parentId { get; set; }
            [DisplayName("子階:")]
            public int childId { get; set; }

            [DisplayName("相簿名稱:")]
            [Required(ErrorMessage = "請輸入相簿名稱")]
            [StringLength(50, ErrorMessage = "名稱不可超過50字元")]
            public string FileName { get; set; }

            [DisplayName("檔案大小:")]
            public int Size { get; set; }

            [DisplayName("描述:")]
            [Required(ErrorMessage = "請輸入相簿描述")]
            [StringLength(500, ErrorMessage = "描述不可超過500字元")]
            [DataType(DataType.MultilineText)]
            public string Descript { get; set; }

            [DisplayName("圖檔路徑:")]
            public string ImgPath { get; set; }

            [DisplayName("標籤1:")]
            [Required(ErrorMessage = "請輸入標籤，例如:篩選機、成型機、震動盤")]
            [StringLength(500, ErrorMessage = "標籤內容不可超過500字元")]
            public string KindType1 { get; set; }

            [DisplayName("標籤2:")]
            public string KindType2 { get; set; }

            [DisplayName("機號:")]
            public string Prod_no { get; set; }

            [DisplayName("生產日期:")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            [DataType(DataType.Date)]
            public DateTime Pdate { get; set; }

            [DisplayName("新增日期:")]
            public DateTime CreateDate { get; set; }

            [DisplayName("新增人員:")]
            public string CreateEmp { get; set; }
        }
    }
}
