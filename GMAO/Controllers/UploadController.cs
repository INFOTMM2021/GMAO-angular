using GMAO.Data;
using GMAO.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GMAO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;


        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public UploadController (IHostingEnvironment environment, GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _hostingEnvironment = environment;
        }

        //[HttpPost, DisableRequestSizeLimit]

        //public IActionResult Upload()
        //{
        //    try
        //    {
        //        var files = Request.Form.Files;
        //        var folderName = Path.Combine("Resources", "Files");
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //        if (files.Any(f => f.Length == 0))
        //        {
        //            return BadRequest();
        //        }
        //        foreach (var file in files)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName); //you can add this path to a list and then return all dbPaths to the client if require
        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //        }
        //        return Ok("All the files are successfully uploaded.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }
        //}




        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        [Obsolete]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "Resources");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (file.Length > 0)
            {
                var filePath = Path.Combine(uploads, file.FileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }

            
            return Ok();
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("download")]
        [Obsolete]
        public async Task<IActionResult> Download([FromQuery] string file)
        {
            var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "Resources");
            var filePath = Path.Combine(uploads, file);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), file);
        }

        [HttpGet]
        [Route("GetAllFiles")]
        [Obsolete]
        public IActionResult Files()
        {
            var result = new List<string>();

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Resources");
            if (Directory.Exists(uploads))
            {
                var provider = _hostingEnvironment.ContentRootFileProvider;
                foreach (string fileName in Directory.GetFiles(uploads))
                {
                    var fileInfo = provider.GetFileInfo(fileName);
                    result.Add(fileInfo.Name);
                }
            }
            return Ok(result);
        }


        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }



        
        [Route("addDocEquip")]
        [HttpPost]
        public JsonResult AddDocumentEquipement(DocumentEquipement DE)
        {
            string query = @"insert into DocumentEquipement (CodeEquipement,CodeDocument,Description,DateCreation,ExtentionFile)
                                                     values (@CodeEquipement, @CodeDocument, @Description, @DateCreation, @ExtentionFile)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@CodeEquipement", DE.CodeEquipement);
                    myCommand.Parameters.AddWithValue("@CodeDocument", DE.CodeDocument);
                    myCommand.Parameters.AddWithValue("@Description", DE.Description);
                    myCommand.Parameters.AddWithValue("@DateCreation", DateTime.Now);
                    myCommand.Parameters.AddWithValue("@ExtentionFile", DE.ExtentionFile);
                    MyReader = myCommand.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
            }
            return new JsonResult("add succ");
        }




        [Route("GetAllDocEquip")]
        [HttpGet]
        public JsonResult GetAllDocEquip()
        {
            string query = @"select * from DocumentEquipement";
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

    }
}
