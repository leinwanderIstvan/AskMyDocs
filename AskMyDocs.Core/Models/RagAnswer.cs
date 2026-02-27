namespace AskMyDocs.Core.Models;

public record RagAnswer(string Answer, EmbeddedChunk[] SourceChunks);