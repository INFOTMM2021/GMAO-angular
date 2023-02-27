using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GMAO.Models;
using GMAO.Data;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace GMAO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FournisseurPiecesController : Controller
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;


        public FournisseurPiecesController(GMAOContext context , IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }




        [HttpGet]
        [Route("GetAllFournisseurs")]
        // GET: FournisseurPieces
        public JsonResult Get()
        {
            //string query = @"select * from FournisseurPiéce";
            string query = @"select No_,Name,Address,City from [tmm-dbs].TMMLIVE.dbo.[TMMLIVE$Vendor] where [Vendor Posting Group]='EPR'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query,myConn))
                {
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult(table);
        }



        [Route("GetFournisseurById")]
        [HttpGet]
        // GET: FournisseurPieces/GetFournisseurById/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fournisseurPiece = await _context.FournisseurPiéce
                .FirstOrDefaultAsync(m => m.CodeFournisseur == id);
            if (fournisseurPiece == null)
            {
                return NotFound();
            }

            return View(fournisseurPiece);
        }

        [Route("AddFournisseur")]
        [HttpPost]
        // GET: FournisseurPieces/AddFournisseur
        public JsonResult AddFournisseur( FournisseurPiéce fp)
        {
            string query = @"insert into FournisseurPiéce values
                        (@CodePiéce, @CodeFournisseur, @PAchat, @RefFrs, @Previent, @Devise) 
                ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@CodePiéce", fp.CodePiéce);
                    myCommand.Parameters.AddWithValue("@CodeFournisseur", fp.CodeFournisseur);
                    myCommand.Parameters.AddWithValue("@PAchat", fp.PAchat);
                    myCommand.Parameters.AddWithValue("@RefFrs", fp.RefFrs);
                    myCommand.Parameters.AddWithValue("@Previent", fp.Previent);
                    myCommand.Parameters.AddWithValue("@Devise", fp.Devise);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult(table);
        }


        [Route("UpdateFournisseur")]
        [HttpPut]
        // GET: FournisseurPieces/UpdateFournisseur
        public JsonResult UpdateFournisseur(FournisseurPiéce fp)
        {
             string query = @"update FournisseurPiéce set
                                CodePiéce = @CodePiéce, PAchat= @PAchat, RefFrs=@RefFrs, Previent=@Previent, Devise=@Devise
                                where CodeFournisseur=@CodeFournisseur ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@CodePiéce", fp.CodePiéce);
                    myCommand.Parameters.AddWithValue("@CodeFournisseur", fp.CodeFournisseur);
                    myCommand.Parameters.AddWithValue("@PAchat", fp.PAchat);
                    myCommand.Parameters.AddWithValue("@RefFrs", fp.RefFrs);
                    myCommand.Parameters.AddWithValue("@Previent", fp.Previent);
                    myCommand.Parameters.AddWithValue("@Devise", fp.Devise);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult(table);
        }



        [Route("DeleteFournisseur")]
        [HttpDelete("{id}")]
        // GET: FournisseurPieces/DeleteFournisseur
        public JsonResult DeleteFournisseur(string CFournisseur)
        {

            //string query = @"delete from FournisseurPiéce where CodeFournisseur='" + CFournisseur + "'";
            string query = @"delete from FournisseurPiéce where CodeFournisseur=@CodeFournisseur";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@CodeFournisseur", CFournisseur);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("Deleted with success");
        }















        
        private bool FournisseurPieceExists(string id)
        {
            return _context.FournisseurPiéce.Any(e => e.CodeFournisseur == id);
        }
    }
}
