module Day5

open System.IO
open System.Text.RegularExpressions
open Utils
open FSharpx.Prelude

let real = @"5.txt" |> File.ReadAllLines

let mock =
    [| "move 1 from 2 to 1"
       "move 3 from 1 to 3"
       "move 2 from 2 to 1"
       "move 1 from 1 to 2" |]

let mockCrate =
    Map [ (1, [| "Z"; "N" |]); (2, [| "M"; "C"; "D" |]); (3, [| "P" |]) ]

let crate =
    Map
        [ (1, [| "J"; "H"; "G"; "M"; "Z"; "N"; "T"; "F" |])
          (2, [| "V"; "W"; "J" |])
          (3, [| "G"; "V"; "L"; "J"; "B"; "T"; "H" |])
          (4, [| "B"; "P"; "J"; "N"; "C"; "D"; "V"; "L" |])
          (5, [| "F"; "W"; "S"; "M"; "P"; "R"; "G" |])
          (6, [| "G"; "H"; "C"; "F"; "B"; "N"; "V"; "M" |])
          (7, [| "D"; "H"; "G"; "M"; "R" |])
          (8, [| "H"; "N"; "M"; "V"; "Z"; "D" |])
          (9, [| "G"; "N"; "F"; "H" |]) ]

let getMatches (regex: Regex) str =
    let mc = regex.Matches str

    match mc.Count with
    | 3 -> Some(((mc.Item 0).Value |> int), ((mc.Item 1).Value |> int), ((mc.Item 2).Value |> int))
    | _ -> None

let move' fn qty start dest crate =
    let chunk =
        crate |> Map.find start |> (fun a -> Array.skip (Array.length a - qty) a) |> fn

    crate
    |> Map.change start ^ Option.map ^ fun a -> Array.take (Array.length a - qty) a
    |> Map.change dest ^ Option.map (Array.append <*> chunk)

let moveReverse qty start dest crate = move' Array.rev qty start dest crate

let move qty start dest crate = move' id qty start dest crate

let solve fn c ls =
    ls
    |> Seq.fold
        (fun acc str ->
            str
            |> getMatches ^ Regex @"\d+"
            |> Option.map ^ fun t -> t |||> fn <| acc
            |> Option.defaultValue acc)
        c
    |> Map.map ^ konst Array.last
    |> Map.values
    |> String.concat ""
    |> printfn "%A"

mock |> solve moveReverse mockCrate
mock |> solve move mockCrate

real |> solve moveReverse crate
real |> solve move crate
