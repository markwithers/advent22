module Day2

open System.IO
open Utils

let real = @"2.txt" |> File.ReadAllLines |> Seq.toList

let mock = [ "A Y"; "B X"; "C Z" ]

type RPS =
    | Rock
    | Paper
    | Scissors
    | NoPlay

type Strategy =
    | Win
    | Lose
    | Draw
    | NoStrategy

let parseRPS str =
    match str with
    | "A"
    | "X" -> Rock
    | "B"
    | "Y" -> Paper
    | "C"
    | "Z" -> Scissors
    | _ -> NoPlay

let parseStrategy str =
    match str with
    | "X" -> Lose
    | "Y" -> Draw
    | "Z" -> Win
    | _ -> NoStrategy

// Rock, Paper, Scissors
let getScore theirs mine =
    match mine with
    | NoPlay -> None
    | Rock ->
        Option.map2
            (+)
            (Some 1)
            (match theirs with
             | Rock -> Some 3
             | Paper -> Some 0
             | Scissors -> Some 6
             | NoPlay -> None)
    | Paper ->
        Option.map2
            (+)
            (Some 2)
            (match theirs with
             | Rock -> Some 6
             | Paper -> Some 3
             | Scissors -> Some 0
             | NoPlay -> None)
    | Scissors ->
        Option.map2
            (+)
            (Some 3)
            (match theirs with
             | Rock -> Some 0
             | Paper -> Some 6
             | Scissors -> Some 3
             | NoPlay -> None)

// Rock, Paper, Scissors
let getScore2 theirs mine =
    match mine with
    | NoStrategy -> None
    | Lose ->
        match theirs with
        | Rock -> Some 3
        | Paper -> Some 1
        | Scissors -> Some 2
        | NoPlay -> None
    | Draw ->
        Option.map2
            (+)
            (Some 3)
            (match theirs with
             | Rock -> Some 1
             | Paper -> Some 2
             | Scissors -> Some 3
             | NoPlay -> None)
    | Win ->
        Option.map2
            (+)
            (Some 6)
            (match theirs with
             | Rock -> Some 2
             | Paper -> Some 3
             | Scissors -> Some 1
             | NoPlay -> None)

real
|> List.map (
    words
    >> toTuple
    >> Option.bind (fun (a, b) -> getScore (parseRPS a) (parseRPS b))
    >> Option.defaultValue 0
)
|> List.sum
|> printfn "%A"

real
|> List.map (
    words
    >> toTuple
    >> Option.bind (fun (a, b) -> getScore2 (parseRPS a) (parseStrategy b))
    >> Option.defaultValue 0
)
|> List.sum
|> printfn "%A"
