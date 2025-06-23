using BusinessObjects;
using DataAccessObjects;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _accountDAO = new AccountDAO();

        public List<SystemAccount> GetAllUsers()
        {
            return _accountDAO.GetAllUsers();
        }

        public void DeleteUser(short userId)
        {
            _accountDAO.DeleteUser(userId);
        }

        public SystemAccount GetUserByEmailAndPassword(string email, string password)
        {
            return _accountDAO.GetUserByEmailAndPassword(email, password);
        }

        public void ToggleAccountStatus(short id)
        {
            _accountDAO.ToggleAccountStatus(id);
        }

        public void UpdateUserRole(short id, int role)
        {
            _accountDAO.UpdateUserRole(id, role);
        }

        public SystemAccount? GetUserById(short id)
        {
            return _accountDAO.GetUserById(id);
        }

        public void UpdateAccount(SystemAccount account)
        {
            _accountDAO.UpdateAccount(account);
        }

        public bool ChangePassword(short userId, string currentPassword, string newPassword)
        {
            return _accountDAO.ChangePassword(userId, currentPassword, newPassword);
        }

        public void CreateUser(SystemAccount newUser)
        {

            _accountDAO.CreateUser(newUser);
        }
    }
}
