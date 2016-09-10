namespace Khnumdev.TwitBot.Data.DWH.Repositories
{
    using Model.Dimensions;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System;

    public class DWHRepository : IDWHRepository
    {
        public async Task<int> AddOrRetrieveIdAsync(Channel entity)
        {
            using (var context = new DWHContext())
            {
                var existingEntity = await context
                    .Set<Channel>()
                    .Where(i => i.Name == entity.Name)
                    .SingleOrDefaultAsync();

                return await Save(context, existingEntity, entity);
            }
        }

        public async Task<int> AddOrRetrieveIdAsync(ConversationTrack entity)
        {
            using (var context = new DWHContext())
            {
                var existingEntity = await context
                    .Set<ConversationTrack>()
                    .Where(i => i.ConversationId == entity.ConversationId)
                    .SingleOrDefaultAsync();

                return await Save(context, existingEntity, entity);
            }
        }

        public async Task<int> AddOrRetrieveIdAsync(Language entity)
        {
            using (var context = new DWHContext())
            {
                var existingEntity = await context
                    .Set<Language>()
                    .Where(i => i.Name == entity.Name)
                    .SingleOrDefaultAsync();

                return await Save(context, existingEntity, entity);
            }
        }

        public async Task<int> AddOrRetrieveIdAsync(MessageSource entity)
        {
            using (var context = new DWHContext())
            {
                var existingEntity = await context
                    .Set<MessageSource>()
                    .Where(i => i.Source == entity.Source)
                    .SingleOrDefaultAsync();

                return await Save(context, existingEntity, entity);
            }
        }

        public async Task<int> AddOrRetrieveIdAsync(MessageType entity)
        {
            using (var context = new DWHContext())
            {
                var existingEntity = await context
                    .Set<MessageType>()
                    .Where(i => i.Name == entity.Name)
                    .SingleOrDefaultAsync();

                return await Save(context, existingEntity, entity);
            }
        }

        public async Task<int> AddOrRetrieveIdAsync(User entity)
        {
            using (var context = new DWHContext())
            {
                var existingEntity = await context
                    .Set<User>()
                    .Where(i => i.Name == entity.Name)
                    .SingleOrDefaultAsync();

                return await Save(context, existingEntity, entity);
            }
        }

        public async Task<int> AddOrRetrieveIdAsync(SingleWord entity)
        {
            using (var context = new DWHContext())
            {
                var existingEntity = await context
                    .Set<SingleWord>()
                    .Where(i => i.Text == entity.Text)
                    .SingleOrDefaultAsync();

                return await Save(context, existingEntity, entity);
            }
        }

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
    }
}
