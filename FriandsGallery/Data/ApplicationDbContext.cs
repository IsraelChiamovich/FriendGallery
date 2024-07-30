using FriandsGallery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;

namespace FriandsGallery.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
			Seed();
        }

        public DbSet<FriendModel> Friends { get; set; }

        public DbSet<ImageModel> Images { get; set; }


		private void Seed()
		{
			if (Friends.IsNullOrEmpty())
			{
				ImmutableList<FriendModel> newFriends = [
					new () {FirstName = "Ross", LastName = "Geller"},
					new () {FirstName = "Monica", LastName = "Geller"},
					new () {FirstName = "Joey", LastName = "Tribiani"}
                   ];
				Friends.AddRange(newFriends);
				SaveChanges();
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<FriendModel>()
				.HasMany(f => f.Images)
				.WithOne(i => i.Friend)
				.HasForeignKey(i => i.FriendId);
		}
	}
}
