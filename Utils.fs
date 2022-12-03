module Utils

open FSharpx.String

let tryParseWith (tryParseFunc: string -> bool * _) =
    tryParseFunc
    >> function
        | true, v -> Some v
        | false, _ -> None

let parseInt = tryParseWith System.Int32.TryParse

let words = splitChar [| ' ' |] >> Array.toList

let toTuple ls =
    match ls with
    | [ a; b ] -> Some(a, b)
    | _ -> None

let rec apperture' lls rem n =
    if List.length rem >= n then
        apperture' (List.append lls [ (List.take n rem) ]) (List.skip n rem) n
    else
        (if List.length rem = 0 then lls else List.append lls [ rem ])

let apperture n ls = apperture' [] ls n
