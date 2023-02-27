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
using System.Web;
using Microsoft.Data.SqlClient;
using System.Collections;




namespace GMAO.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class CentreCoutsController : Controller
    {
        //private CentreCoutSeq centreCoutSeq;
        private readonly GMAOContext _context;
        private readonly IConfiguration _configuration;
        public CentreCoutsController(GMAOContext context, IConfiguration configuration)
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



        // GET: CentreCouts

        [Route("GetAllCentreCout")]
        [HttpGet]
        public JsonResult Get()
        {
            //string query = @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            SqlConnection myConn = new SqlConnection(sqlDataSource);
            SqlCommand myCommand = new SqlCommand("[dbo].LstCentreCC", myConn);
            SqlCommand myCommandCC = new SqlCommand("select * from dbo.CentreCoutSeq order by Seq", myConn);
            myCommand.CommandType = CommandType.StoredProcedure;
            using (myConn)

            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                using (myCommand)
                {
                    MyReader = myCommandCC.ExecuteReader();
                    table.Load(MyReader);
                    MyReader.Close();
                    myConn.Close();
                }
                myConn.Close();
            }

            return new JsonResult(table);
        }




        // POST: CentreCouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeCC,Designation,CodeAnt")] CentreCout centreCout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centreCout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(centreCout);
        }

        // GET: CentreCouts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centreCout = await _context.CentreCout.FindAsync(id);
            if (centreCout == null)
            {
                return NotFound();
            }
            return View(centreCout);
        }

        // POST: CentreCouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodeCC,Designation,CodeAnt")] CentreCout centreCout)
        {
            if (id != centreCout.CodeCC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centreCout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentreCoutExists(centreCout.CodeCC))
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
            return View(centreCout);
        }

        // GET: CentreCouts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centreCout = await _context.CentreCout
                .FirstOrDefaultAsync(m => m.CodeCC == id);
            if (centreCout == null)
            {
                return NotFound();
            }

            return View(centreCout);
        }

        // POST: CentreCouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var centreCout = await _context.CentreCout.FindAsync(id);
            _context.CentreCout.Remove(centreCout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentreCoutExists(string id)
        {
            return _context.CentreCout.Any(e => e.CodeCC == id);
        }







        public string presentCC(CentreCout p, Hashtable liens, Hashtable lst, string jd)
        {

            string r = "{text : " + jd + (p.Designation) + ",id2 : '" + p.CodeCC + "', leaf:";
            if (liens[p.CodeCC] != null)
            {
                r += "false, children: [";
                foreach (string cc in (List<string>)liens[p.CodeCC])
                {
                    r += presentCC((CentreCout)lst[cc], liens, lst, jd) + ",";
                }
                r += "]";
            }
            else r += "true";
            r += "}";
            return r;
        }

        [Route("ListeChildren")]
        [HttpGet]
        protected void getlstchildrens()
        {
            //System.Web.Script.Serialization.JavaScriptSerializer jd = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jd = "";
            Hashtable lst = new Hashtable();
            Hashtable liens = (Hashtable)lst[""];
            string storeCentreCout = "[";
            storeCentreCout += presentCC((CentreCout)lst["U"], liens, lst, jd);
            storeCentreCout += "]";
            Response.Redirect(storeCentreCout);
            //Response.write(storeCentreCout);

        }


        [Route("ListeCentreCoutHT")]
        [HttpGet]
        public Hashtable listecc()
        {
            Hashtable lst = new Hashtable();
            Hashtable liens = new Hashtable();
            foreach (CentreCout pp in (from p in _context.CentreCout select p).ToList<CentreCout>())
            {
                lst.Add(pp.CodeCC, pp);
                if (pp.CodeCC == "U") continue;
                if (liens[pp.CodeAnt] == null)
                    liens.Add(pp.CodeAnt, new List<string>());
                ((List<string>)(liens[pp.CodeAnt])).Add(pp.CodeCC);
            }
            lst.Add("", liens);
            //instance = null;
            return lst;
        }





        public List<CentreCoutSeq> getList()
        {
            List<CentreCoutSeq> CCSeq = new List<CentreCoutSeq>();
              CCSeq  = _context.CentreCoutSeq.ToList();
            return CCSeq;
        }

        //[Route("ListeCentreCoutSeq")]
        //[HttpGet]
        //public List<CentreCoutSeq> transformListCentreObj(List<CentreCoutSeq> list)
        //{

        //    //List<CentreCoutSeq> data = new List<CentreCoutSeq>();
        //     List data = new System.Collections.Generic.List;
        //    data = this.getList();
        //    list.ForEach(centre=> {
        //         var listChilds = getChildren(centre, list);
        //        Console.WriteLine("listchilds",listChilds);
        //        if (listChilds!=null)
        //        {
        //            List<CentreCoutSeq> listItem = new List<CentreCoutSeq>();
        //            listItem.Add(centre);
        //            listItem.AddRange(listChilds);
        //            data.AddRange(listItem);
        //            //log.info("seq:{}, code:{}, desgn:{}, children:{}", centre.getseq(), centre.getCodeAnt(), centre.getDesignation(), listChilds);
        //        }
        //    });
        //    return data;
        //}

        [Route("ListeCentreCoutSeq")]
        [HttpGet]
        public List<List<List<CentreCoutSeq>>> transformListCentreObj1(List<CentreCoutSeq> list)
        {

            List<List<List<CentreCoutSeq>>> data = new List<List<List<CentreCoutSeq>>>();

            List<CentreCoutSeq> allCentreCout = getList();
            allCentreCout.ForEach(centre => {
                var listChilds = getChildren(centre, allCentreCout);

                //Console.WriteLine("listchilds", listChilds);
                if (listChilds != null && listChilds.Count > 0)
                {
                    List<List<CentreCoutSeq>> listItem = new List<List<CentreCoutSeq>> { new List<CentreCoutSeq> { centre } };
                    listItem.Add(listChilds);
                    data.Add(listItem);
                    //log.info("seq:{}, code:{}, desgn:{}, children:{}", centre.getseq(), centre.getCodeAnt(), centre.getDesignation(), listChilds);
                }
            });
            return data;
        }


        public List<CentreCoutSeq> getChildren(CentreCoutSeq centre, List<CentreCoutSeq> centreCoutSeqs)
        {
            return centreCoutSeqs.Where(centreCoutSeq=>centreCoutSeq.getCodeAnt().Equals(centre.getCodeCC())).ToList();
        }

        [Route("DeleteCC")]
        [HttpDelete("{CodeCC}")]
        // GET: CC/Delete/5
        public JsonResult DeleteCC(string CodeCC)
        {
            string query = @"delete from CentreCoutSeq where CodeCC=@CodeCC";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("GMAOContext");
            SqlDataReader MyReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConn))
                {
                    myCommand.Parameters.AddWithValue("@Codecc", CodeCC);
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
