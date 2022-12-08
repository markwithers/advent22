module Day7

open System.IO
open System.Text.RegularExpressions

type Directory = Map<string, int>

let mock =
    [| "$ cd /"
       "$ ls"
       "dir a"
       "14848514 b.txt"
       "8504156 c.dat"
       "dir d"
       "$ cd a"
       "$ ls"
       "dir e"
       "29116 f"
       "2557 g"
       "62596 h.lst"
       "$ cd e"
       "$ ls"
       "584 i"
       "$ cd .."
       "$ cd .."
       "$ cd d"
       "$ ls"
       "4060174 j"
       "8033020 d.log"
       "5626152 d.ext"
       "7214296 k" |]

let real = @"7.txt" |> File.ReadAllLines

let (|CD|_|) str =
    if Utils.startsWith "$ cd " str then
        Some(str[5..])
    else
        None

let (|File|_|) str =
    let m = Regex.Match(str, "(\d+)")
    if (m.Success) then Some(int m.Groups.[1].Value) else None

let updateDirectorySize size directory path =
    directory
    |> Map.change path (function
        | Some s -> Some(s + size)
        | None -> Some size)

let fn (paths, directory) =
    function
    | CD "/" -> [ "" ], directory
    | CD ".." -> List.tail paths, directory
    | CD d -> (List.head paths + "/" + d) :: paths, directory
    | File size -> paths, paths |> List.fold (updateDirectorySize size) directory
    | _ -> paths, directory

let solve1 =
    Seq.fold fn (List.Empty, Map.empty)
    >> snd
    >> Map.values
    >> Seq.filter ((>=) 100000)
    >> Seq.sum

let solve2 ls =
    let directory = ls |> Seq.fold fn (List.Empty, Map.empty) |> snd
    let gap = 30000000 - 70000000 + (Map.find "" directory)

    directory |> Map.values |> Seq.filter ((<) gap) |> Seq.min

mock |> solve1 |> printfn "%A"
mock |> solve2 |> printfn "%A"

real |> solve1 |> printfn "%A"
real |> solve2 |> printfn "%A"
