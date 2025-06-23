using BusinessObjects;

namespace Repositories
{
    public interface IAccountRepository
    {
        AccountNumber GetAccountById(string accountId);
    }
}
