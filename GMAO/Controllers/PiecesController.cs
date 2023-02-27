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
    public class PiecesController : Controller
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;
        
        public PiecesController(GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

      

        [HttpGet("PrixPiece/{CodePiece}")]
        public float PrixPiece(string CodePiece)
        {
            int prix;
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                SqlDataAdapter adp = new SqlDataAdapter("SELECT Pachat FROM Piece WHERE CodePiece ='" + CodePiece + "'", connection);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (adp != null)
                { prix = Convert.ToInt32(ds.Tables[0].Rows[0][0]); }
                else
                    prix = 0;
                connection.Close();
                return prix;
            }
        }



        [HttpGet("CheckStockBeforeInsert/{CodePiece}")]
        public int CheckStockBeforeInsert(string CodePiece)
        {
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();
                // Check stock quantity

                SqlDataAdapter adp = new SqlDataAdapter("SELECT QteStock FROM Piece WHERE CodePiece ='" + CodePiece + "'", connection);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                String t = ds.Tables[0].Rows[0][0].ToString();
                int qte = Convert.ToInt32(t);
                connection.Close();
                return qte;
            }
        }




        [Route("GetAllDesignCodeP")]
        [HttpGet]
        // GET: Pieces
        public JsonResult GetDesignationcodePiece()
        {
            string query = @"select CodePiece,Designation from Piece ORDER BY(CodePiece) asc";

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

        
       [Route("GetAllPiecesToList")]
       [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pieces = await _context.Piece.ToListAsync();
            return Ok(pieces);
        }
        //public List<Piece> Get() => _context.Piece.OrderBy(p => p.CodeCC).ToList();




        [Route("GetAllPieces")]
        [HttpGet]
        // GET: Pieces
        public JsonResult Getall()
        {
            string query = @"select * from Piece";

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




        /*[Route("GetPieceById")]
        [HttpGet("{id}")]
        // GET: Pieces/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piece = await _context.Piece
                .FirstOrDefaultAsync(m => m.CodePiece == id);
            if (piece == null)
            {
                return NotFound();
            }

            return View(piece);
        }*/

        [Route("AddPiece")]
        [HttpPost]
        // GET: Pieces/Create
        public JsonResult AddPiece(Piece fp)
        {
            string query = @"insert into Piece (CodePiece, Designation, AnCode, Unite, Analyse1, DateAchat , DateFabrication, Marque, 
                                                Analyse2, Machine, CentreCout,NumSerie, QteStock , StockAlerte , QteCmd , Pachat , CodeFournisseur , Analyse3)
                                    values (@CodePiece, @Designation, @AnCode, @Unite, @Analyse1, @DateAchat , @DateFabrication, @Marque, 
                                                @Analyse2, @Machine, @CentreCout,@NumSerie, @QteStock , @StockAlerte , @QteCmd , @Pachat , @CodeFournisseur , @Analyse3)";


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
                    myCommand.Parameters.AddWithValue("@AnCode", fp.AnCode);
                    myCommand.Parameters.AddWithValue("@Unite", fp.Unite);
                    myCommand.Parameters.AddWithValue("@Analyse1", fp.Analyse1);
                    myCommand.Parameters.AddWithValue("@DateAchat", fp.DateAchat);
                    myCommand.Parameters.AddWithValue("@DateFabrication", fp.DateFabrication);
                    myCommand.Parameters.AddWithValue("@Marque", fp.Marque);
                    myCommand.Parameters.AddWithValue("@Analyse2", fp.Analyse2);
                    myCommand.Parameters.AddWithValue("@Machine", fp.Machine);
                    myCommand.Parameters.AddWithValue("@CentreCout", fp.CentreCout);
                    myCommand.Parameters.AddWithValue("@NumSerie", fp.NumSerie);
                    myCommand.Parameters.AddWithValue("@QteStock", fp.QteStock);
                    myCommand.Parameters.AddWithValue("@StockAlerte", fp.StockAlerte);
                    myCommand.Parameters.AddWithValue("@QteCmd", fp.QteCmd);
                     myCommand.Parameters.AddWithValue("@Pachat", fp.Pachat);
                    myCommand.Parameters.AddWithValue("@CodeFournisseur", fp.CodeFournisseur);
                    myCommand.Parameters.AddWithValue("@Analyse3", fp.Analyse3);

                    //myCommand.Parameters.AddWithValue("@StockMin", fp.StockMin);
                    //myCommand.Parameters.AddWithValue("@StockMax", fp.StockMax);
                    //myCommand.Parameters.AddWithValue("@CodeCC", fp.CodeCC);
                    //myCommand.Parameters.AddWithValue("@PRevient", fp.Previent);
                    //myCommand.Parameters.AddWithValue("@RefFournisseur", fp.RefFournisseur);
                    //myCommand.Parameters.AddWithValue("@PAchatTTC", fp.PAchatTTC);
                    //myCommand.Parameters.AddWithValue("@ZStockage", fp.ZStockage);
                    //myCommand.Parameters.AddWithValue("@If_CC", fp.If_CC);
                    //myCommand.Parameters.AddWithValue("@AnneeConstruction", fp.AnneeConstruction);
                    //myCommand.Parameters.AddWithValue("@DateMES", "");
                    //myCommand.Parameters.AddWithValue("@Etalonnage", fp.Etalonnage);
                    //myCommand.Parameters.AddWithValue("@Image", fp.Image);
                    //myCommand.Parameters.AddWithValue("@FicheT", fp.FicheT);
                    //myCommand.Parameters.AddWithValue("@Test_CCout", fp.Test_CCout);
                    //myCommand.Parameters.AddWithValue("@Obsolete", fp.Obsolete);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("add succ");
        }


        [Route("UpdatePiece")]
        [HttpPut]
        // GET: Pieces/Edit/5
        public JsonResult UpdatePiece(Piece fp)
        {
            string query = @"Update Piece set 
                            (@Designation=Designation, @AnCode=AnCode, @Unite=Unite, @Analyse1=Analyse1 , @DateAchat=DateAchat , 
                            @DateFabrication=DateFabrication, @Marque=Marque, @Analyse2=Analyse2, @Machine=Machine,
                            @CentreCout=CentreCout, @NumSerie=NumSerie, @QteStock=QteStock , @StockAlerte=StockAlerte , @QteCm=QteCmd , 
                            @Pachat=Pachat, @CodeFournisseur=CodeFournisseur , @Analyse3=Analyse3 where CodePiece= @CodePiece)";

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
                    myCommand.Parameters.AddWithValue("@AnCode", fp.AnCode);
                    myCommand.Parameters.AddWithValue("@Unite", fp.Unite);
                    myCommand.Parameters.AddWithValue("@Analyse1", fp.Analyse1);
                    myCommand.Parameters.AddWithValue("@DateAchat", fp.DateAchat);
                    myCommand.Parameters.AddWithValue("@DateFabrication", fp.DateFabrication);
                    myCommand.Parameters.AddWithValue("@Marque", fp.Marque);
                    myCommand.Parameters.AddWithValue("@Analyse2", fp.Analyse2);
                    myCommand.Parameters.AddWithValue("@Machine", fp.Machine);
                    myCommand.Parameters.AddWithValue("@CentreCout", fp.CentreCout);
                    myCommand.Parameters.AddWithValue("@NumSerie", fp.NumSerie);
                    myCommand.Parameters.AddWithValue("@QteStock", fp.QteStock);
                    myCommand.Parameters.AddWithValue("@StockAlerte", fp.StockAlerte);
                    myCommand.Parameters.AddWithValue("@QteCmd", fp.QteCmd);
                    myCommand.Parameters.AddWithValue("@Pachat", fp.Pachat);
                    myCommand.Parameters.AddWithValue("@CodeFournisseur", fp.CodeFournisseur);
                    myCommand.Parameters.AddWithValue("@Analyse3", fp.Analyse3);

                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("updated");
        }





        [Route("DeletePiece")]
        [HttpDelete("{CodePiece}")]
        // GET: Pieces/Delete/5
        public JsonResult DeletePiece(string CodePiece)
        {
            string query = @"delete from Piece where CodePiece=@CodePiece";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@CodePiece",CodePiece);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("Deleted");
        }

        
        //[Route("DeletePieceById")]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteById(string id)
        //{
        //    try
        //    {
        //        var customer = await _context.Piece.FindAsync(id);

        //        _context.Piece.Remove(customer);
        //        await _context.SaveChangesAsync();
        //        //await mainHub.UpdateClients();
        //        return Ok("Successfully deleted");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        private bool PieceExists(string id)
        {
            return _context.Piece.Any(e => e.CodePiece == id);
        }
    }
}
