module Day3

open System.IO

let real = @"3.txt" |> File.ReadAllLines

let mock =
    [ "vJrwpWtwJgWrhcsFMMfFFhFp"
      "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL"
      "PmmdzqPrVvPwwTWBwg"
      "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn"
      "ttgJtRGJQctTZtZT"
      "CrZsJsPPZsGzwwsLwLmpwMDw" ]

let inline findAndConvertMatches ls =
    ls
    |> Seq.map Set.ofSeq
    |> Set.intersectMany
    |> Set.map (int >> (fun i -> i - 96) >> (fun i -> if i < 0 then i + 58 else i)) // HA HA LOL!
    |> Set.toSeq
    |> Seq.head

real
|> Seq.map (Seq.splitInto 2 >> findAndConvertMatches)
|> Seq.sum
|> printfn "%A"

real
|> Seq.chunkBySize 3
|> Seq.map findAndConvertMatches
|> Seq.sum
|> printfn "%A"
