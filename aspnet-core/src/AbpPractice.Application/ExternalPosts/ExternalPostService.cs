using AbpPractice.ExternalPosts.Dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AbpPractice.ExternalPosts;

/// <summary>
/// Handles communication with the external posts API.
/// </summary>
public class ExternalPostService : IExternalPostService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ExternalPostService> _logger;

    public ExternalPostService(
        HttpClient httpClient,
        ILogger<ExternalPostService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all posts from the external API.
    /// </summary>
    public async Task<List<ExternalPostDto>> GetPostsAsync()
    {
        try
        {
            _logger.LogInformation("Retrieving all posts from the external API.");

            var posts = await _httpClient.GetFromJsonAsync<List<ExternalPostDto>>("posts");

            return posts ?? new List<ExternalPostDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve posts from the external API.");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a single post by id from the external API.
    /// </summary>
    public async Task<ExternalPostDto?> GetPostByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Retrieving external post with id {PostId}.", id);

            return await _httpClient.GetFromJsonAsync<ExternalPostDto>($"posts/{id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve external post with id {PostId}.", id);
            throw;
        }
    }
}