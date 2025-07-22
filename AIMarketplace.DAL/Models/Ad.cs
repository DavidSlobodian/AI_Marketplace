using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMarketplace.DAL.Models
{
    public class Ad : BaseEntity
    {
        public string Title {  get; set; } // by AI
        public string Description { get; set; } // by user
        public string Category {  get; set; } // by AI
        public string Tags {  get; set; } // by AI
        public string VectorEmbedding {  get; set; } // vector to find similar ads
        
    }
}
