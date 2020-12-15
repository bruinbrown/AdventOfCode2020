module Day9

open System.IO

let private parseInput filePath =
    filePath
    |> File.ReadAllLines
    |> Array.map int64

let private findFirstError numbers =
    numbers
    |> Seq.windowed 26
    |> Seq.tryPick (fun window ->
        let numbers = window |> Array.ofSeq
        let total = numbers.[25]
        numbers.[0..24]
        |> Seq.scan (fun (seenNumbers, isValid) t ->
            let target = total - t
            if seenNumbers |> Set.contains target then seenNumbers, true
            else
                seenNumbers |> Set.add t, isValid
        ) (Set.empty, false)
        |> Seq.tryPick (fun (_, isValid) -> if isValid then Some true else None)
        |> function
        | Some _ -> None
        | None -> Some total
    )

let run filePath =
    filePath
    |> parseInput
    |> findFirstError
    |> function
    | Some err -> printfn "Error %i" err
    | None -> printfn "Valid stream|"

let private executePart2 invalidNumber (numbers: int64 []) =
    [0..numbers.Length-1]
    |> Seq.tryPick (fun idx ->
        let nextAvailableNumbers = numbers.[idx..numbers.Length-1]
        nextAvailableNumbers
        |> Seq.scan (fun (sum, last) t -> sum + t, t) (0L, numbers.[idx])
        |> Seq.tryPick (fun (sum, last) -> if sum >= invalidNumber then Some (sum, last) else None)
        |> Option.bind (fun (sum, last) ->
            if sum = invalidNumber then Some (numbers.[idx], last) else None)
    )

let runPart2 filePath =
    let numbers = parseInput filePath
    let invalidNumber = findFirstError numbers
    match invalidNumber with
    | Some invalid ->
        executePart2 invalid numbers
        |> Option.iter (fun (a,b) -> printfn "Sum: %i" (a + b))
    | None -> printfn "oops"