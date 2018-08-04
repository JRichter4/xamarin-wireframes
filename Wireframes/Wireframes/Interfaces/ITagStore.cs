using System.Collections.Generic;
using System.Threading.Tasks;
using Wireframes.Models;

namespace Wireframes.Interfaces {
    public interface ITagStore {
        Task<Tag> GetTagById(int id);
        Task<IEnumerable<Tag>> GetAllTags();
        Task<int> AddTag(Tag tag);
        Task<int> UpdateTag(Tag tag);
        Task<int> DeleteTag(Tag tag);
    }
}
