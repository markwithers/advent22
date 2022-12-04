module Day4

open System.IO
open Utils

let real = @"4.txt" |> File.ReadAllLines |> Seq.toList

let mock = [ "2-4,6-8"; "2-3,4-5"; "5-7,7-9"; "2-8,3-7"; "6-6,4-6"; "2-6,4-8" ]

let crossover a b =
    let a' = Set.ofList a
    let b' = Set.ofList b
    Set.isSubset a' b' || Set.isSubset b' a'

let overlap a b =
    Set.intersect (Set.ofList a) (Set.ofList b) |> Set.isEmpty = false

real
|> List.filter (
    split ','
    >> List.map (split '-' >> List.map int >> toTuple >> Option.defaultValue (0, 0))
    >> toTuple
    >> Option.map (fun ((a1, a2), (b1, b2)) -> crossover [ a1..a2 ] [ b1..b2 ])
    >> Option.defaultValue false
)
|> List.length
|> printfn "%i"

real
|> List.filter (
    split ','
    >> List.map (split '-' >> List.map int >> toTuple >> Option.defaultValue (0, 0))
    >> toTuple
    >> Option.map (fun ((a1, a2), (b1, b2)) -> overlap [ a1..a2 ] [ b1..b2 ])
    >> Option.defaultValue false
)
|> List.length
|> printfn "%i"
