using QuizManagementSystem.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace QuizManagementSystem.WebApi.Controllers
{
    public class MembersController : ApiController
    {
        QuizTeamDbContext db = new QuizTeamDbContext();
        [HttpGet]
        [Route("api/quiz/members")]
        public IHttpActionResult GetMembers()
        {
            var mems = db.Members.ToList();
            if(mems.Count == 0 || mems == null)
                return NotFound();
            return Ok(mems);
        }
        [HttpGet]
        [Route("api/quiz/members/{id}")]
        public IHttpActionResult GetMember(int id)
        {
            var mem = db.Members.Find(id);
            if (mem == null)
                return BadRequest();
            return Ok(mem);
        }
        [HttpGet]
        [Route("api/quiz/members/unplaced")]
        public IHttpActionResult GetUnplacedMembers()
        {
            var mems = (from m in db.Members
                       where m.isPlaced == false
                       select m).ToList();
            if (mems.Count == 0 || mems == null)
                return NotFound();
            return Ok(mems);
        }
    }
}