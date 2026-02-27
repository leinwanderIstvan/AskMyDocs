using AskMyDocs.Core.Services;

namespace AskMyDocs.Tests;

public class FixedSizeChunkerTests
{
    private readonly FixedSizeChunker _sut = new();
    private const string FilePath = "test.txt";

    [Fact]
    public void WhenTextIsEmpty_ThenNoChunksReturned()
    {
        var chunks = _sut.GetChunks(FilePath, "").ToList();

        Assert.Empty(chunks);
    }

    [Fact]
    public void WhenTextFitsInOneChunk_ThenSingleChunkReturned()
    {
        var text = string.Join(" ", Enumerable.Repeat("word", 100));

        var chunks = _sut.GetChunks(FilePath, text).ToList();

        Assert.Single(chunks);
    }

    [Fact]
    public void WhenTextFitsInOneChunk_ThenChunkIndexIsZero()
    {
        var text = string.Join(" ", Enumerable.Repeat("word", 100));

        var chunks = _sut.GetChunks(FilePath, text).ToList();

        Assert.Equal(0, chunks[0].ChunkIndex);
    }

    [Fact]
    public void WhenTextExceedsOneChunk_ThenChunkIndicesAreSequential()
    {
        // 1000 words forces at least 2 chunks
        var text = string.Join(" ", Enumerable.Repeat("word", 1000));

        var chunks = _sut.GetChunks(FilePath, text).ToList();

        for (var i = 0; i < chunks.Count; i++)
            Assert.Equal(i, chunks[i].ChunkIndex);
    }

    [Fact]
    public void WhenTextExceedsOneChunk_ThenOverlapExists()
    {
        // Build text where each word is its index so we can detect overlap
        var words = Enumerable.Range(0, 1000).Select(i => $"word{i}").ToArray();
        var text = string.Join(" ", words);

        var chunks = _sut.GetChunks(FilePath, text).ToList();

        // Last word of chunk 0 should appear at the start of chunk 1
        var lastWordOfChunk0 = chunks[0].Text.Split(' ').Last();
        Assert.Contains(lastWordOfChunk0, chunks[1].Text);
    }

    [Fact]
    public void WhenChunkCreated_ThenSourceFilePathIsPreserved()
    {
        var text = string.Join(" ", Enumerable.Repeat("word", 10));

        var chunks = _sut.GetChunks(FilePath, text).ToList();

        Assert.All(chunks, c => Assert.Equal(FilePath, c.SourceFilePath));
    }

    [Fact]
    public void WhenChunkCreated_ThenTokenCountMatchesWordCount()
    {
        var text = string.Join(" ", Enumerable.Repeat("word", 100));

        var chunks = _sut.GetChunks(FilePath, text).ToList();

        Assert.Equal(100, chunks[0].TokenCount);
    }
}
