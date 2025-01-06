using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class FavoriteMovie
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        [ForeignKey(nameof(MovieId))]
        [ValidateNever]
        public Movie Movie { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
