namespace AskMyDocs.Core.Models;

/// <summary>
/// Represents a chunk of text extracted from a document, along with metadata about its source and token count.
/// </summary>
public record DocumentChunk(string SourceFilePath, int ChunkIndex, string Text, int TokenCount);