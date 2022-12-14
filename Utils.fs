module Utils

open FSharpx.String
open FSharpx.Prelude

let tryParseWith (tryParseFunc: string -> bool * _) =
    tryParseFunc
    >> function
        | true, v -> Some v
        | false, _ -> None

let parseInt = tryParseWith System.Int32.TryParse

let words = splitChar [| ' ' |]

let split c s = s |> splitChar [| c |]

let toTuple ss =
    match (List.ofSeq ss) with
    | [ a; b ] -> Some(a, b)
    | _ -> None

let (<*>) = flip

let startsWith = startsWith
