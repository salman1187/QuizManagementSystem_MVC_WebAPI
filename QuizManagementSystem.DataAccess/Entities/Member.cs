using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManagementSystem.DataAccess.Entities
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int? TeamId { get; set; }
        public bool isPlaced { get; set; }
    }
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public List<int> MemberIds { get; set; } = new List<int>();
        public virtual List<Member> TheMembers { get; set; } = new List<Member>();
    }
}
