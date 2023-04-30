using System.Threading.Tasks;
using lb1.Services.Abstractions;

namespace lb1;

public class App
{
    private readonly IUserService _userService;
    private readonly IResourceService _resourceService;
    private readonly ILoginService _loginService;
    private readonly IRegisterService _registerService;

    public App(
        IUserService userService,
        IResourceService resourceService,
        ILoginService loginService,
        IRegisterService registerService
        )
    {
        _userService = userService;
        _resourceService = resourceService;
        _loginService = loginService;
        _registerService = registerService;
    }

    public async Task Start()
    {
        _ = await _userService.GetUsersPage(2);
        _ = await _userService.GetUserById(2);
        _ = await _userService.GetUserById(23);

        _ = await _resourceService.GetResourcePage(1);
        _ = await _resourceService.GetResourceById(2);
        _ = await _resourceService.GetResourceById(23);

        _ = await _userService.CreateUser("morpheus", "leader");
        _ = await _userService.PutUpdateUser(2, "morpheus", "zion resident");
        _ = await _userService.PatchUpdateUser(2, "morpheus", "zion resident");
        await _userService.DeleteUser(2);

        _ = await _registerService.Register( "eve.holt@reqres.in", "pistol");
        _ = await _registerService.Register( "sydney@fife", null);

        _ = await _loginService.Login("eve.holt@reqres.in", "cityslicka");
        _ = await _loginService.Login("sydney@fife", null);

        Console.ReadKey();

    }
}