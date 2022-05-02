using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheSupperLog.Data;
using TheSupperLog.Data.Entities;
using TheSupperLog.Models.Rating;

namespace TheSupperLog.Services.Rating
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;

        public RatingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRatingAsync(RatingCreate request)
        {
            var mealRating = new RatingEntity
            {
                MealId = request.MealId,
                Rating = request.Rating,
                DateAdded = DateTimeOffset.Now
            };

            _context.Ratings.Add(mealRating);

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteRatingAsync(int ratingId)
        {
            var ratingEntity = await _context.Ratings.FindAsync(ratingId);

            _context.Ratings.Remove(ratingEntity);
            return await _context.SaveChangesAsync() == 1; ;
        }

        //public async Task<IEnumerable<RatingListItem>> GetMealsByRatingAsync(int rating)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<bool> UpdateRatingRatingAsync(RatingEdit model)
        {
            var rating = await _context.Ratings.FindAsync(model.Id);

            rating.Rating = model.Rating;
            rating.DateModified = DateTimeOffset.Now;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}
