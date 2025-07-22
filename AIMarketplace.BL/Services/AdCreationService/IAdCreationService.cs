using AIMarketplace.BL.DTOs;
using AIMarketplace.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMarketplace.BL.Services.AdCreationService
{
    public interface IAdCreationService
    {
        public Task<AdCreationResult> CreateAd(AdDto ad);
        public Task<AdCreationResult> GetAdById(Guid id);
        public Task<AdCreationResult> DeleteAd(Guid id);
        public Task<AdCreationResult> UpdateAd(Guid id, AdDto ad);
        public Task<string> ShowAllAds();
    }
}
