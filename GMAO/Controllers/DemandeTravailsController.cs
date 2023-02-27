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
    public class DemandeTravailsController : Controller
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;
        public DemandeTravailsController(GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        

        [Route("GetAllDemandesTravails")]
        [HttpGet]
        // GET: Pieces
        public JsonResult GetAllDemandesTravails()
        {
            string query = @"select NumDem,Nature,Contact,Description,Status,FORMAT(DateDem,'dd/MM/yyyy') as DateDem from DemTravail where Status <> 'Effectue'  ORDER BY DateDem DESC";
            
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

       

        [Route("DeleteDemandeTravail")]
        [HttpDelete("{NumDem}")]
        // GET: Pieces/Delete/5
        public JsonResult DeleteDemandeTravail(string NumDem)
        {
            string query = @"delete from DemTravail where NumDem=@NumDem";
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

            return new JsonResult("Deleted");
        }



        [Route("AddDemTravail")]
        [HttpPost]
        // GET: DemandeTravail/Create
        public JsonResult AddDemandeTravail(DemandeTravail fp)
        {

            string query = @"insert into DemTravail (Contact,CentreCout,DateDem,HeureDem,Nature,Consommable,Description,NatPanne,DateSouh,Status)
                                    values (@Contact,@CentreCout,@DateDem,@HeureDem,@Nature,@Consommable,@Description,@NatPanne,@DateSouh,@Status)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    //myCommand.Parameters.AddWithValue("@NumDem", fp.NumDem);
                    myCommand.Parameters.AddWithValue("@Contact", fp.Contact);
                    myCommand.Parameters.AddWithValue("@CentreCout", fp.CentreCout);
                    myCommand.Parameters.AddWithValue("@DateDem", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@HeureDem", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@Nature", fp.Nature);
                    myCommand.Parameters.AddWithValue("@Consommable",fp.Consommable);
                    myCommand.Parameters.AddWithValue("@Description", fp.Description);
                    myCommand.Parameters.AddWithValue("@NatPanne", fp.NatPanne);
                    myCommand.Parameters.AddWithValue("@DateSouh", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@Service", fp.Service);
                    //myCommand.Parameters.AddWithValue("@Departement", fp.Departement);
                    //myCommand.Parameters.AddWithValue("@CodeIntervention", fp.CodeIntervention);
                    //myCommand.Parameters.AddWithValue("@UserId", fp.UserId);
                    //myCommand.Parameters.AddWithValue("@UserName", fp.UserName);
                    //myCommand.Parameters.AddWithValue("@CodeEquipement", fp.CodeEquipement);
                    //myCommand.Parameters.AddWithValue("@Designation", fp.Designation);
                    myCommand.Parameters.AddWithValue("@Status", "En cours");
                    //myCommand.Parameters.AddWithValue("@TEffect", fp.TEffect);
                 
                    //myCommand.Parameters.AddWithValue("@DateDiag", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@DateDInter", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@DateDiagClo", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@DatePreClo", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@DateClo", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@DateFinT", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@HeureFinT", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@DateValid", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@HeureValid", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@Reception", fp.Reception);
                    //myCommand.Parameters.AddWithValue("@DateValidSys", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@HeureValidSys", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@ArretMachine", fp.ArretMachine);
                    //myCommand.Parameters.AddWithValue("@MotifValid", fp.MotifValid);
                    //myCommand.Parameters.AddWithValue("@NumEquipement", fp.NumEquipement);
                    //myCommand.Parameters.AddWithValue("@CodeEmplacement", fp.CodeEmplacement);


                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("add succ");
        }

        



        [Route("UpdateDemTravail")]
        [HttpPut]
        // GET: DemTravail/Edit/5
        public JsonResult UpdateDemTravail( DemandeTravail fp)
        {
            string query = @"update DemTravail set 
            Contact=@Contact,  Description=@Description,
            Status=@Status, TEffect=@TEffect,  ArretMachine=@ArretMachine, DateSouh=@DateSouh, DateFinT=@DateFinT where NumDem=@NumDem";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                                    
                    myCommand.Parameters.AddWithValue("@Contact", fp.Contact);
                    myCommand.Parameters.AddWithValue("@Description", fp.Description);
                    myCommand.Parameters.AddWithValue("@Status", fp.Status);
                    myCommand.Parameters.AddWithValue("@TEffect", fp.TEffect);
                    myCommand.Parameters.AddWithValue("@ArretMachine", "N");
                    myCommand.Parameters.AddWithValue("@NumDem", fp.NumDem);
                    myCommand.Parameters.AddWithValue("@DateFinT", fp.DateFinT);
                    myCommand.Parameters.AddWithValue("@DateSouh", fp.DateSouh);


                    //myCommand.Parameters.AddWithValue("@DateDiag", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@DateDInter", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@DateDiagClo", DateTime.Now);
                    //
                    //myCommand.Parameters.AddWithValue("@DateClo", fp.DateClo);
                    //myCommand.Parameters.AddWithValue("@NatPanne", fp.NatPanne);
                    //myCommand.Parameters.AddWithValue("@Nature", fp.Nature);
                    //myCommand.Parameters.AddWithValue("@Consommable", fp.Consommable);
                    //myCommand.Parameters.AddWithValue("@DateValid", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@HeureValid", DateTime.Now.Hour);
                    //myCommand.Parameters.AddWithValue("@Reception", fp.Reception);
                    //myCommand.Parameters.AddWithValue("@MotifValid", fp.MotifValid);
                    //myCommand.Parameters.AddWithValue("@TypePanne", fp.TypePanne);
                    //myCommand.Parameters.AddWithValue("@UserId", fp.UserId);
                    //myCommand.Parameters.AddWithValue("@UserName", fp.UserName);
                    // myCommand.Parameters.AddWithValue("@Service", fp.Service);
                    // myCommand.Parameters.AddWithValue("@Departement", fp.Departement);
                    // myCommand.Parameters.AddWithValue("@HeureDem", fp.DateDem.Hour);
                    //myCommand.Parameters.AddWithValue("@CodeIntervention", fp.CodeIntervention);
                    // myCommand.Parameters.AddWithValue("@CodeEquipement", fp.CodeEquipement);
                    //myCommand.Parameters.AddWithValue("@Designation", fp.Designation);
                    //myCommand.Parameters.AddWithValue("@HeureFinT", fp.DateFinT.Hour);
                    //myCommand.Parameters.AddWithValue("@DateValidSys", DateTime.Now);
                    // myCommand.Parameters.AddWithValue("@HeureValidSys", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@NumEquipement", fp.NumEquipement);
                    //myCommand.Parameters.AddWithValue("@CodeEmplacement", fp.CodeEmplacement);

                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("updated");
        }



        [Route("UpdateDemTravailRecap")]
        [HttpPut]
        // GET: DemTravail/Edit/5
        public JsonResult UpdateDemTravailRecap(DemandeTravail fp)
        {
            string query = @"update DemTravail set 
            Contact=@Contact, Nature=@Nature, Consommable=@Consommable, Description=@Description,
            Status=@Status, TEffect=@TEffect, NatPanne=@NatPanne,ArretMachine=@ArretMachine, Reception=@Reception, 
            MotifValid=@MotifValid, HeureValid=@HeureValid, DateValid=@DateValid where NumDem=@NumDem";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {

                    myCommand.Parameters.AddWithValue("@Contact", fp.Contact);                   
                    myCommand.Parameters.AddWithValue("@Nature", fp.Nature);
                    myCommand.Parameters.AddWithValue("@Consommable", fp.Consommable);
                    myCommand.Parameters.AddWithValue("@Description", fp.Description);                   
                    myCommand.Parameters.AddWithValue("@Status", fp.Status);
                    myCommand.Parameters.AddWithValue("@TEffect", fp.TEffect);
                    myCommand.Parameters.AddWithValue("@NatPanne", fp.NatPanne);                   
                    myCommand.Parameters.AddWithValue("@ArretMachine", "N");
                    myCommand.Parameters.AddWithValue("@DateValid", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@HeureValid", DateTime.Now.Hour);
                    myCommand.Parameters.AddWithValue("@Reception", fp.Reception);
                    myCommand.Parameters.AddWithValue("@MotifValid", fp.MotifValid);

                    //myCommand.Parameters.AddWithValue("@DateDem", fp.DateDem);
                    //myCommand.Parameters.AddWithValue("@DateSouh", fp.DateSouh);
                    //myCommand.Parameters.AddWithValue("@DateDiag", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@DateDInter", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@DateDiagClo", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@DatePreClo", fp.DatePreClo);
                    //myCommand.Parameters.AddWithValue("@DateClo", fp.DateClo);
                    //myCommand.Parameters.AddWithValue("@DateFinT", fp.DateFinT);
                    //myCommand.Parameters.AddWithValue("@TypePanne", fp.TypePanne);
                    //myCommand.Parameters.AddWithValue("@UserId", fp.UserId);
                    //myCommand.Parameters.AddWithValue("@UserName", fp.UserName);
                    // myCommand.Parameters.AddWithValue("@Service", fp.Service);
                    // myCommand.Parameters.AddWithValue("@Departement", fp.Departement);
                    // myCommand.Parameters.AddWithValue("@HeureDem", fp.DateDem.Hour);
                    //myCommand.Parameters.AddWithValue("@CodeIntervention", fp.CodeIntervention);
                    // myCommand.Parameters.AddWithValue("@CodeEquipement", fp.CodeEquipement);
                    //myCommand.Parameters.AddWithValue("@Designation", fp.Designation);
                    //myCommand.Parameters.AddWithValue("@HeureFinT", fp.DateFinT.Hour);
                    //myCommand.Parameters.AddWithValue("@DateValidSys", DateTime.Now);
                    // myCommand.Parameters.AddWithValue("@HeureValidSys", DateTime.Now);
                    //myCommand.Parameters.AddWithValue("@NumEquipement", fp.NumEquipement);
                    //myCommand.Parameters.AddWithValue("@CodeEmplacement", fp.CodeEmplacement);

                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("updated");
        }

        



        // GET: DemandeTravails
        public async Task<IActionResult> Index()
        {
            return View(await _context.DemandeTravail.ToListAsync());
        }


        [Route("GetDemTravailByNumDem")]
        [HttpGet]
        // GET: DemandeTravails/Details/5
        public async Task<IActionResult> Details(double? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demandeTravail = await _context.DemandeTravail
                .FirstOrDefaultAsync(m => m.NumDem == id);
            if (demandeTravail == null)
            {
                return NotFound();
            }

            return View(demandeTravail);
        }

        // GET: DemandeTravails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DemandeTravails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numdem,UserId,UserName,Contact,Service,Departement,DateDem,HeureDem,Nature,CodeIntervention,Consommable,Description,CodeEquipement,DateSouh,CentreCout,Designation,Status,TEffect,NaturePanne,TypePanne,DateDiag,DateDInter,DateDiagClo,DatePreClo,DateClo,DateFinT,HeureFinT,DateValid,HeureValid,Reception,ArretMachine,MotifValid,NumEquipement,CodeEmplacement")] DemandeTravail demandeTravail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(demandeTravail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(demandeTravail);
        }










        [Route("UpdateDemTravail2")]
        [HttpPut]
        // GET: DemandeTravails/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDemTravail(int id, DemandeTravail demtravail)
        {
            if (id != demtravail.NumDem)
            {
                return BadRequest();
            }

            _context.Entry(demtravail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemandeTravailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: DemandeTravails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("UpdateDemTravail2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(double id, [Bind("Numdem,UserId,UserName,Contact,Service,Departement,DateDem,HeureDem,Nature,CodeIntervention,Consommable,Description,CodeEquipement,DateSouh,CentreCout,Designation,Status,TEffect,NaturePanne,TypePanne,DateDiag,DateDInter,DateDiagClo,DatePreClo,DateClo,DateFinT,HeureFinT,DateValid,HeureValid,Reception,ArretMachine,MotifValid,NumEquipement,CodeEmplacement")] DemandeTravail demandeTravail)
        {
            if (id != demandeTravail.NumDem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demandeTravail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemandeTravailExists(demandeTravail.NumDem))
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
            return View(demandeTravail);
        }

        // GET: DemandeTravails/Delete/5
        public async Task<IActionResult> Delete(double? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demandeTravail = await _context.DemandeTravail
                .FirstOrDefaultAsync(m => m.NumDem == id);
            if (demandeTravail == null)
            {
                return NotFound();
            }

            return View(demandeTravail);
        }

        // POST: DemandeTravails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(double id)
        {
            var demandeTravail = await _context.DemandeTravail.FindAsync(id);
            _context.DemandeTravail.Remove(demandeTravail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("GetDemandeTravailById")]
        [HttpGet]
        private bool DemandeTravailExists(double id)
        {
            return _context.DemandeTravail.Any(e => e.NumDem == id);
        }
    }
}
