using Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public interface ICommentService
    {
        IEnumerable<Comment> Create(int id, string text);
        string Delete(int id);
        IEnumerable<Comment> GetAllCommentsByPost(int Id);
    }
}