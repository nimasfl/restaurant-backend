using Microsoft.AspNetCore.Mvc.ModelBinding;
using Restaurant.App.Services.Authentication;

namespace Restaurant.App.Data.Models.Requester;

public class RequesterEntityBinder : IModelBinder
{
    private readonly AuthService _authService;

    public RequesterEntityBinder(AuthService authService)
    {
        _authService = authService;
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var user = bindingContext
            .HttpContext
            .User
            .Identities
            .SingleOrDefault(i => i.NameClaimType == "App");
        
        if (user == null)
        {
            return;
        }

        var userId = user.Claims.SingleOrDefault(c => c.Type == "Id")?.Value ?? null;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return;
        }
        
        var requester = await _authService.GetUserById(new Guid(userId));
        if (requester == null)
        {
            return;
        }
        bindingContext.Result = ModelBindingResult.Success(requester);
    }
}
