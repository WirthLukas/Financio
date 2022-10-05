using Financio.Core.Entities;

namespace Financio.WebApi.DataTranferObjects;

public record CreateAccountDto(string Number, string Name, string? Description)
{
    public Account ToAccount() => new Account
    {
        Number = this.Number, Name = this.Name, Description = this.Description
    };
}

public record AccountDto(string Number, string Name, string? Description, byte[] RowVersion)
{
    public AccountDto(Account account) : this(account.Number, account.Name, account.Description, account.RowVersion)
    {
    }

    public AccountDto() : this(string.Empty, string.Empty, null, Array.Empty<byte>())
    {

    }
}