module Day1

open System.IO

type Tally = { current: int; combined: List<int> }

let countItem tally str =
    match Utils.parseInt str with
    | Some i -> { tally with current = tally.current + i }
    | None ->
        { combined = tally.combined @ [ tally.current ]
          current = 0 }

let calories = @"1.txt" |> File.ReadAllLines |> Seq.toList

let tally = calories |> List.fold countItem { current = 0; combined = [] }

// Part 1
tally |> (fun tally -> tally.combined) |> List.max |> printfn "%A"

// Part 2
tally
|> fun tally -> tally.combined
|> List.sortDescending
|> List.take 3
|> List.sum
|> printfn "%A"
