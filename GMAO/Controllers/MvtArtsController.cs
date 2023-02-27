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
    public class MvtArtsController : Controller
    {
         
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;
        public MvtArtsController(GMAOContext context , IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Route("AddMvtArtP")]
        [HttpPost]        
        public JsonResult AddMvtArts(MvtArt fp)
        {
            string query = @"insert into MvtArt (NumMvt, Type, CodePiece, QteMvt, PrixRevient, QteAvMvt,PMPAvMvt)
                                    values (@NumMvt, @Type,@CodePiece, @QteMvt, @PrixRevient, @QteAvMvt,@PMPAvMvt)";

            
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@NumMvt", fp.NumMvt);
                    myCommand.Parameters.AddWithValue("@Type", "P");
                    myCommand.Parameters.AddWithValue("@CodePiece", fp.CodePiece);
                    myCommand.Parameters.AddWithValue("@QteMvt", fp.QteMvt);
                    myCommand.Parameters.AddWithValue("@PrixRevient", fp.PrixRevient);
                    myCommand.Parameters.AddWithValue("@QteAvMvt", fp.QteAvMvt);
                    myCommand.Parameters.AddWithValue("@PMPAvMvt",fp.PMPAvMvt);

                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("add succ");
        }



        [Route("GetAllMvtArts")]
        [HttpGet]
        public List<MvtArt> Get() => _context.MvtArt.OrderBy(p => p.NumMvt).ToList();




        [Route("DeleteMvtArt")]
        [HttpDelete("{NumMvt}")]
        
        public JsonResult DeleteMvtArt(string NumMvt)
        {
            string query = @"delete from MvtArt where NumMvt=@NumMvt";
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



        







        // GET: MvtArts
        public async Task<IActionResult> Index()
        {
            return View(await _context.MvtArt.ToListAsync());
        }

        // GET: MvtArts/Details/5
        public async Task<IActionResult> Details(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mvtArt = await _context.MvtArt
                .FirstOrDefaultAsync(m => m.NumMvt == id);
            if (mvtArt == null)
            {
                return NotFound();
            }

            return View(mvtArt);
        }

        // GET: MvtArts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MvtArts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumMvt,Type,CodePiece,QteMvt,PrixRevient,QteAvMvt,PMPAvMvt,tint")] MvtArt mvtArt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mvtArt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mvtArt);
        }

        // GET: MvtArts/Edit/5
        public async Task<IActionResult> Edit(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mvtArt = await _context.MvtArt.FindAsync(id);
            if (mvtArt == null)
            {
                return NotFound();
            }
            return View(mvtArt);
        }

        // POST: MvtArts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(float id, [Bind("NumMvt,Type,CodePiece,QteMvt,PrixRevient,QteAvMvt,PMPAvMvt,tint")] MvtArt mvtArt)
        {
            if (id != mvtArt.NumMvt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mvtArt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MvtArtExists(mvtArt.NumMvt))
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
            return View(mvtArt);
        }

        // GET: MvtArts/Delete/5
        public async Task<IActionResult> Delete(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mvtArt = await _context.MvtArt
                .FirstOrDefaultAsync(m => m.NumMvt == id);
            if (mvtArt == null)
            {
                return NotFound();
            }

            return View(mvtArt);
        }

        // POST: MvtArts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(float id)
        {
            var mvtArt = await _context.MvtArt.FindAsync(id);
            _context.MvtArt.Remove(mvtArt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MvtArtExists(float id)
        {
            return _context.MvtArt.Any(e => e.NumMvt == id);
        }
    }
}
