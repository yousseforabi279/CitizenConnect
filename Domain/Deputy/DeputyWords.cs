using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Deputy
{
    public class DeputyWords
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [Required]
        public string Video_image { get; set; }
      
    }
}
