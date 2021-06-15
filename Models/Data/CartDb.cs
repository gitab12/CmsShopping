using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CmsShopping.Models.Data
{
    public class CartDb : DbContext
    {
      public DbSet<PageDTO> Pages { get; set; }
        public DbSet<SidebarDto> Sidebar { get; set; }
    }
}