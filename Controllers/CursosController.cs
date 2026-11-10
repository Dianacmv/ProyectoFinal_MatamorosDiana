
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_MatamorosDiana.Models;
using ProyectoFinal_MatamorosDiana.Data;
using Microsoft.AspNetCore.Authorization;


[Authorize(Roles = "Administrador")]
public class CursosController : Controller
{
    private readonly ApplicationDbContext _context;

    public CursosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Cursos
    public async Task<IActionResult> Index()
    {
        var cursos = _context.Cursos.Include(c => c.Carrera);
        return View(await cursos.ToListAsync());
    }

    // GET: Cursos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos
            .Include(c => c.Carrera)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (curso == null)
        {
            return NotFound();
        }

        return View(curso);
    }

    // GET: Cursos/Create
    public IActionResult Create()
    {
        ViewBag.CarreraId = new SelectList(
            _context.Carreras, "Id", "Nombre");

        return View();
    }

    // POST: Cursos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Codigo,Nombre,Creditos,Horario,CarreraId")] Curso curso)
    {
        if (!await _context.Carreras.AnyAsync(c => c.Id == curso.CarreraId))
        {
            ModelState.AddModelError("CarreraId",
                "Debe seleccionar una carrera válida.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.CarreraId = new SelectList(
            _context.Carreras, "Id", "Nombre", curso.CarreraId);

        return View(curso);
    }

    // GET: Cursos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos.FindAsync(id);

        if (curso == null)
        {
            return NotFound();
        }

        ViewBag.CarreraId = new SelectList(
            _context.Carreras, "Id", "Nombre", curso.CarreraId);

        return View(curso);
    }

    // POST: Cursos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Codigo,Nombre,Creditos,Horario,CarreraId")] Curso curso)
    {
        if (id != curso.Id)
        {
            return NotFound();
        }

        if (!await _context.Carreras.AnyAsync(c => c.Id == curso.CarreraId))
        {
            ModelState.AddModelError("CarreraId",
                "Debe seleccionar una carrera válida.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(curso);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(curso.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.CarreraId = new SelectList(
            _context.Carreras, "Id", "Nombre", curso.CarreraId);

        return View(curso);
    }

    // GET: Cursos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos
            .Include(c => c.Carrera)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (curso == null)
        {
            return NotFound();
        }

        return View(curso);
    }

    // POST: Cursos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);

        if (curso != null)
        {
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool CursoExists(int id)
    {
        return _context.Cursos.Any(e => e.Id == id);
    }
}
