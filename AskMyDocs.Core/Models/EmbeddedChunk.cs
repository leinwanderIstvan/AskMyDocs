namespace AskMyDocs.Core.Models;
/// <summary>
/// Represents a chunk of text extracted from a document, along with its embedding vector and metadata about its source and token count.
/// </summary>
public record EmbeddedChunk(string SourceFilePath, int ChunkIndex, string Text, int TokenCount, float[] Vector) 
    : DocumentChunk(SourceFilePath, ChunkIndex, Text, TokenCount);