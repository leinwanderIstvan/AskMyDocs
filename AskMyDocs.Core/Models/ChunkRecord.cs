using Microsoft.Extensions.VectorData;

namespace AskMyDocs.Core.Models;

public class ChunkRecord
{
    [VectorStoreKey]
    public Guid ChunkId { get; set; }

    [VectorStoreData]
    public string? SourceFilePath { get; set; }

    [VectorStoreData (IsFullTextIndexed = true)]
    public string? Text { get; set; }

    [VectorStoreData]
    public int Index { get; set; }

    [VectorStoreVector(Dimensions: 1536, DistanceFunction = DistanceFunction.CosineSimilarity, IndexKind = IndexKind.Hnsw)]
    public ReadOnlyMemory<float>? Vector { get; set; }
}