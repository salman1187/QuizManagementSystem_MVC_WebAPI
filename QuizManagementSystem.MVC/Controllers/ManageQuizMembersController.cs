using QuizManagementSystem.DataAccess.Entities;
using QuizManagementSystem.MVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace QuizManagementSystem.MVC.Controllers
{
    public class ManageQuizMembersController : Controller
    {
        public ActionResult Index()
        {
            string unplacedMembers = "https://localhost:44311/api/quiz/members/unplaced";
            string allTeams = "https://localhost:44311/api/quiz/teams";

            HttpClient client = new HttpClient();
            var request = client.GetAsync(allTeams);
            var response = request.Result;
            List<Team> teams = null;
            if (response.IsSuccessStatusCode)
            {
                teams = response.Content.ReadAsAsync<List<Team>>().Result;
            }
            ViewBag.Teams = teams as List<Team>;

            HttpClient client1 = new HttpClient();
            var request1 = client1.GetAsync(unplacedMembers); // Change client to client1
            var response1 = request1.Result; // Change response to response1
            List<Member> umembers = null;
            if (response1.IsSuccessStatusCode) // Change response to response1
            {
                umembers = response1.Content.ReadAsAsync<List<Member>>().Result; // Change response to response1
            }
            ViewBag.UnplacedMembers = umembers as List<Member>;
            return View();
        }
        public async Task<ActionResult> Add(TeamMemberViewModel viewModel)
        {
            // Construct the URL
            string addMemberUrl = $"https://localhost:44311/api/quiz/teams/{viewModel.TeamId}/members/{viewModel.MemberId}";

            // Initialize HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Make the POST request to the API endpoint
                HttpResponseMessage response = await client.PostAsync(addMemberUrl, null);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var member = await response.Content.ReadAsAsync<Member>();
                    
                }
                
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(TeamMemberViewModel viewModel)
        {
            // Construct the URL
            string removeMemberUrl = $"https://localhost:44311/api/quiz/teams/{viewModel.TeamId}/members/{viewModel.MemberId}";

            // Initialize HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Make the POST request to the API endpoint
                HttpResponseMessage response = await client.PutAsync(removeMemberUrl, null);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var member = await response.Content.ReadAsAsync<Member>();

                }

            }
            return RedirectToAction("Index");
        }
    }
}
