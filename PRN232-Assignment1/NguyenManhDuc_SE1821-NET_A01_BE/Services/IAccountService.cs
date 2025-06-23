using BusinessObjects;

namespace Services
{
    public interface IAccountService
    {
        SystemAccount ValidateUser(string email, string password);
        List<SystemAccount> GetAllUsers();
        void DeleteUser(short id);
        void ToggleAccountStatus(short id);
        void UpdateUserRole(short id, int role);
        SystemAccount? GetUserById(short id);
        void UpdateAccount(SystemAccount account);
        bool ChangePassword(short userId, string currentPassword, string newPassword);
        void CreateUser(SystemAccount newUser);
    }
}
