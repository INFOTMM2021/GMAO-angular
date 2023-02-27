using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GMAO.Data;
using GMAO.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace GMAO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametresController : Controller
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;
        public ParametresController(GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [Route("GetMaxPrelev")]
        [HttpGet]
        public int GetMaxPrelev()
        {

            string query = @"select Valeur from Parametres where Cle='Prelev'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            var maxVal = table.Rows[0][0];
            return (Int32)maxVal;
        }



        [Route("UpdatePrelev")]
        [HttpPut]
        public void UpdatePrelev()
        {

            string query = @"update Parametres set Valeur=Valeur +1 where Cle='Prelev'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            
        }





        [Route("UpdateParametre")]
        [HttpPut("{cle}")]
        public async Task<IActionResult> Update(string cle)
        {
            var parametre = await _context.Parametres.FirstOrDefaultAsync(p => p.Cle == cle);

            if (parametre == null)
            {
                return NotFound();
            }

            switch (cle)
            {
                case "DemT":
                    parametre.Valeur++;
                    break;
                case "NOInter":
                    parametre.Valeur++;
                    break;
                case "NOPiece":
                    parametre.Valeur++;
                    break;
                case "NumSérie":
                    parametre.Valeur++;
                    break;
                case "Prelev":
                    parametre.Valeur++;
                    break;
                case "Recep":
                    parametre.Valeur++;
                    break;
                case "Regul":
                    parametre.Valeur++;
                    break;
                case "RetAtl":
                    parametre.Valeur++;
                    break;
                case "RetFour":
                    parametre.Valeur++;
                    break;
                default:
                    return BadRequest();
            }

            _context.Parametres.Update(parametre);
            await _context.SaveChangesAsync();

            return NoContent();
        }








        // GET: Parametres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parametres.ToListAsync());
        }

        // GET: Parametres/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametre = await _context.Parametres
                .FirstOrDefaultAsync(m => m.Cle == id);
            if (parametre == null)
            {
                return NotFound();
            }

            return View(parametre);
        }

        // GET: Parametres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parametres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cle,Valeur")] Parametres parametre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parametre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parametre);
        }

        // GET: Parametres/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametre = await _context.Parametres.FindAsync(id);
            if (parametre == null)
            {
                return NotFound();
            }
            return View(parametre);
        }

        // POST: Parametres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Cle,Valeur")] Parametres parametre)
        {
            if (id != parametre.Cle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parametre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParametreExists(parametre.Cle))
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
            return View(parametre);
        }

        // GET: Parametres/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametre = await _context.Parametres
                .FirstOrDefaultAsync(m => m.Cle == id);
            if (parametre == null)
            {
                return NotFound();
            }

            return View(parametre);
        }

        // POST: Parametres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var parametre = await _context.Parametres.FindAsync(id);
            _context.Parametres.Remove(parametre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParametreExists(string id)
        {
            return _context.Parametres.Any(e => e.Cle == id);
        }
    }
}
