module Day1

open System.IO

let private targetNumber = 2020

let private readInput inputFilePath =
    File.ReadAllLines inputFilePath
    |> Seq.map int

type private Result = { SeenNumbers : Set<int>; MatchingPair : (int * int) option }

let private findNumbers total inp =
    inp
    |> Seq.fold
        (fun s t ->
            let setTarget = total - t
            if Set.contains setTarget s.SeenNumbers then
                { s with MatchingPair = Some(t, setTarget) }
            else
                { s with SeenNumbers = Set.add t s.SeenNumbers }) { SeenNumbers = Set.empty; MatchingPair = None }

let private calculateExpense res =
    match res with
    | { MatchingPair = Some(i, j) } -> Ok(i * j)
    | _ -> Error "No matching pair found"

let private calculate = readInput >> (findNumbers targetNumber) >> calculateExpense

let run filePath =
    match calculate filePath with
    | Ok res -> printfn "Expense value: %i" res
    | Error err -> printfn "%A" err

type private Part2Result = { SeenNumbers : Map<int, int list>; MatchingTriple : (int * int * int) option }

let private findNumbersPart2 inp =
    let inpSet = inp |> Set.ofSeq
    inp
    |> Seq.fold (fun s t ->
        match s with
        | None ->
            let remaining = inpSet |> Set.remove t
            let totalNeeded = targetNumber - t
            let matchingPair = findNumbers totalNeeded remaining
            match matchingPair.MatchingPair with
            | Some (a, b) -> Some (t, a, b)
            | _ -> None
        | Some _ ->
            s
    ) None

let private calculateExpensePart2 res =
    match res with
    | Some (a, b, c) -> Ok(a * b * c)
    | None -> Error("Could not calculate a valid triple")

let private calculatePart2 = readInput >> findNumbersPart2 >> calculateExpensePart2

let runPart2 filePath =
    match calculatePart2 filePath with
    | Ok res -> printfn "Final result: %i" res
    | Error err -> printfn "Error: %s" err