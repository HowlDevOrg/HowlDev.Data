# Structures.Games.Chess

So, I benchmarked both a full and a nibbler version of the InlineArray function (which.. I don't really like the syntax of), but both of them passed the small test suite required of them (5/15/26 commits). I used BenchmarkDotNet for both versions (an array of length 64 vs length 32 where I use byte functions to get either side of the nibble). Surprisingly, the one that needed a division and a modulo to get the correct index (as well as the byte manipulation) was **faster** by `0.1 ns`, between 0.6 and 0.7 ns (so a good amount faster). 

(So, the purpose of this story is to not go back to think that an array value for each individual square will be better. Somehow)