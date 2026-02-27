
using AskMyDocs.Core.Models;

namespace AskMyDocs.Core.Interfaces;

public interface IDocumentChunker
{
    IEnumerable<DocumentChunk> GetChunks(string sourceFilePath, string text);
}