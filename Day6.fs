module Day6

open System.IO

let real = @"6.txt" |> File.ReadAllLines |> Seq.head |> List.ofSeq

let mock = "mjqjpqmgbljsphdztnvjfqwrcgsmlb" |> List.ofSeq

let distinct xs = List.distinct xs = xs

let indexOfFirstUniqueWindow windowSize =
    List.windowed windowSize >> List.findIndex distinct >> (+) windowSize

let solve1 = indexOfFirstUniqueWindow 4
let solve2 = indexOfFirstUniqueWindow 14

mock |> solve1 |> printfn "%A"
real |> solve1 |> printfn "%A"

mock |> solve2 |> printfn "%A"
real |> solve2 |> printfn "%A"
