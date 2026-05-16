# Structures.Games.Chess

So, I benchmarked both a full and a nibbler version of the InlineArray function (which.. I don't really like the syntax of), but both of them passed the small test suite required of them (5/15/26 commits). I used BenchmarkDotNet for both versions (an array of length 64 vs length 32 where I use byte functions to get either side of the nibble). Surprisingly, the one that needed a division and a modulo to get the correct index (as well as the byte manipulation) was **faster** by `0.1 ns`, between 0.6 and 0.7 ns (so a good amount faster). 

(So, the purpose of this story is to not go back to think that an array value for each individual square will be better. Somehow)

---

I've been testing a whole bunch of data structures (way too early; I need to actually do and hold the logic so I can optimize it later with the way it will actually be used). I've tried: 
- Immutable array (slower)
- Span and ReadOnlySpan (way slower and ruins the API)
- List/Array (slower, more GC)
- InlineArray (seems like an undeveloped/possible to change API (and I don't like it), but I think the fastest?)

I'm trying way too hard to write things that are fast before they work. Like, I'm already reading about SIMD instructions to possibly calculate feasible moves faster, even though all I can do is get the value at a square. 

I need to stop and just make it work first. 