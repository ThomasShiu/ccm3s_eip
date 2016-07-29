using System.Collections.Generic;
using CCM3S_EIP.Models;

namespace CCM3S_EIP.Areas.SRV.ViewModels
{
    public class SrvProdViewModel
    {
        //來自CCM3S_EIP.Areas.SRV.Models中的表格類別
        public SRVPRODMT srvprodmt_data
        {
            get; set;
        }

        public IEnumerable<SRVPRODDL> srvproddl_list
        {
            get; set;
        }

        public IEnumerable<V_SRVDATAMT> vsrvdatamt_list
        {
            get; set;
        }
    }
}
