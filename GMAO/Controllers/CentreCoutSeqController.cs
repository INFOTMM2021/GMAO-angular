using GMAO.Data;
using GMAO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentreCoutSeqController : Controller
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;
        public CentreCoutSeqController(GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [Route("AddCentreCout")]
        [HttpPost]
        // GET: AddCentreCout
        public JsonResult AddCentreCout(CentreCoutSeq cc)
        {
            string query = @"insert into CentreCoutSeq (CodeCC, Designation, CodeAnt, Seq) values (@CodeCC, @Designation, @CodeAnt, @Seq)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@CodeCC", cc.CodeCC);
                    myCommand.Parameters.AddWithValue("@Designation", cc.Designation);
                    myCommand.Parameters.AddWithValue("@CodeAnt", cc.CodeAnt);
                    myCommand.Parameters.AddWithValue("@Seq", cc.Seq);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("add succ");
        }






    }
}
