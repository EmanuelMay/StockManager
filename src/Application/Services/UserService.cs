using StockManager.Application.DTO;
using StockManager.Domain.Entities;
using StockManager.Domain.Exceptions;
using StockManager.Domain.Interfaces;

namespace StockManager.Application.Services;

public class UserService(
    IUserRepository repository
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

    private async Task<User> GetUserOrThrow(int id)
    {
        var user = await repository.GetUser(id) ?? throw new UserNotFoundException("user not found");
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
