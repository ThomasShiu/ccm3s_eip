using CCM3S_EIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM3S_EIP.Areas.MIS.Services
{
    class IP_AddressCUIDServices
    {
        public EIP01Entities db = new EIP01Entities();

        public List<IP_Address> IP_Address_GetData()
        {
            return (db.IP_Address.ToList());
        }

        public void IP_Address_DBCreate
            (string ip_addr,
            string mac_addr,
            string dept, 
            string user, 
            string device, 
            string os, 
            string group, 
            string antivirus,
            string remark)
        {
            IP_Address NewData = new IP_Address();
            
            NewData.ip_addr =  ip_addr;
            NewData.mac_addr =  mac_addr;
            NewData.dept =  dept;
            NewData.user = user;
            NewData.device =  device;
            NewData.os = os;
            NewData.group = group;
            NewData.antivirus = antivirus;
            NewData.remark = remark;
            db.IP_Address.Add(NewData);
            db.SaveChanges();

        }
    }
}
