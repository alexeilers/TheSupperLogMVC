using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<RatingListItem>> GetAllRatingsAsync()
        {
            var ratingQuery = _context
                .Ratings
                .Select(m =>
                new RatingListItem
                {
                    Id = m.Id,
                    Rating = m.Rating,
                });
            return await ratingQuery.ToListAsync();
        }

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
