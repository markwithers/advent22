module Utils

open FSharpx.String

let tryParseWith (tryParseFunc: string -> bool * _) =
    tryParseFunc
    >> function
        | true, v -> Some v
        | false, _ -> None

let parseInt = tryParseWith System.Int32.TryParse

let words = splitChar [| ' ' |] >> Array.toList

let split c s = s |> splitChar [| c |] |> Array.toList

let toTuple ls =
    match ls with
    | [ a; b ] -> Some(a, b)
    | _ -> None
