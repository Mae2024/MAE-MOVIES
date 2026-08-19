using mae_movie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace mae_movie.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)   //constructor de la clase UsuarioController de usermanager y signInManager
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(
                    usuario.Email,
                    usuario.Clave,
                    isPersistent: false,
                    lockoutOnFailure: false);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                // Login failed: show a single general error and return Registro view with minimal model
                ModelState.Clear();
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
                var vm = new UsuarioViewModel { Email = usuario.Email };
                return View("Registro", vm);
            }

            // If modelstate invalid (login data invalid), return Registro with provided email only
            var vmInvalid = new UsuarioViewModel { Email = usuario?.Email };
            return View("Registro", vmInvalid);
        }


        public IActionResult Registro()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]  //validacion de token para evitar ataques CSRF (Cross-Site Request Forgery), 
        public async Task<IActionResult> Registro(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                //logica para registrar el usuario en la base de datos
                var nuevousuario = new Usuario
                {
                    UserName = usuario.Email,
                    Email = usuario.Email,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    ImagenPerfilUrl = "default-profile.jpg"
                };
                var resultado = await _userManager.CreateAsync(nuevousuario, usuario.Clave);

                if (resultado.Succeeded)    //si el resultado es exitoso, se inicia la sesion del usuario y se redirige a la pagina principal
                {
                    await _signInManager.SignInAsync(nuevousuario, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View("Registro", usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> MiPerfil()
        {
            var usuarioActual = await _userManager.GetUserAsync(User);
            
            var usuariovm = new MiPerfilViewModel
            {
                Nombre = usuarioActual.Nombre,
                Apellido = usuarioActual.Apellido,
                Email = usuarioActual.Email,
                //ImagenPerfilUrl = usuarioActual.ImagenPerfilUrl
            };

            return View(usuariovm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MiPerfil(MiPerfilViewModel usuariovm)
        {
            if (ModelState.IsValid)
            {
                var usuarioActual = await _userManager.GetUserAsync(User);

                usuarioActual.Nombre = usuariovm.Nombre;
                usuarioActual.Apellido = usuariovm.Apellido;
              
                var resultado = await _userManager.UpdateAsync(usuarioActual);
                if (resultado.Succeeded)
                {
                    ViewBag.Mensaje = "Perfil actualizado correctamente";
                    return View(usuariovm);
                }
                else
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(usuariovm);

        }
    }
}
