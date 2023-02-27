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
    public class InterventionsController : Controller
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;

        public InterventionsController(GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [Route("GetAllIntervByNumDem")]
        [HttpGet("{NumDem}")]
        // GET: Intervention
        public JsonResult GetAllInterventionsMatricule([FromQuery] int NumDem)
        {
            string query = @"select * from Intervention where NumDem=@NumDem";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@NumDem", NumDem);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult(table);
        }




        
        [Route("GetAllInterv")]
        [HttpGet]
        // GET: Intervention
        public JsonResult GetAllInterventions()
        {
            string query = @"select * from Intervention";

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




        [Route("AddIntervention")]
        [HttpPost]
        // GET: Pieces/Create
        public JsonResult AddIntervention(Intervention Intv)
        {
            string query = @"insert into Intervention (NumDem, Matricule, Intervenant
                                                        ,TauxHoraire, DDebutEstim, HDebutEstim
                                                        ,DDebut, HDebut, NHeure, DateSaisie, HeureSaisie)
                                                values (@NumDem, @Matricule, @Intervenant
                                                        ,@TauxHoraire, @DDebutEstim, @HDebutEstim
                                                        ,@DDebut, @HDebut, @NHeure, @DateSaisie, @HeureSaisie)";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {

                    myCommand.Parameters.AddWithValue("@NumDem", Intv.NumDem);
                    myCommand.Parameters.AddWithValue("@Matricule", Intv.Matricule);
                    myCommand.Parameters.AddWithValue("@Intervenant", Intv.Intervenant);
                    myCommand.Parameters.AddWithValue("@TauxHoraire", Intv.TauxHoraire);
                    myCommand.Parameters.AddWithValue("@DDebutEstim", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@HDebutEstim", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@DDebut", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@HDebut", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@NHeure", Intv.NHeure);
                    myCommand.Parameters.AddWithValue("@DateSaisie", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@HeureSaisie", DateTime.Now);

                    //myCommand.Parameters.AddWithValue("@DFin", Intv.DFin);
                    //myCommand.Parameters.AddWithValue("@HFin", Intv.HFin);
                    //myCommand.Parameters.AddWithValue("@DFinEstim", Intv.DFinEstim);
                    //myCommand.Parameters.AddWithValue("@HFinEstim", Intv.HFinEstim);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("add succ");
        }


        [Route("UpdateIntervention")]
        [HttpPut]
        // GET: Intervention/Edit/5
        public JsonResult UpdateIntervention(Intervention Intv)
        {
            
            string query = @"Update Intervention set 
                            (NumDem=@NumDem, Matricule=@Matricule, Intervenant=@Intervenant, Categorie=@Categorie,
                            TauxHoraire=@TauxHoraire, DDebutEstim=@DDebutEstim, HDebutEstim=@HDebutEstim, DFinEstim=@DFinEstim, 
                            HFinEstim=@HFinEstim, DDebut=@DDebut, HDebut=@HDebut, DFin=@DFin, HFin=@HFin, NHeure=@NHeure,
                            DateSaisie=@DateSaisie, HeureSaisie=@HeureSaisie where NOInter=@NOInter) ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;
            double nbh = (Intv.DateSaisie - Intv.DFin).TotalHours;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@NOInter", Intv.NOInter);
                    myCommand.Parameters.AddWithValue("@NumDem", Intv.NumDem);
                    myCommand.Parameters.AddWithValue("@Matricule", Intv.Matricule);
                    myCommand.Parameters.AddWithValue("@Intervenant", Intv.Intervenant);
                    myCommand.Parameters.AddWithValue("@Categorie", Intv.Categorie);
                    myCommand.Parameters.AddWithValue("@TauxHoraire", Intv.TauxHoraire);
                    myCommand.Parameters.AddWithValue("@DDebutEstim", Intv.DDebutEstim);
                    myCommand.Parameters.AddWithValue("@HDebutEstim", Intv.HDebutEstim);
                    myCommand.Parameters.AddWithValue("@DFinEstim", Intv.DFinEstim);
                    myCommand.Parameters.AddWithValue("@HFinEstim", Intv.HFinEstim);
                    myCommand.Parameters.AddWithValue("@DDebut", Intv.DDebut);
                    myCommand.Parameters.AddWithValue("@HDebut", Intv.HDebut);
                    myCommand.Parameters.AddWithValue("@DFin", Intv.DFin);
                    myCommand.Parameters.AddWithValue("@HFin", Intv.HFin);
                    myCommand.Parameters.AddWithValue("@Nheure", nbh);
                    myCommand.Parameters.AddWithValue("@DateSaisie", Intv.DateSaisie);
                    myCommand.Parameters.AddWithValue("@HeureSaisie", Intv.HeureSaisie);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("updated");
        }



        [Route("DeleteIntervention")]
        [HttpDelete("{NOInter}")]
        // GET: Intervention/Delete/5
        public JsonResult DeleteIntervention(string NOInter)
        {
            string query = @"delete from Intervention where NOInter=@NOInter";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@NOInter", NOInter);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("Deleted");
        }



        
        
        public async Task<IActionResult> Details(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Intervention
                .FirstOrDefaultAsync(m => m.NOInter == id);
            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // GET: Interventions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Interventions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NOInter,NumDem,Matricule,Intervenant,Categorie,TauxHoraire,DDebutEstim,HDebutEstim,DFinEstim,HFinEstim,DDebut,HDebut,DFin,HFin,Nheure,DateSaisie,HeureSaisie")] Intervention intervention)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intervention);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(intervention);
        }

        // GET: Interventions/Edit/5
        public async Task<IActionResult> Edit(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Intervention.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }
            return View(intervention);
        }

        // POST: Interventions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(float id, [Bind("NOInter,NumDem,Matricule,Intervenant,Categorie,TauxHoraire,DDebutEstim,HDebutEstim,DFinEstim,HFinEstim,DDebut,HDebut,DFin,HFin,Nheure,DateSaisie,HeureSaisie")] Intervention intervention)
        {
            if (id != intervention.NOInter)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intervention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterventionExists(intervention.NOInter))
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
            return View(intervention);
        }

        // GET: Interventions/Delete/5
        public async Task<IActionResult> Delete(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _context.Intervention
                .FirstOrDefaultAsync(m => m.NOInter == id);
            if (intervention == null)
            {
                return NotFound();
            }

            return View(intervention);
        }

        // POST: Interventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(float id)
        {
            var intervention = await _context.Intervention.FindAsync(id);
            _context.Intervention.Remove(intervention);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterventionExists(float id)
        {
            return _context.Intervention.Any(e => e.NOInter == id);
        }
    }
}
