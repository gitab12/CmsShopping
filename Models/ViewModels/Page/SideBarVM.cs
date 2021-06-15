using CmsShopping.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShopping.Models.ViewModels.Page
{
    public class SideBarVM
    {

        public SideBarVM()
        {

        }
        public SideBarVM(SidebarDto row)
        {
            id = row.id;
            Body = row.Body;

        }
        [Key]
        public int id { get; set; }
        
        [AllowHtml]
        public string Body { get; set; }
    }

}