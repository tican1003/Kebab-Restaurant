using API.Entities;

namespace API.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role> GetRoleByIdAsync(int id);
        Task<Role> CreateRoleAsync(Role role);
        void DeleteRole(Role role);
        Task<bool> SaveAllAsync();
        Task GetRoleByIdAsync(object roleId);
    }
}
