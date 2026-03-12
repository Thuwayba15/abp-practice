using AbpPractice.ExternalPosts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbpPractice.ExternalPosts;

/// <summary>
/// Defines how posts are retrieved from the external API.
/// </summary>
public interface IExternalPostService
{
    /// <summary>
    /// Retrieves all posts from the external API.
    /// </summary>
    Task<List<ExternalPostDto>> GetPostsAsync();

    /// <summary>
    /// Retrieves a single post by id from the external API.
    /// </summary>
    Task<ExternalPostDto?> GetPostByIdAsync(int id);
}