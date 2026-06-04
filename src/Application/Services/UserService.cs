using System.Security.Cryptography;
using StockManager.Application.DTO;
using StockManager.Domain.Entities;
using StockManager.Domain.Exceptions;
using StockManager.Domain.Interfaces;

namespace StockManager.Application.Services;

public class UserService(
    IUserRepository repository,
    IEmailService emailService
)
{
    public async Task<ResponseUserDTO> Create(CreateUserDTO userDTO)
    {
        if(await repository.EmailExists(userDTO.Email))
            throw new EmailAlreadyExistsException("email already exists");

        var user = new User(userDTO.Name, userDTO.Email, BCrypt.Net.BCrypt.HashPassword(userDTO.Password));

        await repository.Create(user);
        await repository.SaveChanges();

        return ToDTO(user);
    }

    public async Task<ResponseUserDTO> GetUser(int id) 
        => ToDTO(await GetUserOrThrow(id));

    public async Task<IEnumerable<ResponseUserDTO>> GetAllUser()
    {
        var users = await repository.GetAllUsers();

        return users.Select(u => ToDTO(u));
    }

    public async Task Delete(int id)
    {
        var user = await GetUserOrThrow(id);

        repository.Delete(user);
        await repository.SaveChanges();
    }

    public async Task<ResponseUserDTO> Update(int id, UpdateUserDTO userDTO)
    {
        var user = await GetUserOrThrow(id);

        if (await repository.EmailExists(userDTO.Email))
            throw new EmailAlreadyExistsException("email already exists");

        user.Update(userDTO.Name, userDTO.Email);
        await repository.SaveChanges();

        return ToDTO(user);
    }

    public async Task ForgotPassword(string email)
    {
        var user = await GetUserByEmailOrThrow(email);
        
        var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

        await emailService.ResetPasswordEmail(code, user.Email, user.Name);

        var resetPassword = new UserResetPassword(email, code, user.Id);

        await repository.CreateResetPassword(resetPassword);
        await repository.SaveChanges();
    }

    public async Task ResetPassword(string email, string code, string password)
    {
        var user = await GetUserByEmailOrThrow(email);
        var resetPassword = await repository.GetResetPasswordCode(code, user.Id)
            ?? throw new Exception("invalid code");
        
        user.UpdatePassword(BCrypt.Net.BCrypt.HashPassword(password));
        resetPassword.Used();

        await repository.SaveChanges();
    }

    private async Task<User> GetUserByEmailOrThrow(string email)
    {
        var user = await repository.GetUserByEmail(email)
            ?? throw new UserNotFoundException("user not found");
        return user;
    }

    private async Task<User> GetUserOrThrow(int id)
    {
        var user = await repository.GetUser(id)
            ?? throw new UserNotFoundException("user not found");
        return user;
    }

    private static ResponseUserDTO ToDTO(User user)
    {
        return new ResponseUserDTO()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}
