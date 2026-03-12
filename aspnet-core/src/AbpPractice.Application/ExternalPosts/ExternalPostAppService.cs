using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AbpPractice.ExternalPosts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbpPractice.ExternalPosts;

/// <summary>
/// Coordinates external post retrieval use cases for the API layer.
/// </summary>
public class ExternalPostAppService : ApplicationService, IExternalPostAppService
{
    private readonly IExternalPostService _externalPostService;

    public ExternalPostAppService(IExternalPostService externalPostService)
    {
        _externalPostService = externalPostService;
    }

    /// <summary>
    /// Returns all posts from the external API.
    /// </summary>
    public async Task<List<ExternalPostDto>> GetAllAsync()
    {
        return await _externalPostService.GetPostsAsync();
    }

    /// <summary>
    /// Returns a single post by id from the external API.
    /// </summary>
    public async Task<ExternalPostDto?> GetByIdAsync(EntityDto<int> input)
    {
        return await _externalPostService.GetPostByIdAsync(input.Id);
    }
}