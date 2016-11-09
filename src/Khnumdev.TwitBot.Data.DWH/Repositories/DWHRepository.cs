namespace Khnumdev.TwitBot.Data.DWH.Repositories
{
    using Model.Dimensions;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System;

    public class DWHRepository : IDWHRepository
    {

        public async Task<int> AddAsync<T>(T entity)
            where T : class, IDimension
        {
            int result = 0;
            using (var context = new DWHContext())
            {
                context.Set<T>().Add(entity);
                await context.SaveChangesAsync();
                result = entity.Id;

                return result;
            }
        }

        public async Task AddFactAsync<T>(T entity) where T : class
        {
            using (var context = new DWHContext())
            {
                context.Set<T>().Add(entity);
                await context.SaveChangesAsync();
            }
        }

        async Task<int> Save<T>(DWHContext context, T existingEntity, T entity)
            where T : class, IDimension
        {
            int result = 0;

            if (existingEntity == null)
            {
                context.Set<T>().Add(entity);
                await context.SaveChangesAsync();
                result = entity.Id;
            }
            else
            {
                result = existingEntity.Id;
            }

            return result;
        }

        public async Task UpsertAsync(List<Channel> entities)
        {
            var distinctEntities = entities
                .Select(i => i.Name)
                .Distinct()
                .Select(i => new Channel { Name = i });

            var allEntities = await GetAllAsync<Channel>();

            var newEntities = distinctEntities
                .Where(i => !allEntities.Any(j => j.Name == i.Name));

            using (var context = new DWHContext())
            {
                context.Set<Channel>().AddRange(newEntities);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpsertAsync(List<ConversationTrack> entities)
        {
            var distinctEntities = entities
               .Select(i => i.ConversationId)
               .Distinct()
               .Select(i => new ConversationTrack { ConversationId = i });

            var allEntities = await GetAllAsync<ConversationTrack>();

            var newEntities = distinctEntities
                .Where(i => !allEntities.Any(j => j.ConversationId == i.ConversationId));

            using (var context = new DWHContext())
            {
                context.Set<ConversationTrack>().AddRange(newEntities);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpsertAsync(List<Language> entities)
        {
            var distinctEntities = entities
               .Select(i => i.Name)
               .Distinct()
               .Select(i => new Language { Name = i });

            var allEntities = await GetAllAsync<Language>();

            var newEntities = distinctEntities
                .Where(i => !allEntities.Any(j => j.Name == i.Name));

            using (var context = new DWHContext())
            {
                context.Set<Language>().AddRange(newEntities);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpsertAsync(List<MessageSource> entities)
        {
            var distinctEntities = entities
               .Select(i => i.Source)
               .Distinct()
               .Select(i => new MessageSource { Source = i });

            var allEntities = await GetAllAsync<MessageSource>();

            var newEntities = distinctEntities
                .Where(i => !allEntities.Any(j => j.Source == i.Source));

            using (var context = new DWHContext())
            {
                context.Set<MessageSource>().AddRange(newEntities);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpsertAsync(List<MessageType> entities)
        {
            var distinctEntities = entities
                .Select(i => i.Name)
                .Distinct()
                .Select(i => new MessageType { Name = i });

            var allEntities = await GetAllAsync<MessageType>();

            var newEntities = distinctEntities
                .Where(i => !allEntities.Any(j => j.Name == i.Name));

            using (var context = new DWHContext())
            {
                context.Set<MessageType>().AddRange(newEntities);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpsertAsync(List<User> entities)
        {
            var distinctEntities = entities
               .Select(i => new { i.UserId, i.Name })
               .Distinct()
               .Select(i => new User { UserId = i.UserId , Name = i.Name });

            var allEntities = await GetAllAsync<User>();

            var newEntities = distinctEntities
                .Where(i => !allEntities.Any(j => j.UserId == i.UserId));

            using (var context = new DWHContext())
            {
                context.Set<User>().AddRange(newEntities);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpsertAsync(List<SingleWord> entities)
        {
            var distinctEntities = entities
               .Select(i => i.Text)
               .Distinct()
               .Select(i => new SingleWord { Text = i });

            var allEntities = await GetAllAsync<SingleWord>();

            var newEntities = distinctEntities
                .Where(i => !allEntities.Any(j => j.Text == i.Text));

            using (var context = new DWHContext())
            {
                context.Set<SingleWord>().AddRange(newEntities);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpsertAsync(List<TrendingTopic> entities)
        {
            var distinctEntities = entities
               .Select(i => new { i.Name, i.IsPromoted })
               .Distinct()
               .Select(i => new TrendingTopic { Name = i.Name, IsPromoted = i.IsPromoted });

            var allEntities = await GetAllAsync<TrendingTopic>();

            var newEntities = distinctEntities
                .Where(i => !allEntities.Any(j => j.Name == i.Name ));

            using (var context = new DWHContext())
            {
                context.Set<TrendingTopic>().AddRange(newEntities);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpsertAsync(List<Geography> entities)
        {
            var distinctEntities = entities
               .Select(i => i.Name)
               .Distinct()
               .Select(i => new Geography { Name = i });

            var allEntities = await GetAllAsync<Geography>();

            var newEntities = distinctEntities
                .Where(i => !allEntities.Any(j => j.Name == i.Name));

            using (var context = new DWHContext())
            {
                context.Set<Geography>().AddRange(newEntities);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllAsync<T>()
            where T : class, IDimension
        {
            using (var context = new DWHContext())
            {
                return await context
                    .Set<T>()
                    .ToListAsync();
            };
        }
    }
}
