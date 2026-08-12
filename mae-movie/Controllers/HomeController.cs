using mae_movie.DATA;
using mae_movie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;


namespace mae_movie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieDbContext _context;

        public HomeController(ILogger<HomeController> logger, MovieDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int? generoId = null, int? plataformaId = null)
        {

            
            const int pageSize = 6;

            // base query including navigation props
            var consulta = _context.Peliculas
                .Include(p => p.Genero)
                .Include(p => p.Plataforma)
                .AsQueryable();

            // apply filters
            if (generoId.HasValue)
            {
                consulta = consulta.Where(p => p.GeneroId == generoId.Value);
            }

            if (plataformaId.HasValue)
            {
                consulta = consulta.Where(p => p.PlataformaId == plataformaId.Value);
            }

            var totalCount = await consulta.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (totalPages < 1) totalPages = 1;

            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;

            var peliculas = await consulta
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // pass pagination and filter state to view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SelectedGeneroId = generoId;
            ViewBag.SelectedPlataformaId = plataformaId;

            // lists for filter buttons
            ViewBag.Generos = await _context.Generos.OrderBy(g => g.Descripcion).ToListAsync();
            ViewBag.Plataformas = await _context.Plataformas.OrderBy(p => p.Nombre).ToListAsync();

            return View(peliculas);
        }

        public IActionResult Details(int id)
        {
            var pelicula = _context.Peliculas
                .Include(p => p.Genero)
                .FirstOrDefault(p => p.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);
        }
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
}
