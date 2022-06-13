# Linq Extensions
A collection of reusable Linq extensions.
 
## Chunk extensions

``` C#
var list = new List<int>();
for (var i = 0; i < 21000; i++) list.Add(i);

var chunks = list.Chunks(1800);
```
chunks contains a list of 12 lists, which are
each 1800 elements except for the last, which
contains the 1200 remaining elements.

## Group extensions
This extension groups elements based on a key
definition and on a equality comparer.

``` C#
var list = new List<string>()
{
    "Monkey",
    "Disco",
    "monkey",
    "banana",
    " Monkey",
    "Banana",
    "tree"
};
var grouped = list.Group(new CaseInsensitiveComparer());
```
Suppose that the CaseInsensitiveComparere compares on trimmed
and lowercase values, than this results in a dictionary with
4 pairs:
  - "Monkey": { "Monkey", "monkey", " Monkey" },
  - "Disco": { "Disco" },
  - "banana": { "banana", "Banana" },
  - "tree": { "tree" }

## Duplicate extensions
This extension finds duplicates in a list an collects
all duplicates in a dictionary.

``` C#
var list = new List<string>()
{
    "Monkey",
    "Disco",
    "monkey",
    "banana",
    " Monkey",
    "Banana",
    "tree"
};
var grouped = list.Group(new CaseInsensitiveComparer());
```
Suppose that the CaseInsensitiveComparere compares on trimmed
and lowercase values, than this results in a dictionary with
2 pairs:
- "Monkey": { "Monkey", "monkey", " Monkey" },
- "banana": { "banana", "Banana" }


