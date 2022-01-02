using simple_blog_api.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace simple_blog_api.Services
{
    public class BlogService
    {
        private readonly IMongoCollection<Blog> _blogCollection;

        public BlogService(IOptions<BlogDatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(
            settings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                settings.Value.DatabaseName);

            _blogCollection = mongoDatabase.GetCollection<Blog>(
                settings.Value.CollectionName);
        }

        public async Task<List<Blog>> GetAllAsync() =>
            await _blogCollection.Find(_ => true).ToListAsync();

        public async Task<Blog?> GetAsync(string id) =>
            await _blogCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(Blog blog) =>
            await _blogCollection.InsertOneAsync(blog);

        public async Task DeleteAsync(string id) =>
            await _blogCollection.DeleteOneAsync(x => x.Id == id);

    }
}