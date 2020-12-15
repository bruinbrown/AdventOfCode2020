module Day8

open System.IO

type Command = Acc of int | Jmp of int | Nop of int

let parseInput filePath =
    filePath
    |> File.ReadAllLines
    |> Array.map (fun line ->
        match line.Split(' ') with
        | [| "acc"; acc|] -> Acc (int acc)
        | [| "nop"; nop |] -> Nop (int nop)
        | [| "jmp"; jmp |] -> Jmp (int jmp))

let private execute (program: Command []) =
    let rec _run acc idx executed =
        if executed |> Set.contains idx then Error (acc, idx)
        elif idx = program.Length then Ok (acc)
        else
            match program.[idx] with
            | Acc accAdd -> _run (accAdd + acc) (idx + 1) (executed |> Set.add idx)
            | Jmp pc -> _run acc (idx + pc) (executed |> Set.add idx)
            | Nop _ -> _run acc (idx + 1) (executed |> Set.add idx)
    _run 0 0 Set.empty

let run filePath =
    filePath
    |> parseInput
    |> execute
    |> function
    | Ok acc -> printfn "Program executed successfully"
    | Error (acc, idx) -> printfn "Program terminated in an infinite loop with acc %i and idx %i" acc idx

let private findWorkingProgram (program: Command []) =
    let isValidChange = function | Nop _ | Jmp _ -> true | _ -> false
    let change = function | Nop a -> Jmp a | Jmp a -> Nop a | Acc a -> Acc a
    
    [0..program.Length - 1]
    |> Seq.filter (fun t -> isValidChange program.[t])
    |> Seq.tryPick (fun t ->
        let prevVal = program.[t]
        program.[t] <- change program.[t]
        match execute program with
        | Ok acc -> Some acc
        | Error (acc, idx) ->
            program.[t] <- prevVal
            None)

let runPart2 filePath =
    filePath
    |> parseInput
    |> findWorkingProgram
    |> function
    | Some acc -> printfn "Final acc %i" acc
    | None -> printfn "Couldn't fnd a working program"