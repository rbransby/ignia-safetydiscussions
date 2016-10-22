using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafetyDiscussionApplication.Models
{
    public class SafetyDiscussion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ObserverUserId { get; set; }

        [ForeignKey("ObserverUserId")]
        public User Observer { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }


        public int ColleagueUserId { get; set; }

        [ForeignKey("ColleagueUserId")]
        public User Colleague { get; set; }

        public string Subject { get; set; }
        public string Outcomes { get; set; }
    }
}