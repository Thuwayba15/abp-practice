namespace AbpPractice.ExternalPosts.Dto;

/// <summary>
/// Represents a post returned by the external API.
/// </summary>
public class ExternalPostDto
{
    public int UserId { get; set; }

    public int Id { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }
}