using BusinessObjects;

namespace Services
{
    public interface IAccountService
    {
        AccountNumber GetAccountById(string accountId);
    }
}
