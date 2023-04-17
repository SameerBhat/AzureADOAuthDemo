using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    // by default the defaul scheme is cookies, but here we are still specifying it explicitly.
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;




}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme).AddOpenIdConnect(
    OpenIdConnectDefaults.AuthenticationScheme,
    options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        
        // To get authority, go to OpenID Connect metadata document endpoint and copy the value of "issuer" property.
        options.Authority = "https://login.microsoftonline.com/5cb3d92b-e3ef-4332-866f-c77e97db25fd/v2.0";
        
        // this is the app id/ client id of the app that we are trying to access.
        options.ClientId = "f5c87a9f-f8ff-4df3-ade5-c102c4a9f5f6";
        
        // we have set in our app redirect uris tab to be "ID tokens (used for implicit and hybrid flows)", we are using implicit flow here.
        // this basically means we want identity token to be returned in the callback url from the authorization endpoint.
        options.ResponseType = "id_token";
        
        
        options.SaveTokens = true;
        
        
        
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


// app id/ client id: f5c87a9f-f8ff-4df3-ade5-c102c4a9f5f6
// Oauth 2 authorization end point:  https://login.microsoftonline.com/5cb3d92b-e3ef-4332-866f-c77e97db25fd/oauth2/v2.0/authorize

//OpenID Connect metadata document endpoint:
// https://login.microsoftonline.com/5cb3d92b-e3ef-4332-866f-c77e97db25fd/v2.0/.well-known/openid-configuration


// Install this package: Microsoft.AspNetCore.Authentication.OpenIdConnect - ASP.NET Core middleware that enables an application to support the OpenID Connect authentication workflow.
// Install this package as well: Microsoft.Identity.Web - This package enables ASP.NET Core web apps and web APIs to use the Microsoft identity platform (formerly Azure AD v2.0).

