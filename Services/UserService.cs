using Models;
using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Services;


public class UserService
{
    private readonly IRepository _repo;

    public UserService(IRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<User> GetUserByLogin(string login)
    {

        return await _repo.GetUserByLogin(login);
    }

    [HttpPost]
    public async Task<User> CreateUser(User user)
    {
        var newUser = await _repo.CreateUser(user);
        return newUser;
    }

    [HttpGet]
    public async Task<User> GetUserById(int id)
    {
        return await _repo.GetUserById(id);
    }

}