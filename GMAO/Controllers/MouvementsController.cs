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
    public class MouvementsController : Controller
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;
        public MouvementsController(GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        [Route("AddMouvement")]
        [HttpPost]
        // Post: AddMouvement/Create
        public JsonResult Add(Mouvement fp)
        {
            string query = @"insert into Mouvement (NumMvt,Type,Tier,DateMvt,Ref,CentreCout,UserName,DateSaisie,HeureSaisie)
                                    values (@NumMvt,@Type,@Tier,@DateMvt,@Ref,@CentreCout,@UserName,@DateSaisie,@HeureSaisie)";

            
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@NumMvt", fp.NumMvt);
                    myCommand.Parameters.AddWithValue("@Type", fp.Type);
                    myCommand.Parameters.AddWithValue("@Tier", fp.Tier);
                    myCommand.Parameters.AddWithValue("@DateMvt", DateTime.Now );
                    myCommand.Parameters.AddWithValue("@Ref", fp.Ref);
                    myCommand.Parameters.AddWithValue("@CentreCout", fp.CentreCout);
                    myCommand.Parameters.AddWithValue("@UserName", fp.UserName);
                    myCommand.Parameters.AddWithValue("@DateSaisie", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@HeureSaisie", DateTime.Now);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("add succ");
        }




        [Route("GetAllMouvements")]
        [HttpGet]
        // GET: Mouvements
        public JsonResult GetAllMouvements()
        {
            string query = @"select * from Mouvement";

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
            return new JsonResult(table);
        }


        [Route("DeleteMouvement")]
        [HttpDelete("{NumMvt}")]

        public JsonResult DeleteMouvemen(string NumMvt)
        {
            string query = @"delete from Mouvemen where NumMvt=@NumMvt";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@NumMvt", NumMvt);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("Deleted");
        }
















        // GET: Mouvements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mouvement.ToListAsync());
        }

        // GET: Mouvements/Details/5
        public async Task<IActionResult> Details(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mouvement = await _context.Mouvement
                .FirstOrDefaultAsync(m => m.NumMvt == id);
            if (mouvement == null)
            {
                return NotFound();
            }

            return View(mouvement);
        }

        // GET: Mouvements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mouvements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumMvt,Type,Tier,DateMvt,Ref,CentreCout,UserName,DateSaisie,HeureSaisie")] Mouvement mouvement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mouvement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mouvement);
        }

        // GET: Mouvements/Edit/5
        public async Task<IActionResult> Edit(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mouvement = await _context.Mouvement.FindAsync(id);
            if (mouvement == null)
            {
                return NotFound();
            }
            return View(mouvement);
        }

        // POST: Mouvements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(float id, [Bind("NumMvt,Type,Tier,DateMvt,Ref,CentreCout,UserName,DateSaisie,HeureSaisie")] Mouvement mouvement)
        {
            if (id != mouvement.NumMvt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mouvement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MouvementExists(mouvement.NumMvt))
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
            return View(mouvement);
        }

        // GET: Mouvements/Delete/5
        public async Task<IActionResult> Delete(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mouvement = await _context.Mouvement
                .FirstOrDefaultAsync(m => m.NumMvt == id);
            if (mouvement == null)
            {
                return NotFound();
            }

            return View(mouvement);
        }

        // POST: Mouvements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(float id)
        {
            var mouvement = await _context.Mouvement.FindAsync(id);
            _context.Mouvement.Remove(mouvement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MouvementExists(float id)
        {
            return _context.Mouvement.Any(e => e.NumMvt == id);
        }
    }
}
