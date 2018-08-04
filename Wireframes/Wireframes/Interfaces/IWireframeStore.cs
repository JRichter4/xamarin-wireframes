using System.Collections.Generic;
using System.Threading.Tasks;
using Wireframes.Models;

namespace Wireframes.Interfaces {
    public interface IWireframeStore {
        Task<Wireframe> GetWireframeById(int id);
        Task<IEnumerable<Wireframe>> GetAllWirefames();
        Task<int> AddWireframe(Wireframe wireframe);
        Task<int> UpdateWireframe(Wireframe wireframe);
        Task<int> DeleteWireframe(Wireframe wireframe);
    }
}
