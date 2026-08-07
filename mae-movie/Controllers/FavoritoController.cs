
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mae_movie.Models;
using mae_movie.DATA;

public class FavoritoController : Controller
{
    private readonly MovieDbContext _context;

    public FavoritoController(MovieDbContext context)
    {
        _context = context;
    }

    // GET: FAVORITOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Favoritos.ToListAsync());
    }

    // GET: FAVORITOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var favorito = await _context.Favoritos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (favorito == null)
        {
            return NotFound();
        }

        return View(favorito);
    }

    // GET: FAVORITOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: FAVORITOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,UsuarioId,Usuario,PeliculaId,Pelicula,Fecha")] Favorito favorito)
    {
        if (ModelState.IsValid)
        {
            _context.Add(favorito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(favorito);
    }

    // GET: FAVORITOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var favorito = await _context.Favoritos.FindAsync(id);
        if (favorito == null)
        {
            return NotFound();
        }
        return View(favorito);
    }

    // POST: FAVORITOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,UsuarioId,Usuario,PeliculaId,Pelicula,Fecha")] Favorito favorito)
    {
        if (id != favorito.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(favorito);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoritoExists(favorito.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(favorito);
    }

    // GET: FAVORITOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var favorito = await _context.Favoritos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (favorito == null)
        {
            return NotFound();
        }

        return View(favorito);
    }

    // POST: FAVORITOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var favorito = await _context.Favoritos.FindAsync(id);
        if (favorito != null)
        {
            _context.Favoritos.Remove(favorito);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool FavoritoExists(int? id)
    {
        return _context.Favoritos.Any(e => e.Id == id);
    }
}
