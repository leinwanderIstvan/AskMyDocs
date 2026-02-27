using AskMyDocs.Core.Interfaces;
using AskMyDocs.Core.Models;

namespace AskMyDocs.Core.Services;

public class FixedSizeChunker : IDocumentChunker
{
    public IEnumerable<DocumentChunk> GetChunks(string sourceFilePath, string text)
    {
        // Split the full text into individual words, ignoring extra spaces
        var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var position = 0;   // current word index — where the next chunk starts
        var chunkIndex = 0; // which chunk number we're on (0, 1, 2, ...)

        while (position < words.Length) // keep going until we've covered all words
        {
            var start = position;                             // remember where this chunk starts
            var end = Math.Min(position + 500, words.Length); // chunk ends at start+500, or end of text if shorter
            var chunkText = string.Join(" ", words[position..end]); // rejoin words[start..end] back into a string

            // advance by 450 (not 500) — the 50-word overlap keeps context at boundaries
            position += 450;

            yield return new DocumentChunk(
                sourceFilePath, // which file this came from
                chunkIndex,     // e.g. 0, 1, 2 ...
                chunkText,      // the actual text of this chunk
                end - start     // word count = end - start (no need to re-split)
            );

            chunkIndex++; // move to next chunk number AFTER yielding
        }
    }
}