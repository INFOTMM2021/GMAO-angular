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
    public class EquipementCompteursController : Controller
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;

        public EquipementCompteursController(GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: EquipementCompteurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.EquipementCompteur.ToListAsync());
        }

        [Route("GetAllEqCompteurs")]
        [HttpGet]
        // GET: EquipementCompteurs
        public JsonResult Get()
        {
            string query = @"select * from EquipementCompteur";

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




        [Route("GetEqCompteursById")]
        [HttpGet]
        // GET: EquipementCompteurs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ECompteurs = await _context.CompteurEquipement
                .FirstOrDefaultAsync(m => m.CodeCompteur == id);
            if (ECompteurs == null)
            {
                return NotFound();
            }

            return View(ECompteurs);
        }


        [Route("AddEqCompteur")]
        [HttpPost]
        // GET: EqCompteur/Create
        public JsonResult AddEqCompteur(EquipementCompteur eq)
        {

            string query = @"insert into EquipementCompteur (CodeEquipement, NumSerie, CodeTMM, CodeCompteur, Dernieremodif, Valeur)
                                    values (@CodePiece, @Designation, @Unite, @NumSerie, @Pachat, @CodeFournisseur)";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {

                    myCommand.Parameters.AddWithValue("@CodeEquipement", eq.CodeEquipement);
                    myCommand.Parameters.AddWithValue("@NumSerie", eq.NumSerie);
                    myCommand.Parameters.AddWithValue("@CodeTMM", eq.CodeTMM);
                    myCommand.Parameters.AddWithValue("@CodeCompteur", eq.CodeCompteur);
                    myCommand.Parameters.AddWithValue("@Dernieremodif", eq.Dernieremodif);
                    myCommand.Parameters.AddWithValue("@Valeur", eq.Valeur);

                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("add succ");
        }



        [Route("UpdateEqCompteur")]
        [HttpPut]
        // GET: EquipementCompteur/Edit/5
        public JsonResult UpdateEqCompteur(EquipementCompteur eq)
        {
            string query = @"Update EquipementCompteur set 
                            (CodeEquipement=@CodeEquipement, NumSerie=@NumSerie,  CodeTMM=@CodeTMM, Dernieremodif=@Dernieremodif, Valeur=@Valeur
                            where CodeCompteur= @CodeCompteur ) ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {

                    myCommand.Parameters.AddWithValue("@CodeEquipement", eq.CodeEquipement);
                    myCommand.Parameters.AddWithValue("@NumSerie", eq.NumSerie);
                    myCommand.Parameters.AddWithValue("@CodeTMM", eq.CodeTMM);
                    myCommand.Parameters.AddWithValue("@CodeCompteur", eq.CodeCompteur);
                    myCommand.Parameters.AddWithValue("@Dernieremodif", eq.Dernieremodif);
                    myCommand.Parameters.AddWithValue("@Valeur", eq.Valeur);


                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("updated");
        }



        [Route("DeleteEqCompteur")]
        [HttpGet("{CodeCompteur}")]
        // GET: EquipementCompteur/Delete/5
        public JsonResult DeleteEqCompteur(string CodeCompteur)
        {
            string query = @"delete from EquipementCompteur where CodeCompteur=@CodeCompteur";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@CodeCompteur", CodeCompteur);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("Deleted");
        }

        
        [Route("DeleteEqCompteurById")]
        [HttpDelete("{CodeCompteur}")]
        public async Task<IActionResult> DeleteById(string CodeCompteur)
        {
            try
            {
                var customer = await _context.EquipementCompteur.FindAsync(CodeCompteur);
                _context.EquipementCompteur.Remove(customer);
                await _context.SaveChangesAsync();
                //await mainHub.UpdateClients();
                return Ok("Successfully deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [Route("existEqCompteur")]
        [HttpGet("{CodeCompteur}")]
        private bool EquipementCompteurExists(string CodeCompteur)
        {
            return _context.EquipementCompteur.Any(e => e.CodeCompteur == CodeCompteur);
        }
    }
}
