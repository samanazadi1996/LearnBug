using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class JsonResultViewmodel
    {
        public bool Success { get; set; }
        public string Html { get; set; }
        public string Scripts { get; set; }
    }
}