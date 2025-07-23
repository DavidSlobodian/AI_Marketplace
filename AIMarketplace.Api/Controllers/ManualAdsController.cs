using AIMarketplace.BL.Services.AdCreationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AIMarketplace.BL;
using AIMarketplace.BL.Statuses;
using AIMarketplace.BL.DTOs;

namespace AIMarketplace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManualAdsController : ControllerBase
    {
        private readonly IAdCreationService _adService;
        public ManualAdsController(IAdCreationService adService)
        {
            _adService = adService;
        }
        [HttpPost("/create")]
        public async Task<IActionResult> CreateAd(AdDto ad)
        {
            var result = await _adService.CreateAd(ad);
            return result.Status switch
            {
                AdCreationStatus.Success => Ok(result.ReturnObject),
                _ => BadRequest(result.ReturnObject)
            };
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _adService.GetAdById(id);
            return result.Status switch
            {
                AdCreationStatus.Success => Ok(result.ReturnObject),
                _ => BadRequest(result.ReturnObject)
            };
        }

        [HttpPut("/update/{id}")]
        public async Task<IActionResult> Update(Guid id, AdDto ad, bool isSold = false)
        {
            var result = await _adService.UpdateAd(id, ad, isSold);
            return result.Status switch
            {
                AdCreationStatus.Success => Ok(result.ReturnObject),
                _ => BadRequest(result.ReturnObject)
            };
        }
        [HttpDelete("/delete/{id}")]
        public async Task<IActionResult> DeleteAdById(Guid id)
        {
            var result = await _adService.DeleteAd(id);
            return result.Status switch
            {
                AdCreationStatus.Success => Ok(result.ReturnObject),
                _ => BadRequest(result.ReturnObject)
            };
        }

        //DELETE IN PRODUCTION
        [HttpGet("/all")]
        public async Task<IActionResult> ShowAllAds()
        {
            var result = await _adService.ShowAllAds();
            return Ok(result);
        }
    }
}
