using BusinessObjects;
using Repositories;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iAccountRepository;

        public AccountService()
        {
            iAccountRepository = new AccountRepository();
        }

        public AccountNumber GetAccountById(string accountId)
        {
            return iAccountRepository.GetAccountById(accountId);
        }
    }
}
