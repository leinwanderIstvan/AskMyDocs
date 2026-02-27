namespace AskMyDocs.Core.Models;

public record EmbeddedChunk(string SourceFilePath, int ChunkIndex, string Text, int TokenCount, float[] Vector) 
    : DocumentChunk(SourceFilePath, ChunkIndex, Text, TokenCount);