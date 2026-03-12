using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AbpPractice.ExternalPosts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbpPractice.ExternalPosts;

/// <summary>
/// Exposes external post retrieval use cases.
/// </summary>
public interface IExternalPostAppService : IApplicationService
{
    /// <summary>
    /// Returns all posts from the external API.
    /// </summary>
    Task<List<ExternalPostDto>> GetAllAsync();

    /// <summary>
    /// Returns a single post by id from the external API.
    /// </summary>
    Task<ExternalPostDto?> GetByIdAsync(EntityDto<int> input);
}