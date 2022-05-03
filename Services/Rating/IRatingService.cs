using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheSupperLog.Models.Rating;

namespace TheSupperLog.Services.Rating
{
    public interface IRatingService
    {

        //Task<bool> CreateRatingAsync(RatingCreate request);
        Task<IEnumerable<RatingListItem>> GetAllRatingsAsync();
        //Task<bool> UpdateRatingRatingAsync(RatingEdit request);
        //Task<bool> DeleteRatingAsync(int recipeId);
    }
}
