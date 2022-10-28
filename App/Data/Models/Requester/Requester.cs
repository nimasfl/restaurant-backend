using Microsoft.AspNetCore.Mvc;

namespace Restaurant.App.Data.Models.Requester;

[ModelBinder(BinderType = typeof(RequesterEntityBinder))]
public class Requester
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
}
