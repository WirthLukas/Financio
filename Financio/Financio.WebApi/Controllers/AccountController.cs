using Financio.Core.Contracts;
using Financio.Core.Entities;
using Financio.WebApi.DataTranferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace Financio.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<AccountDto[]> GetAll()
        {
            return (await _unitOfWork.Accounts.GetAsync()).Select(a => new AccountDto(a)).ToArray();
        }

        [HttpGet("{number}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<AccountDto>> GetByNumber(string number)
        {
            Account? account = await _unitOfWork.Accounts.GetByIdAsync(number);

            if (account is null)
            {
                return NotFound($"Account with number {number} not found");
            }

            return Ok(new AccountDto(account));
        }

        [HttpGet("filterbyname/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<AccountDto[]>> FilterByName(string name)
        {
            var accounts = await _unitOfWork.Accounts.GetAsync(
                filter: a => a.Name.Contains(name),
                orderBy: query => query.OrderBy(a => a.Number));

            return Ok(accounts.Select(a => new AccountDto(a)).ToArray());
        }

        [HttpGet("filterbynumber/{number}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<AccountDto[]>> FilterByNumber(string number)
        {
            var accounts = await _unitOfWork.Accounts.GetAsync(
                filter: a => a.Number.Contains(number),
                orderBy: query => query.OrderBy(a => a.Number));

            return Ok(accounts.Select(a => new AccountDto(a)).ToArray());
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<Account>> CreateAccount(CreateAccountDto accountData)
        {
            // This happens automatically by AspNet Core
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var account = accountData.ToAccount();
            await _unitOfWork.Accounts.AddAsync(account);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetByNumber), new { number = account.Number }, account);
        }

        [HttpPut("{number}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<AccountDto>> UpdateAccount(string number, [FromBody] AccountDto account)
        {
            if (number != account.Number)
            {
                return BadRequest("Number from Route is not equal with Number from body!");
            }

            Account? accountInDb = await _unitOfWork.Accounts.GetByIdAsync(number);

            if (accountInDb is null)
            {
                return NotFound($"Device with number {number} not found!");
            }

            // TODO: changes
            //accountInDb.Name = account.Name;
            //accountInDb.Description = account.Description;
            

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(account);
        }

        [HttpDelete("{number}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<AccountDto>> DeleteAccount(string number)
        {
            Account? account = await _unitOfWork.Accounts.GetByIdAsync(number);

            if (account is null)
            {
                return NotFound($"Account with number {number} not found!");
            }

            _unitOfWork.Accounts.Remove(account);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new AccountDto(account));
        }
    }
}
