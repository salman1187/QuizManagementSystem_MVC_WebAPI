using QuizManagementSystem.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace QuizManagementSystem.WebApi.Controllers
{
    public class TeamsController : ApiController
    {
        QuizTeamDbContext db = new QuizTeamDbContext();
        [HttpGet]
        [Route("api/quiz/teams")]
        public IHttpActionResult GetTeams()
        {
            var teams = db.Teams.ToList();
            if (teams.Count == 0 || teams == null)
                return NotFound();
            return Ok(teams);
        }
        [HttpGet]
        [Route("api/quiz/teams/{id}")]
        public IHttpActionResult GetTeamById(int id)
        {
            var team = db.Teams.Find(id);
            if(team == null)
                return BadRequest();
            return Ok(team);
        }
        [HttpGet]
        [Route("api/quiz/teams/{teamid}/members/{memberid}")]
        public IHttpActionResult GetTeamMember(int teamid, int memberid)
        {
            var teams = db.Teams.ToList();
            var members = db.Members.ToList();
            if (teams == null || members == null)
                return BadRequest();

           var mem = (from m in members
                     where m.TeamId == teamid && m.MemberId == memberid
                     select m).FirstOrDefault();
           return Ok(mem);
        }
        [HttpGet]
        [Route("api/quiz/teams/{teamid}/members")]
        public IHttpActionResult GetTeamMembers(int teamid)
        {
            var teams = db.Teams.ToList();
            var members = db.Members.ToList();
            if (teams == null || members == null)
                return NotFound();

            var mems = (from m in members
                       where m.TeamId == teamid
                       select m).ToList();
            return Ok(mems);
        }
        [HttpPost]
        [Route("api/quiz/teams/{teamid}/members/{memberid}")]
        public IHttpActionResult PostTeamMember(int teamid, int memberid)
        {
           
            var member = db.Members.Find(memberid);
            if(member == null)
                return BadRequest();

            member.isPlaced = true;
            member.TeamId = teamid;
            db.SaveChanges(); //!!!
            var team = db.Teams.Find(teamid);
            team.MemberIds.Add(memberid);
            team.TheMembers.Add(member);
            db.SaveChanges();

            return Created($"api/teams/{team.TeamId}/members/{member.MemberId}", member);
        }
        [HttpPut]
        [Route("api/quiz/teams/{teamid}/members/{memberid}")]
        public IHttpActionResult PutTeamMember(int teamid, int memberid)
        {
            var team = db.Teams.Find(teamid);
            var member = db.Members.Find(memberid);
            if (team == null || member == null)
                return BadRequest();

            member.isPlaced = false;
            member.TeamId = null;
            team.MemberIds.Remove(memberid);
            team.TheMembers.Remove(member);
            db.SaveChanges();

            return Ok();
        }
    }
}