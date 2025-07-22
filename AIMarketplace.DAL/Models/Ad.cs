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
        public string Title {  get; set; }
        public string Description { get; set; }
        public string Category {  get; set; }
        public string Tags {  get; set; }
        public double Price {  get; set; }
        public DateTime DateCreated {  get; set; }
        public DateTime DateUpdated { get; set; }
        public int Views { get; set; }
        public AdStatus Status {  get; set; }
        public string VectorEmbedding {  get; set; } // vector to find similar ads
        
        //public string Image { get; set; } - later
        //public DateTime ExpirationDate { get; set; } - later
        
    }

    public enum AdStatus
    {
        Published,
        Sold,
        //Pending - in future
        //Archived - in future
    }
}
