using AskMyDocs.Core.Models;

namespace AskMyDocs.Core.Interfaces;

public interface IChunkRepository
{
    Task UpsertAsync(ChunkRecord chunk, CancellationToken ct = default);
    Task<IEnumerable<ChunkRecord>> SearchAsync(ReadOnlyMemory<float> vector, int topK, CancellationToken ct = default);
}