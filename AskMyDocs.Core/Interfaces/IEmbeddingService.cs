using AskMyDocs.Core.Models;

namespace AskMyDocs.Core.Interfaces;

public interface IEmbeddingService
{
    Task<EmbeddedChunk> EmbedAsync(DocumentChunk chunk, CancellationToken ct = default);

}