using CCM3S_EIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM3S_EIP.Areas.Album.Models
{
    public class UploadItemViewModel
    {
        public PU_ALBUMS pu_album { get;set; }
        /// <summary>
        /// 加這個，當MemberViewModel new出來時，存取PhotoFileNames才不會發生NULL例外
        /// </summary>
        public List<string> _PhotoFileNames = new List<string>();
        /// <summary>
        /// 多張大頭照的檔案名稱
        /// </summary>
        public List<string> PhotoFileNames { get { return this._PhotoFileNames; } set { this._PhotoFileNames = value; } }
    }
}
