//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CCM3S_EIP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PU_ALBUMS
    {
        public int Id { get; set; }
        public Nullable<int> parentId { get; set; }
        public Nullable<int> childId { get; set; }
        public string FileName { get; set; }
        public string Descript { get; set; }
        public string ImgPath { get; set; }
        public string KindType1 { get; set; }
        public string KindType2 { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateEmp { get; set; }
        public Nullable<int> Size { get; set; }
        public string Prod_no { get; set; }
        public Nullable<System.DateTime> Pdate { get; set; }
    }
}