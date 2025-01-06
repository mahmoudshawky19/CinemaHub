 using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [PrimaryKey("MovieId", "ApplicationUserId")]

    public class Cart
    {
        public int MovieId { get; set; }
        [ForeignKey(nameof(MovieId))]
        [ValidateNever]
        public Movie Movie { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public int count { get; set; }

    }
}
