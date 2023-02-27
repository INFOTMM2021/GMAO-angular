using GMAO.Data;
using GMAO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeMouvementController : Controller
    {
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;

        public TypeMouvementController(GMAOContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [Route("GetAllTypeMouvementToList")]
        [HttpGet]
        public List<TypeMouvement> Get() => _context.TypeMouvement.ToList();
    }
}
