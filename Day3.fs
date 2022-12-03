module Day3

open System.IO
open Utils

let real = @"3.txt" |> File.ReadAllLines |> Seq.toList

let mock =
    [ "vJrwpWtwJgWrhcsFMMfFFhFp"
      "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL"
      "PmmdzqPrVvPwwTWBwg"
      "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn"
      "ttgJtRGJQctTZtZT"
      "CrZsJsPPZsGzwwsLwLmpwMDw" ]

let inline findAndConvertMatches ls =
    ls
    |> List.map Set.ofSeq
    |> Set.intersectMany
    |> Set.map (int >> (fun i -> i - 96) >> (fun i -> if i < 0 then i + 58 else i)) // HA HA LOL!
    |> Set.toList
    |> List.head

real
|> List.map (Seq.toList >> List.splitInto 2 >> findAndConvertMatches)
|> List.sum
|> printfn "%A"

real
|> (apperture 3)
|> List.map findAndConvertMatches
|> List.sum
|> printfn "%A"
