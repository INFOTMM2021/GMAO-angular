using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GMAO.Data;
using GMAO.Models;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Threading;

namespace GMAO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandePiecesController : Controller
    {
        private int maxBon=1;
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;
        public DemandePiecesController(GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Route("GetAllPiecesByNumDem")]
        [HttpGet("{NumDem}")]
        // GET: Intervention
        public IActionResult GetAllPiecesByNumDem([FromQuery] int NumDem)
        {
            string query = @"select * from DemPiece where NumDem=@NumDem";
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


        [Route("AddDemPiece")]
        [HttpPost]
        // Post: DemandePiece/Create
        public JsonResult Add(DemandePiece fp)
        {
            string query = @"insert into DemPiece (CodePiece, Designation, NumDem, QteDem, QteLiv, Type,DateSaisie,NumBon,HeureSaisie)
                                    values (@CodePiece, @Designation,@NumDem, @QteDem, @QteLiv, @Type,@DateSaisie,@NumBon,@HeureSaisie)";

            maxBon = Convert.ToInt32(this.MaxNumBon().ToString());
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@CodePiece", fp.CodePiece);
                    myCommand.Parameters.AddWithValue("@Designation", fp.Designation);
                    myCommand.Parameters.AddWithValue("@NumDem", fp.NumDem);
                    myCommand.Parameters.AddWithValue("@QteDem", fp.QteDem);
                    myCommand.Parameters.AddWithValue("@QteLiv", fp.QteDem);
                    myCommand.Parameters.AddWithValue("@Type", "P");
                    myCommand.Parameters.AddWithValue("@DateSaisie", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@NumBon", this.maxBon);
                    myCommand.Parameters.AddWithValue("@HeureSaisie",DateTime.Now);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("add succ");
        }





        [Route("MAJPiece")]
        [HttpPut]
        public IActionResult UpdatePieceCount(  int newPieceCount,string codePiece)
        {
            var piece = _context.Piece.FirstOrDefault(p => p.CodePiece == codePiece);
            if (piece == null)
            {
                return NotFound();
            }
            if (piece.QteStock > newPieceCount)
                piece.QteStock = piece.QteStock - newPieceCount;
            else
                Console.WriteLine("Le nombre des pieces disponible est insuffisant");
            _context.SaveChanges();
            return Ok();
        }

        /*[Route("MAJPiece")]
        [HttpPut]
        public void MAJPiece([FromQuery] int qtePiece, [FromQuery] string cp)
        {
            string query = "update Piece set QteStock=QteStock-@qtePiece where CodePiece=@cp";
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
        }*/


        [Route("GetMaxNumBon")]
        [HttpGet]
        public int MaxNumBon()
        {
            
                string query = @"select MAX (NumBon +1) as maxnumbon from DemPiece";
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





        // GET: DemandePieces
        public async Task<IActionResult> Index()
        {
            return View(await _context.DemandePiece.ToListAsync());
        }

        // GET: DemandePieces/Details/5
        public async Task<IActionResult> Details(double? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demandePiece = await _context.DemandePiece
                .FirstOrDefaultAsync(m => m.NumDem == id);
            if (demandePiece == null)
            {
                return NotFound();
            }

            return View(demandePiece);
        }




        // POST: DemandePieces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(double id, [Bind("NumOrdre,NumDemande,CodePiece,Designation,QteDem,QteLiv,NumBon,Type,DateSaisie,HeureSaisie")] DemandePiece demandePiece)
        {
            if (id != demandePiece.NumDem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demandePiece);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemandePieceExists(demandePiece.NumDem))
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
            return View(demandePiece);
        }

        // GET: DemandePieces/Delete/5
        public async Task<IActionResult> Delete(double? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demandePiece = await _context.DemandePiece
                .FirstOrDefaultAsync(m => m.NumDem == id);
            if (demandePiece == null)
            {
                return NotFound();
            }

            return View(demandePiece);
        }

        // POST: DemandePieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(double id)
        {
            var demandePiece = await _context.DemandePiece.FindAsync(id);
            _context.DemandePiece.Remove(demandePiece);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DemandePieceExists(double id)
        {
            return _context.DemandePiece.Any(e => e.NumDem == id);
        }
    }
}
