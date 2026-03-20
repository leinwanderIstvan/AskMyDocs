using AskMyDocs.Core.Interfaces;
using AskMyDocs.Core.Models;
using Microsoft.Extensions.VectorData;

namespace AskMyDocs.Infrastructure.Repositories.VectorStore;

public class ChunkRepository(VectorStoreCollection<Guid, ChunkRecord> collection) : IChunkRepository
{
    public Task UpsertAsync(ChunkRecord chunk, CancellationToken ct = default)
    {
        return collection.UpsertAsync(chunk, ct);

    }

    public async Task<IEnumerable<ChunkRecord>> SearchAsync(ReadOnlyMemory<float> vector, int topK,
        CancellationToken ct = default)
    {
        IAsyncEnumerable<VectorSearchResult<ChunkRecord>> searchResult = collection.SearchAsync(vector, topK, cancellationToken: ct);
        List<VectorSearchResult<ChunkRecord>> resultItems = await searchResult.ToListAsync(cancellationToken: ct);

        return resultItems.Select(x => x.Record);
    }

}