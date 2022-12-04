module Day2

open System.IO
open Utils

let real = @"2.txt" |> File.ReadAllLines

let mock = [ "A Y"; "B X"; "C Z" ]

type RPS =
    | Rock
    | Paper
    | Scissors

type Strategy =
    | Win
    | Lose
    | Draw

let parseRPS str =
    match str with
    | "A"
    | "X" -> Some Rock
    | "B"
    | "Y" -> Some Paper
    | "C"
    | "Z" -> Some Scissors
    | _ -> None

let parseStrategy str =
    match str with
    | "X" -> Some Lose
    | "Y" -> Some Draw
    | "Z" -> Some Win
    | _ -> None

// Rock, Paper, Scissors
let getScore theirs mine =
    match mine with
    | Rock ->
        1
        + (match theirs with
           | Rock -> 3
           | Paper -> 0
           | Scissors -> 6)
    | Paper ->
        2
        + (match theirs with
           | Rock -> 6
           | Paper -> 3
           | Scissors -> 0)
    | Scissors ->
        3
        + (match theirs with
           | Rock -> 0
           | Paper -> 6
           | Scissors -> 3)

// Rock, Paper, Scissors
let getScore2 theirs mine =
    match mine with
    | Lose ->
        match theirs with
        | Rock -> 3
        | Paper -> 1
        | Scissors -> 2
    | Draw ->
        3
        + (match theirs with
           | Rock -> 1
           | Paper -> 2
           | Scissors -> 3)
    | Win ->
        6
        + (match theirs with
           | Rock -> 2
           | Paper -> 3
           | Scissors -> 1)

real
|> Seq.map (
    words
    >> toTuple
    >> Option.bind (fun (a, b) -> Option.map2 getScore (parseRPS a) (parseRPS b))
    >> Option.defaultValue 0
)
|> Seq.sum
|> printfn "%A"

real
|> Seq.map (
    words
    >> toTuple
    >> Option.bind (fun (a, b) -> Option.map2 getScore2 (parseRPS a) (parseStrategy b))
    >> Option.defaultValue 0
)
|> Seq.sum
|> printfn "%A"
