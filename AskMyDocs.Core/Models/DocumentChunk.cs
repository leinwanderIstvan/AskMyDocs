namespace AskMyDocs.Core.Models;

public record DocumentChunk(string SourceFilePath, int ChunkIndex, string Text, int TokenCount);