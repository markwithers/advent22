module Day1

open System.IO

type Tally = { current: int; combined: List<int> }

let countItem tally str =
    match Utils.parseInt str with
    | Some i -> { tally with current = tally.current + i }
    | None ->
        { combined = tally.combined @ [ tally.current ]
          current = 0 }

let real = @"1.txt" |> File.ReadAllLines

let tally = real |> Seq.fold countItem { current = 0; combined = [] }

// Part 1
tally |> (fun tally -> tally.combined) |> List.max |> printfn "%A"

// Part 2
tally
|> fun tally -> tally.combined
|> Seq.sortDescending
|> Seq.take 3
|> Seq.sum
|> printfn "%i"
