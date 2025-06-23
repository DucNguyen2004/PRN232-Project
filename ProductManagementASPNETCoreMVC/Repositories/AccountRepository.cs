using BusinessObjects;
using DataAccessObjects;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public AccountNumber GetAccountById(string accountId)
        {
            return AccountDAO.GetAccountById(accountId);
        }
    }
}
