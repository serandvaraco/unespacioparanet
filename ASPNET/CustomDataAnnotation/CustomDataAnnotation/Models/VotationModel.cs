using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomDataAnnotation.Models
{
    public class VotationModel
    {
        [AgeValidator(18, ErrorMessage ="La edad mínima para votar son 18 años")]
        public int VoterAge { get; set; }
    }
}