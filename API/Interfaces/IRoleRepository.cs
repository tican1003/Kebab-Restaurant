using API.Entities;

namespace API.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role> CreateRoleAsync(Role role);
        void UpdateRole(Role role);
        Task<bool> SaveAllAsync();
    }
}
