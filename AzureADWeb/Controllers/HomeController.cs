using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AzureADWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;

namespace AzureADWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        
        
        return View();
    }
    
    
    
    public IActionResult LogIn()
    {
        var scheme = OpenIdConnectDefaults.AuthenticationScheme;

        var redirectUrl = Url.ActionContext.HttpContext.Request.Scheme + "://" +
                          Url.ActionContext.HttpContext.Request.Host;
        
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = redirectUrl
        }, scheme);
        
    }
    
    public IActionResult LogOut()
    {
        var scheme = OpenIdConnectDefaults.AuthenticationScheme;
        return SignOut(new AuthenticationProperties(), CookieAuthenticationDefaults.AuthenticationScheme, scheme);
        
    }

    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}