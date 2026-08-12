using mae_movie.Models;
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
        [ValidateAntiForgeryToken]  //validacion de token para evitar ataques CSRF (Cross-Site Request Forgery), 
        public async Task<IActionResult> Login( UsuarioViewModel usuario)
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
            return View(usuario);
        }
   
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registro(string user)
        {
            return View();
        }
        public IActionResult Logout() 
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
