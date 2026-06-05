using StockManager.Application.DTO;

namespace StockManager.Domain.Interfaces.Services;

public interface IUserService
{
    public Task<ResponseUserDTO> Add(CreateUserDTO userDTO);

    public Task<ResponseUserDTO> GetById(int id);

    public Task Remove(int id);

    public Task<ResponseUserDTO> Update(int id, UpdateUserDTO userDTO);

    public Task ForgotPassword(string email);

    public Task ResetPassword(ResetPasswordUserDTO resetDTO);

    public Task<IEnumerable<ResponseUserDTO>> GetAllUser();
}
