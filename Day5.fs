module Day5

open System
open System.IO

let private parseInput filePath =
    File.ReadAllLines filePath

let private bsp max (s: string) =
    ({| Min = 0; Max = max |}, s)
    ||> Seq.fold (fun s t ->
        let pivot = (s.Max + s.Min) / 2
        match t with
        | 'F' | 'L' -> {| s with Max = pivot |}
        | 'B' | 'R' -> {| s with Min = pivot + 1 |}
    )
    |> fun s -> s.Min

let private runForLine (line: string) =
    let span = line.AsSpan()
    let row = bsp 127 (String(span.Slice(0, 7)))
    let col = bsp 7 (String(line.AsSpan().Slice(7, 3)))
    row * 8 + col

let private calculate = parseInput >> (Seq.map runForLine) >> Seq.max

let run filePath =
    filePath
    |> calculate
    |> printfn "Highest seat ID: %i"

let private findMissingSeat seats =
    seats
    |> Seq.sort
    |> Seq.pairwise
    |> Seq.tryFind (fun (l, r) -> r - l > 1)

let runPart2 filePath =
    filePath
    |> parseInput
    |> Seq.map runForLine
    |> findMissingSeat
    |> function
    | Some (l, r) -> printfn "Your seat is: %i" (l + 1)
    | None -> printfn "Oops"