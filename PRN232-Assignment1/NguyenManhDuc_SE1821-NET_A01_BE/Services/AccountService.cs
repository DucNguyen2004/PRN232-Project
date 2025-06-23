using BusinessObjects;
using Repository;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository = new AccountRepository();

        private readonly string _adminEmail;
        private readonly string _adminPassword;
        public AccountService(string adminEmail, string adminPassword)
        {
            _adminEmail = adminEmail;
            _adminPassword = adminPassword;
        }

        public List<SystemAccount> GetAllUsers()
        {
            return _accountRepository.GetAllUsers();
        }

        public void DeleteUser(short userId)
        {
            _accountRepository.DeleteUser(userId);
        }

        public SystemAccount ValidateUser(string email, string password)
        {
            // Check if the input matches the default admin credentials
            if (email == _adminEmail && password == _adminPassword)
            {
                return new SystemAccount
                {
                    AccountEmail = _adminEmail,
                    AccountPassword = _adminPassword,
                    AccountName = "Administrator",
                    AccountRole = 0
                };
            }

            // Otherwise, check in the database
            return _accountRepository.GetUserByEmailAndPassword(email, password);
        }

        public void ToggleAccountStatus(short id)
        {
            _accountRepository.ToggleAccountStatus(id);
        }

        public void UpdateUserRole(short id, int role)
        {
            _accountRepository.UpdateUserRole(id, role);
        }

        public SystemAccount? GetUserById(short id)
        {
            return _accountRepository.GetUserById(id);
        }

        public void UpdateAccount(SystemAccount account)
        {
            _accountRepository.UpdateAccount(account);
        }

        public bool ChangePassword(short userId, string currentPassword, string newPassword)
        {
            return _accountRepository.ChangePassword(userId, currentPassword, newPassword);
        }

        public void CreateUser(SystemAccount newUser)
        {
            _accountRepository.CreateUser(newUser);
        }
    }
}
