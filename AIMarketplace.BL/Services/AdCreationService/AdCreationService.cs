using AIMarketplace.BL.DTOs;
using AIMarketplace.BL.Statuses;
using AIMarketplace.DAL.Models;
using AIMarketplace.DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMarketplace.BL.Services.AdCreationService
{
    public class AdCreationService : IAdCreationService
    {
        private IGenericRepo<Ad> _adRepos;
        public AdCreationService(IGenericRepo<Ad> adRepos)
        {
            _adRepos = adRepos;
        }
        public async Task<AdCreationResult> CreateAd(AdDto ad)
        {
            #region Validation
            var validation = ValidateDescription(ad.Description);
            if(validation.Status != AdCreationStatus.Success)
            {
                return validation;
            }

            validation = ValidateString(ad.Title);
            if(validation.Status != AdCreationStatus.Success)
            {
                return validation;
            }

            validation = ValidateString(ad.Category);
            if (validation.Status != AdCreationStatus.Success)
            {
                return validation;
            }
            #endregion
        
            Ad currentAd = new() //temporary data for test without AI
            { 
                Description = ad.Description,
                Title = ad.Title,
                Category = ad.Category,
                Tags = ad.Tags,
                VectorEmbedding = "vectorembedding1"
            };

            Guid adId = await _adRepos.Add(currentAd);

            return new AdCreationResult
            {
                ReturnObject = adId,
                Status = AdCreationStatus.Success
            };
        }

        public async Task<AdCreationResult> UpdateAd(Guid id, AdDto ad)
        {
            #region Validation

            //1. Empty ID
            if (id == Guid.Empty)
            {
                return new AdCreationResult()
                {
                    Status = AdCreationStatus.NotFound,
                    ReturnObject = "Id is null"
                };
            }

            //2. Does entity exist?
            var entity = await _adRepos.GetById(id);
            if(entity == null)
            {
                return new AdCreationResult()
                {
                    Status = AdCreationStatus.NotFound,
                    ReturnObject = "Ad with this ID doesn't exist"
                };
            }
            #endregion

            //3. Title validated and updated
            var validation = new AdCreationResult();

            if (!string.IsNullOrWhiteSpace(ad.Title))
            {
                validation = ValidateString(ad.Title);
                if (validation.Status != AdCreationStatus.Success)
                {
                    return validation;
                }

                entity.Title = ad.Title;
            }

            //4. Description validated and updated
            if(!string.IsNullOrWhiteSpace(ad.Description))
            {
                validation = ValidateDescription(ad.Description);
                if (validation.Status != AdCreationStatus.Success)
                {
                    return validation;
                }

                entity.Description = ad.Description;
            }

            //5. Category validated and updated
            if(!string.IsNullOrWhiteSpace(ad.Category))
            {
                validation = ValidateString(ad.Category);
                if(validation.Status != AdCreationStatus.Success)
                {
                    return validation;
                }

                entity.Category = ad.Category;
            }

            //6. Tags validated and updated
            if(!string.IsNullOrEmpty(ad.Tags))
            {
                validation = ValidateString(ad.Tags); // other validation will be provided later
                if (validation.Status != AdCreationStatus.Success)
                {
                    return validation;
                }

                entity.Tags = ad.Tags;
            }

            try
            {
                await _adRepos.Update(entity);
                return new()
                {
                    Status = AdCreationStatus.Success,
                    ReturnObject = true
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Status = AdCreationStatus.Unhandled,
                    ReturnObject = $"Error while updating\n\n {ex.Message}"
                };
            }
            
            
        }

        public async Task<AdCreationResult> DeleteAd(Guid id)
        {
            #region Validation
            if (Guid.Empty == id)
            {
                return new AdCreationResult()
                {
                    Status = AdCreationStatus.NotFound,
                    ReturnObject = "Id is null",
                };
            }
            if(_adRepos.GetById(id) == null)
            {
                return new AdCreationResult()
                {
                    Status = AdCreationStatus.NotFound,
                    ReturnObject = "Ad with this ID doesn't exist"
                };
            }
            #endregion

            try
            {
                await _adRepos.DeleteById(id);
                return new AdCreationResult()
                {
                    Status = AdCreationStatus.Success,
                    ReturnObject = "Deleted successfully!"
                };
            }
            catch(Exception ex) 
            {
                return new AdCreationResult()
                {
                    Status = AdCreationStatus.NotFound,
                    ReturnObject = $"Something went wrong. Error message: \n\n {ex.Message}"
                };
            }
        }

        public async Task<AdCreationResult> GetAdById(Guid adId)
        {
            if (Guid.Empty == adId)
            {
                return new AdCreationResult()
                {
                    Status = AdCreationStatus.NotFound,
                    ReturnObject = "Ad with this ID doesn't exist"
                };
            }

            try
            {
                var advertisement = await _adRepos.GetById(adId);
                return new AdCreationResult()
                {
                    Status = AdCreationStatus.Success,
                    ReturnObject = ShowInfo(advertisement)
                };
            }
            catch (Exception ex)
            {
                return new AdCreationResult()
                {
                    Status = AdCreationStatus.NotFound,
                    ReturnObject = $"Something went wrong. Error message:\n\n{ex.Message}"
                };
            }
        }

        public async Task<string> ShowAllAds()
        {
            var allAds = await _adRepos.GetAll();
            string result = string.Empty;
            foreach (var ad in allAds)
            {
                result += ShowInfo(ad);
                result += "\n\n";
            }
            return result;
        }

        public static string ShowInfo(Ad ad)
        {
            return $"Id: {ad.Id}\n" +
                    $"Title: {ad.Title}\n" +
                    $"Description: {ad.Description}\n" +
                    $"Category: {ad.Category}\n" +
                    $"Tags: {ad.Tags}";
        }

        private AdCreationResult ValidateDescription(string description)
        {
            // 1. Check if not empty
            if (string.IsNullOrEmpty(description))
            {
                return new AdCreationResult()
                {
                    Status = Statuses.AdCreationStatus.InvalidData,
                    ReturnObject = "Empty description"
                };
            }

            // 2. Check length without spaces
            string withoutSpaceDesc = description.Replace(" ", "");
            if (withoutSpaceDesc.Length < 20)
            {
                return new AdCreationResult()
                {
                    Status = Statuses.AdCreationStatus.InvalidData,
                    ReturnObject = "Description must be not less than 20 characters"
                };
            }

            // 3. Check length without punctuation and digits. What if a person inputs '$3423#232'? It's impossible to move further with that.
            int letters = 0;
            foreach (var symbol in withoutSpaceDesc)
            {
                if (char.IsLetter(symbol))
                {
                    letters++;
                }
            }
            if (letters < withoutSpaceDesc.Length / 2) // just an imagionary algorithm. impossible for a use to have a description like 'as!on!eo@fe4xa@mp9le!s"
            {
                return new AdCreationResult()
                {
                    Status = Statuses.AdCreationStatus.InvalidData,
                    ReturnObject = "Too many digits or puncutations. Add more letters"
                };
            }

            return new()
            {
                Status = AdCreationStatus.Success,
                ReturnObject = "Successfully validated description"
            };
        }
        private AdCreationResult ValidateString(string title)
        {
            if(string.IsNullOrEmpty(title))
            {
                return new()
                {
                    Status = AdCreationStatus.InvalidData,
                    ReturnObject = "Empty string"
                };
            }

            // 2. Check length without spaces
            string withoutSpaceDesc = title.Replace(" ", "");
            if (withoutSpaceDesc.Length < 3)
            {
                return new AdCreationResult()
                {
                    Status = AdCreationStatus.InvalidData,
                    ReturnObject = "string must be not less than 3 characters"
                };
            }

            //3. AI moderating

            return new()
            {
                Status = AdCreationStatus.Success,
                ReturnObject = "Validation succeeded"
            };
        }
        // To be moderated by AI - Title, Description, Category, Tags being RE-checked and VectorEmbedding being RE-updated
        /*private bool ValidateCategory(string category)
        {
        }
        private bool ValidateTags(string tags)
        { }*/

    }
}
