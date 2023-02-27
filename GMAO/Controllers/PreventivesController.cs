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
    public class PreventivesController : Controller
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;
        public PreventivesController(GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }






        [Route("GetAllPreventives")]
        [HttpGet]
        // GET: Preventives
        public JsonResult GetAllPreventives()
        {
            string query = @"select * from Preventive";

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




        [Route("GetPreventiveById")]
        [HttpGet("{CodePrev}")]
        // GET: Pieces/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var prev = await _context.Preventive
                .FirstOrDefaultAsync(m => m.CodePrev == id);
            if (prev == null)
            {
                return NotFound();
            }

            return View(prev);
        }

        [Route("AddPreventive")]
        [HttpPost]
        // GET: Preventive/Create
        public JsonResult AddPreventive(Preventive fp)
        {

            string query = @"insert into Preventive (CodePrev, Description, TypeIntervention)
                                    values (@CodePrev,@Description, @TypeIntervention)";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {

                    //myCommand.Parameters.AddWithValue("@CodePrev", fp.CodePrev);
                    myCommand.Parameters.AddWithValue("@Description", fp.Description);
                    myCommand.Parameters.AddWithValue("@TypeIntervention", fp.TypeIntervention);
                  
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("add succ");
        }


        [Route("UpdatePreventive")]
        [HttpPut]
        // GET: Intervention/Edit/5
        public JsonResult UpdatePreventive(Preventive fp)
        {
            string query = @"Update Piece set 
                            (Description=@Description, TypeIntervention=@TypeIntervention where CodePrev= @CodePrev)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {

                    myCommand.Parameters.AddWithValue("@CodePrev", fp.CodePrev);
                    myCommand.Parameters.AddWithValue("@Description", fp.Description);
                    myCommand.Parameters.AddWithValue("@TypeIntervention", fp.TypeIntervention);
                   
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("updated");
        }





        [Route("DeletePreventive")]
        [HttpDelete("{CodePrev}")]
        // GET: Preventive/Delete/5
        public JsonResult DeletePreventive(double CodePrev)
        {
            string query = @"delete from Preventive where CodePrev=@CodePrev";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@CodePrev", CodePrev);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("Deleted");
        }

































    }
}
