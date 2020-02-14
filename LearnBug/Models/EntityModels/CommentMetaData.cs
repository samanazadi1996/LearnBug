using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Models.EntityModels
{
    internal class CommentMetaData
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public int userId { get; set; }
        public int contentId { get; set; }
        public string Text { get; set; }
        [ScaffoldColumn(false)]
        public string Datetime { get; set; }
    }

}

namespace LearnBug.Models.DomainModels
{
    [MetadataType(typeof(LearnBug.Models.EntityModels.CommentMetaData))]
    public partial class Comment
    {
    }
}


