﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CCM_MainEntities : DbContext
    {
        public CCM_MainEntities()
            : base("name=CCM_MainEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ITEM> ITEM { get; set; }
        public virtual DbSet<ITTIN> ITTIN { get; set; }
        public virtual DbSet<vdaddr> vdaddr { get; set; }
        public virtual DbSet<VENDOR> VENDOR { get; set; }
        public virtual DbSet<customer> customers { get; set; }
    }
}
