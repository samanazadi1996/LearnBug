using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Models.EntityModels
{
    internal class MessageMetaData
    {
     [Key]
        [ScaffoldColumn(false)]
        [Required]

        public int Id { get; set; }
        [ScaffoldColumn(false)]
        [Required]
        public int FromuserId { get; set; }
        [ScaffoldColumn(false)]
        [Required]
        public int TouserId { get; set; }
        public string Text { get; set; }
        [ScaffoldColumn(false)]
        [Required]
        public string Datetime { get; set; }
        [ScaffoldColumn(false)]
        [Required]
        public Nullable<long> Status { get; set; }


    }
}
namespace LearnBug.Models.DomainModels
{
    [MetadataType(typeof(LearnBug.Models.EntityModels.MessageMetaData))]
    public partial class Message
    {

    }
}


