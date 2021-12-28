using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Mail.IdentityServer.Data;
using Mail.IdentityServer.Models;
using Mail.IdentityServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mail.IdentityServer.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _context;

        public AuthController(
            IIdentityServerInteractionService interactionService,
            DataContext context,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _context = context;
            _interactionService = interactionService;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("[action]")]
        public async Task<IActionResult> LoginAsync(string returnUrl)
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalProviders = externalProviders
            });
        }

        [Route("[action]")]
        public async Task<IActionResult> Logout (string logoutId)
        {           
            await _signInManager.SignOutAsync();
            var result = await _interactionService.GetLogoutContextAsync(logoutId);
            if (string.IsNullOrEmpty(result.PostLogoutRedirectUri))
            {
                return RedirectToAction("Index", "Site");
            }

            return Redirect(result.PostLogoutRedirectUri);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "User not found");
                return View(model);
            }

            var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (signinResult.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }
            ModelState.AddModelError("UserName", "Something went wrong");
            return View(model);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = new User(vm.UserName);
            IdentityResult result = await _userManager.CreateAsync(user, vm.Password);
            await _userManager.CreateAsync(user, vm.Password);

            _userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult(); //add role User

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return Redirect(vm.ReturnUrl);
            }

            return View();
        }

        [Route("[action]")]
        public async Task<IActionResult> ExternalLoginStart(string provider, string returnUrl)
        {
            var redirectUri = Url.Action(nameof(ExteranlLoginCallback), "Auth", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUri);
            return Challenge(properties, provider);
        }

        [Route("[action]")]
        public async Task<IActionResult> ExteranlLoginCallback(string returnUrl)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var result = await _signInManager
                .ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            var username = info.Principal.FindFirst(ClaimTypes.Name.Replace(" ", "_")).Value;
            return View("ExternalRegister", new ExternalRegisterViewModel
            {
                UserName = username,
                ReturnUrl = returnUrl
            });
        }

        [Route("[action]")]
        public async Task<IActionResult> ExternalRegister(ExternalRegisterViewModel vm)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var user = new User(vm.UserName);
            var result = await _userManager.CreateAsync(user);
            _userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult(); //add role User

            if (!result.Succeeded)
            {
                return View(vm);
            }

            result = await _userManager.AddLoginAsync(user, info);

            if (!result.Succeeded)
            {
                return View(vm);
            }

            await _signInManager.SignInAsync(user, false);

            return Redirect(vm.ReturnUrl);
        }

        [HttpGet("[action]")]
        public IActionResult Roles()
        {
            var roles = _context.Roles.ToList();
            var users = _context.Users.ToList();
            var userRoles = _context.UserRoles.ToList();

            return View(new DisplayViewModel
            {
                Roles = roles,//.Select(x => x.NormalizedName),
                Users = users,//.Select(x => x.NormalizedUserName),
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRole(Role vm)
        {
            await _roleManager.CreateAsync(new Role { Name = vm.Name });

            return RedirectToAction("Roles");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUserRole(UpdateUserRoleViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.User.Id.ToString());

            if (vm.Delete)
                await _userManager.RemoveFromRoleAsync(user, vm.Role.Name);
            else
                await _userManager.AddToRoleAsync(user, vm.Role.Name);

            return RedirectToAction("Roles");
        }
    }
}
