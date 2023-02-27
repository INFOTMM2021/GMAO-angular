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
using System.Net.Http;

namespace GMAO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MouvementDetailsController : Controller
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public MouvementDetailsController(GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }



        [Route("AddmouvementDetail")]
        [HttpPost]
        public JsonResult AddMouvementDetail(MouvementDetail fp)
        {
            string query = @"insert into MouvementDetail (NumMvt, Type, CodePiece, NumSerie, QteDem, QteMvt,PAchat,PRevient,Devise,RefFournisseur)
                                    values (@NumMvt, @Type, @CodePiece, @NumSerie, @QteDem, @QteMvt,@PAchat,@PRevient,@Devise,@RefFournisseur)";


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
                    myCommand.Parameters.AddWithValue("@CodePiece", fp.CodePiece);
                    myCommand.Parameters.AddWithValue("@NumSerie", fp.NumSerie);
                    myCommand.Parameters.AddWithValue("@QteDem", fp.QteDem);
                    myCommand.Parameters.AddWithValue("@QteMvt", fp.QteMvt);
                    myCommand.Parameters.AddWithValue("@PAchat", fp.PAchat);
                    myCommand.Parameters.AddWithValue("@PRevient", fp.PRevient);
                    myCommand.Parameters.AddWithValue("@Devise", fp.Devise);
                    myCommand.Parameters.AddWithValue("@RefFournisseur", fp.RefFournisseur);

                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("add succ");
        }


        //[HttpGet("PrixPiece/{CodePiece}")]
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



        //[HttpGet("CheckStockBeforeInsert/{CodePiece}")]
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


        [Route("AddmouvementDetailRegul")]
        [HttpPost]
        public IActionResult AddmouvementDetailRegul(MouvementDetail fp)
        {
            int nbrp;
            float prixp;
            string query = @"INSERT INTO MouvementDetail (NumMvt, Type, CodePiece, NumSerie, QteDem, QteMvt, PAchat, PRevient,RefFournisseur)
                     VALUES (@NumMvt, @Type, @CodePiece, @NumSerie, @QteDem, @QteMvt, @PAchat, @PRevient,@RefFournisseur)";

            string query2 = @"UPDATE Piece
                      SET QteStock = QteStock + @QteDem
                      WHERE CodePiece = @CodePiece AND QteStock >= @QteDem";

            string query3 = @"insert into MvtArt (NumMvt, Type, CodePiece, QteMvt, QteAvMvt,PMPAvMvt)
                                    values (@NumMvt, @Type,@CodePiece, @QteMvt, @QteAvMvt,@PMPAvMvt)";

            string query4 = @"insert into Mouvement (NumMvt, Type, Tier, DateMvt, Ref,CentreCout,UserName,DateSaisie)
                                    values (@NumMvt, @Type, @Tier, @DateMvt, @Ref,@CentreCout,@UserName,@DateSaisie)";

            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Check stock quantity
                    SqlCommand checkCommand = new SqlCommand("SELECT QteStock FROM Piece WHERE CodePiece = @CodePiece", connection, transaction);
                    checkCommand.Parameters.AddWithValue("@CodePiece", fp.CodePiece);
                    int qteStock = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (qteStock < Math.Abs(fp.QteDem))
                    {
                        return BadRequest("La quantité en stock est inférieure à la quantité demandée.");
                    }


                     nbrp = CheckStockBeforeInsert(fp.CodePiece);
                     prixp = PrixPiece(fp.CodePiece);
                    Console.WriteLine(prixp.ToString(), nbrp.ToString());

                    // Insert movement detail
                    SqlCommand insertCommand = new SqlCommand(query, connection, transaction);
                    insertCommand.Parameters.AddWithValue("@NumMvt", fp.NumMvt);
                    insertCommand.Parameters.AddWithValue("@Type", "RE");
                    insertCommand.Parameters.AddWithValue("@CodePiece", fp.CodePiece);
                    insertCommand.Parameters.AddWithValue("@NumSerie", null);
                    insertCommand.Parameters.AddWithValue("@QteDem", fp.QteDem);
                    insertCommand.Parameters.AddWithValue("@QteMvt", fp.QteMvt);
                    insertCommand.Parameters.AddWithValue("@PAchat", prixp);
                    insertCommand.Parameters.AddWithValue("@PRevient", fp.PRevient); 
                    insertCommand.Parameters.AddWithValue("@RefFournisseur", fp.RefFournisseur);
                    insertCommand.ExecuteNonQuery();

                    //add MvtArt 
                    SqlCommand insertCommandMvtArt = new SqlCommand(query3, connection, transaction);
                    insertCommandMvtArt.Parameters.AddWithValue("@NumMvt", fp.NumMvt);
                    insertCommandMvtArt.Parameters.AddWithValue("@Type", "P");
                    insertCommandMvtArt.Parameters.AddWithValue("@CodePiece", fp.CodePiece);
                    insertCommandMvtArt.Parameters.AddWithValue("@QteMvt", fp.QteDem);
                    insertCommandMvtArt.Parameters.AddWithValue("@QteAvMvt", nbrp); 
                    insertCommandMvtArt.Parameters.AddWithValue("@PMPAvMvt", prixp);
                    insertCommandMvtArt.ExecuteNonQuery();


                    //add Mouvement 
                    SqlCommand insertCommandMouvement = new SqlCommand(query4, connection, transaction);
                    insertCommandMouvement.Parameters.AddWithValue("@NumMvt", fp.NumMvt);
                    insertCommandMouvement.Parameters.AddWithValue("@Type", "RE");
                    insertCommandMouvement.Parameters.AddWithValue("@Tier", null);
                    insertCommandMouvement.Parameters.AddWithValue("@DateMvt", DateTime.Now);
                    insertCommandMouvement.Parameters.AddWithValue("@Ref", fp.RefFournisseur);
                    insertCommandMouvement.Parameters.AddWithValue("@CentreCout", null);
                    insertCommandMouvement.Parameters.AddWithValue("@UserName", "testUser");
                    insertCommandMouvement.Parameters.AddWithValue("@DateSaisie", DateTime.Now);
                    insertCommandMouvement.ExecuteNonQuery();

                    // Update stock quantity
                    SqlCommand updateCommand = new SqlCommand(query2, connection, transaction);
                    updateCommand.Parameters.AddWithValue("@CodePiece", fp.CodePiece);
                    updateCommand.Parameters.AddWithValue("@QteDem", fp.QteDem);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        // Rollback transaction and return error message
                        transaction.Rollback();
                        return BadRequest("La quantité en stock est inférieure à la quantité demandée.");
                    }
                    



                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest("Failed to add mouvement detail. Error: " + ex.Message);
                }

                connection.Close();
            }

            return Ok("Mouvement detail added successfully.");
        }





        //[Route("AddmouvementDetailRegul")]
        //[HttpPost]
        //public IActionResult AddmouvementDetailRegul(MouvementDetail fp)
        //{
        //    string query = @"INSERT INTO MouvementDetail (NumMvt, Type, CodePiece, NumSerie, QteDem, QteMvt, PAchat, PRevient)
        //             VALUES (@NumMvt, @Type, @CodePiece, @NumSerie, @QteDem, @QteMvt, @PAchat, @PRevient)";

        //    string query2 = @"UPDATE Piece
        //              SET QteStock = QteStock + @QteDem
        //              WHERE CodePiece = @CodePiece AND QteStock >= @QteDem";

        //    string sqlDataSource = _configuration.GetConnectionString("GMAOContext");

        //    using (SqlConnection connection = new SqlConnection(sqlDataSource))
        //    {
        //        connection.Open();

        //        // Check stock quantity
        //        SqlCommand checkCommand = new SqlCommand("SELECT QteStock FROM Piece WHERE CodePiece = @CodePiece", connection);
        //        checkCommand.Parameters.AddWithValue("@CodePiece", fp.CodePiece);
        //        int qteStock = Convert.ToInt32(checkCommand.ExecuteScalar());

        //        if (qteStock < Math.Abs(fp.QteDem))
        //        {
        //            return BadRequest("La quantité en stock est inférieure à la quantité demandée.");
        //        }

        //        // Insert movement detail
        //        SqlCommand insertCommand = new SqlCommand(query, connection);
        //        insertCommand.Parameters.AddWithValue("@NumMvt", fp.NumMvt);
        //        insertCommand.Parameters.AddWithValue("@Type", "RE");
        //        insertCommand.Parameters.AddWithValue("@CodePiece", fp.CodePiece);
        //        insertCommand.Parameters.AddWithValue("@NumSerie", "");
        //        insertCommand.Parameters.AddWithValue("@QteDem", fp.QteDem);
        //        insertCommand.Parameters.AddWithValue("@QteMvt", fp.QteMvt);
        //        insertCommand.Parameters.AddWithValue("@PAchat", fp.PAchat);
        //        insertCommand.Parameters.AddWithValue("@PRevient", fp.PRevient);

        //        insertCommand.ExecuteNonQuery();

        //        // Update stock quantity
        //        SqlCommand updateCommand = new SqlCommand(query2, connection);
        //        updateCommand.Parameters.AddWithValue("@CodePiece", fp.CodePiece);
        //        updateCommand.Parameters.AddWithValue("@QteDem", fp.QteDem);

        //        int rowsAffected = updateCommand.ExecuteNonQuery();
        //        if (rowsAffected == 0)
        //        {
        //            // Rollback transaction and return error message
        //            SqlTransaction transaction = connection.BeginTransaction();
        //            insertCommand.Transaction = transaction;
        //            updateCommand.Transaction = transaction;
        //        transaction.Rollback();
        //        return BadRequest("La quantité en stock est inférieure à la quantité demandée.");
        //    }
        //    //string query3 = @"UPDATE Parametres SET Valeur = Valeur + 1 WHERE Cle = 'Prelev'";
        //    //SqlCommand updateParametreCommand = new SqlCommand(query3, connection);
        //    //int rowsUpdated = updateParametreCommand.ExecuteNonQuery();
        //    //if (rowsUpdated == 0)
        //    //{
        //    //    // Rollback transaction and return error message
        //    //    SqlTransaction transaction = connection.BeginTransaction();
        //    //    insertCommand.Transaction = transaction;
        //    //    updateCommand.Transaction = transaction;
        //    //    transaction.Rollback();
        //    //    //return BadRequest("Failed to update parametres table.");
        //    //}

        //    connection.Close();
        //    }

        //    return Ok("Mouvement detail added successfully.");
        //}


        



        [Route("GetAllMouvementDetail")]
        [HttpGet]
        // GET: MouvementsDetails
        public JsonResult GetAllMouvementDetail()
        {
            string query = @"select TOP 1000 * from MouvementDetail order by NumMvt desc";

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




        //public List<MouvementDetail> Get() => _context.MouvementDetail.OrderBy(p => p.NumMvt).ToList();




        [Route("DeleteMouvemenDetail")]
        [HttpDelete("{NumMvt}")]

        public JsonResult DeleteMouvemenDetail(string NumMvt)
        {
            string query = @"delete from MouvemenDetail where NumMvt=@NumMvt";
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


















        // GET: MouvementDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.MouvementDetail.ToListAsync());
        }

        // GET: MouvementDetails/Details/5
        public async Task<IActionResult> Details(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mouvementDetail = await _context.MouvementDetail
                .FirstOrDefaultAsync(m => m.NumMvt == id);
            if (mouvementDetail == null)
            {
                return NotFound();
            }

            return View(mouvementDetail);
        }

        // GET: MouvementDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MouvementDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumMvt,Type,CodePiece,NumSerie,QteDem,PAchat,Previent,Devise,RefFournisseur,tint")] MouvementDetail mouvementDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mouvementDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mouvementDetail);
        }

        // GET: MouvementDetails/Edit/5
        public async Task<IActionResult> Edit(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mouvementDetail = await _context.MouvementDetail.FindAsync(id);
            if (mouvementDetail == null)
            {
                return NotFound();
            }
            return View(mouvementDetail);
        }

        // POST: MouvementDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(float id, [Bind("NumMvt,Type,CodePiece,NumSerie,QteDem,PAchat,Previent,Devise,RefFournisseur,tint")] MouvementDetail mouvementDetail)
        {
            if (id != mouvementDetail.NumMvt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mouvementDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MouvementDetailExists(mouvementDetail.NumMvt))
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
            return View(mouvementDetail);
        }

        // GET: MouvementDetails/Delete/5
        public async Task<IActionResult> Delete(float? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mouvementDetail = await _context.MouvementDetail
                .FirstOrDefaultAsync(m => m.NumMvt == id);
            if (mouvementDetail == null)
            {
                return NotFound();
            }

            return View(mouvementDetail);
        }

        // POST: MouvementDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(float id)
        {
            var mouvementDetail = await _context.MouvementDetail.FindAsync(id);
            _context.MouvementDetail.Remove(mouvementDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MouvementDetailExists(float id)
        {
            return _context.MouvementDetail.Any(e => e.NumMvt == id);
        }
    }
}
