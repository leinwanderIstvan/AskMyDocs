using AskMyDocs.Core.Models;
using AskMyDocs.Core.Services;
using OpenAI.Embeddings;

namespace AskMyDocs.Tests;

public class OpenAiEmbeddingServiceIntegrationTests
{
    // Run with: OPENAI_API_KEY=sk-... dotnet test
    private static readonly string? ApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

    [Fact]
    public async Task WhenChunkIsEmbedded_ThenVectorHas1536Dimensions()
    {
        if (string.IsNullOrWhiteSpace(ApiKey))
            return; // Skip if no API key — safe to skip in CI without secrets

        var client = new EmbeddingClient("text-embedding-3-small", ApiKey);
        var service = new OpenAiEmbeddingService(client);
        var chunk = new DocumentChunk("test.txt", 0, "The cat sat on the mat.", 6);

        var result = await service.EmbedAsync(chunk);

        Assert.Equal(1536, result.Vector.Length);
    }

    [Fact]
    public async Task WhenChunkIsEmbedded_ThenOriginalChunkDataIsPreserved()
    {
        if (string.IsNullOrWhiteSpace(ApiKey))
            return;

        var client = new EmbeddingClient("text-embedding-3-small", ApiKey);
        var service = new OpenAiEmbeddingService(client);
        var chunk = new DocumentChunk("test.txt", 0, "The cat sat on the mat.", 6);

        var result = await service.EmbedAsync(chunk);

        Assert.Equal(chunk.SourceFilePath, result.SourceFilePath);
        Assert.Equal(chunk.ChunkIndex, result.ChunkIndex);
        Assert.Equal(chunk.Text, result.Text);
        Assert.Equal(chunk.TokenCount, result.TokenCount);
    }
}
