using AskMyDocs.Core.Interfaces;
using AskMyDocs.Core.Models;
using OpenAI.Embeddings;

namespace AskMyDocs.Core.Services;

public class OpenAiEmbeddingService(EmbeddingClient client) : IEmbeddingService
{
    public async Task<EmbeddedChunk> EmbedAsync(DocumentChunk chunk, CancellationToken ct = default)
    {
        var vector = await client.GenerateEmbeddingAsync(chunk.Text, cancellationToken: ct);
        var vectorMemory = vector.Value.ToFloats().ToArray();
        return new EmbeddedChunk(chunk.SourceFilePath, chunk.ChunkIndex, chunk.Text, chunk.TokenCount, vectorMemory);
    }


}