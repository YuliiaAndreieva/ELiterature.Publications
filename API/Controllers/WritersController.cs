using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WritersController : ControllerBase
{
    private readonly IWritersService _writersService;
    
    public WritersController(
        IWritersService writersService)
    {
        _writersService = writersService;
    }
    
    public IActionResult Index()
    {
        return default;
    }
}