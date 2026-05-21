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

---

I've moved back to just a simple byte array and a Class type. It might be faster to modify three cells every update for a full game instead of making a new chessboard on each load (which should theoretically cut down copyover costs, assuming I know how/when that works). Now I can build the game, and *then* optimize it. 

I also just found out about `Math.DivRem(index, num)` which gets both values of the division operator. Because, as we learned about in architecture, we're calculating both, but just getting the value we need out of it. Turns out that's quite useful for my use case. 