using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MIRS.Application.DTOs;
using MIRS.Application.Interfaces;
using MIRS.Web.ViewModels;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace MIRS.Web.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public RegisterModel(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [BindProperty]
    public RegisterViewModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public void OnGet(string returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        if (ModelState.IsValid)
        {
            try
            {
                var registerDto = _mapper.Map<RegisterDto>(Input);
                var userDto = await _authService.RegisterAsync(registerDto);

                if (userDto != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userDto.FullName),
                        new Claim(ClaimTypes.Email, userDto.Email),
                    };

                    foreach (var role in userDto.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = userDto.ExpiresAt
                    };
                    await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity), authProperties);

                    HttpContext.Session.SetString("JWToken", userDto.Token);

                    return RedirectToPage("/Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        return Page();
    }
}
