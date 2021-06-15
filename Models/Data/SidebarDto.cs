using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShopping.Models.Data
{
     [Table("tblSideBar")]
    
    public class SidebarDto
    {
       [Key]
       public int  id { get; set; }
      
        public string Body { get; set; }
    }
}