using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhereIsMyShit.Models
{
    public class ItemModel
    {
        [Required]
        public string Name { get; set; }
    }
}